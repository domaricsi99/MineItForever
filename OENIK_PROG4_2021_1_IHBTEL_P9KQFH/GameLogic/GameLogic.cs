// <copyright file="GameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameLogicDll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GameModelDll;
    using GameRepository;

    public class GameLogic : MapRepository
    {
        private GameModel model;
        private MapRepository mapRepo;
        private List<Ore> map;
        Ore[,] ore;

        int jumpCount = 0; //ugrasok számát számolja
        int maxJump = 2; // max mennyit ugorhatunk

        public enum Direction
        {
            Left, Right, Up, Down
        }

        public event EventHandler RefreshScreen;

        public event EventHandler ChangeScreen;

        public GameLogic(GameModel model, MapRepository mapRepo)
        {
            this.model = model;
            this.mapRepo = mapRepo;
            this.map = this.mapRepo.DrawMap();
            this.ore = this.DrawMap();
        }

        public void MoveCharacter(Direction d, int mapID)
        {
            if (mapID == 0)
            {
                if (d == Direction.Left && this.model.Miner.Area.Left > 0)
                {
                    this.model.Miner.ChangeX(-10);
                }
                else if (d == Direction.Right && this.model.Miner.Area.Right < Config.Width)
                {
                    this.model.Miner.ChangeX(+10);
                }
                else if (d == Direction.Up)
                {
                    this.model.Miner.ChangeY(-30);
                }
            }
            else if (mapID == 1)
            {
                if (d == Direction.Left && this.model.Miner.Area.Left > 0)
                {
                    int predictOreX = (int)((this.model.Miner.Area.Bottom - (Config.MinerHeight / 2)) / Config.oreHeight);
                    int predictOreY = ((int)this.model.Miner.Area.Left - 8) / Config.oreWidth;

                    if (!this.model.Miner.Area.IntersectsWith(this.ore[predictOreX, predictOreY].Area)
                        || this.ore[predictOreX, predictOreY].canPass == true)
                    {
                        this.model.Miner.ChangeX(-7.5);
                    }
                }
                else if (d == Direction.Right && this.model.Miner.Area.Right < Config.Width) // <=?
                {
                    int predictOreX = (int)((this.model.Miner.Area.Bottom - (Config.MinerHeight / 2)) / Config.oreHeight);
                    int predictOreY = ((int)this.model.Miner.Area.Right + 8) / Config.oreWidth;
                    if (predictOreY > Config.Width)
                    {
                        predictOreX = this.ore.GetLength(1);
                    }

                    if (predictOreY < 20) // így nem száll el ha ez nincs IndexOutOfRangeException
                    {
                    if (!this.model.Miner.Area.IntersectsWith(this.ore[predictOreX, predictOreY].Area)
                        || this.ore[predictOreX, predictOreY].canPass == true) // itt jon mert az predictOreY 20 lesz 
                        {
                            this.model.Miner.ChangeX(8.5); // 7.5
                        }
                    }
                }
                else if (d == Direction.Up)
                {
                    this.jumpCount++;
                    int predictOreX = (int)((this.model.Miner.Area.Top - 10) / Config.oreHeight);
                    int predictOreYLeft = (int)(this.model.Miner.Area.Left + 1) / Config.oreWidth;
                    int predictOreYRight = (int)(this.model.Miner.Area.Right - 1) / Config.oreWidth;
                    int predictOreBottom = (int)(this.model.Miner.Area.Bottom + 10) / Config.oreHeight;

                    if (((!this.model.Miner.Area.IntersectsWith(this.ore[predictOreX, predictOreYLeft].Area) // TODO: Levegoben nem kéne ugorjon hehe => kész félig meddig nem tökéletes R
                    && !this.model.Miner.Area.IntersectsWith(this.ore[predictOreX, predictOreYRight].Area)
                    && this.ore[predictOreBottom, predictOreYLeft].OreType != "air"
                    && this.ore[predictOreBottom, predictOreYRight].OreType != "air")
                    && this.jumpCount <= this.maxJump) // ugrások számának vizsgálata
                    || ((this.ore[predictOreX, predictOreYLeft].canPass == true
                    && this.jumpCount <= this.maxJump)
                    && this.ore[predictOreX, predictOreYRight].canPass == true))
                    {
                        this.model.Miner.ChangeY(-60);
                    }
                    else
                    {
                        this.jumpCount = 0;
                    }
                }
            }

            this.RefreshScreen?.Invoke(this, EventArgs.Empty);
        }

        public Ore[,] DrawMap()
        {
            int delimeter = 20;
            Ore[,] oreMatrix = new Ore[this.map.Count / delimeter, delimeter];
            int counter = 0;
            for (int i = 0; i < oreMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < oreMatrix.GetLength(1); j++)
                {
                    oreMatrix[i, j] = this.map[counter];
                    counter++;
                }
            }

            return oreMatrix;
        }

        public void Fall(int mapID) // csak alattunk
        {
            if (!this.model.Miner.Area.IntersectsWith(this.model.Ground.Area) && mapID == 0)
            {
                this.model.Miner.ChangeY(5);
            }
            else if (mapID == 1)
            {
                int predictOreX = (int)(this.model.Miner.Area.Bottom + 5) / Config.oreHeight;
                int predictOreYLeft = (int)(this.model.Miner.Area.Left + 1) / Config.oreWidth;
                int predictOreYRight = (int)(this.model.Miner.Area.Right - 1) / Config.oreWidth;

                if ((!this.model.Miner.Area.IntersectsWith(this.ore[predictOreX, predictOreYLeft].Area)
                    && !this.model.Miner.Area.IntersectsWith(this.ore[predictOreX, predictOreYRight].Area))
                    || (this.ore[predictOreX, predictOreYLeft].canPass == true && this.ore[predictOreX, predictOreYRight].canPass == true))
                {
                    this.model.Miner.ChangeY(5);
                }
            }

            this.RefreshScreen?.Invoke(this, EventArgs.Empty);
        }

        public void MineGate()
        {
            if (this.model.Miner.Area.IntersectsWith(this.model.Gate.Area))
            {
                this.ChangeScreen?.Invoke(this, EventArgs.Empty);
            }
        }

        public void setCharPosition(double x, double y)
        {
            this.model.Miner.SetXY(x, y); // TODO Model?
        }
    }
}
