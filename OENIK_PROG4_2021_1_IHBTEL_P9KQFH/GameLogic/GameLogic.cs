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
    using System.Windows;

    public enum Direction
    {
        Left, Right, Up, Down
    }

    public class GameLogic : IGameLogic
    {
        private GameModel model;
        private MapRepository mapRepo;
        private List<Ore> map;
        Ore[,] ore;

        int jumpCount = 0; //ugrasok számát számolja
        int maxJump = 2; // max mennyit ugorhatunk

        public event EventHandler RefreshScreen;

        public event EventHandler ChangeScreen;

        public event EventHandler BackToMapOneScreen;

        public event EventHandler ShopScreen;

        public GameLogic(GameModel model, MapRepository mapRepo)
        {
            this.model = model;
            this.mapRepo = mapRepo;
            this.map = this.mapRepo.DrawMap();
            this.ore = this.DrawMap();
        }

        public void MoveCharacter(Direction d, int mapID)
        {
            if (mapID == 0 || mapID == 3)
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
                else if (d == Direction.Right && this.model.Miner.Area.Right < Config.Width)
                {
                    int predictOreX = (int)((this.model.Miner.Area.Bottom - (Config.MinerHeight / 2)) / Config.oreHeight);
                    int predictOreY = ((int)this.model.Miner.Area.Right + 8) / Config.oreWidth;
                    if (predictOreY > Config.Width)
                    {
                        predictOreX = this.ore.GetLength(1);
                    }

                    if (predictOreY < 20)
                    {
                        if (!this.model.Miner.Area.IntersectsWith(this.ore[predictOreX, predictOreY].Area)
                            || this.ore[predictOreX, predictOreY].canPass == true)
                        {
                            this.model.Miner.ChangeX(7.5);
                        }
                    }
                }
                else if (d == Direction.Up && this.jumpCount < 3)
                {
                    this.jumpCount++;
                    int predictOreX = (int)((this.model.Miner.Area.Top - 10) / Config.oreHeight);
                    int predictOreYLeft = (int)(this.model.Miner.Area.Left + 1) / Config.oreWidth;
                    int predictOreYRight = (int)(this.model.Miner.Area.Right - 1) / Config.oreWidth;
                    int predictOreBottom = (int)(this.model.Miner.Area.Bottom + 10) / Config.oreHeight;

                    if (this.CanJumpMethod(predictOreX, predictOreYLeft, predictOreBottom, predictOreYRight, this.jumpCount))
                    {
                        this.model.Miner.ChangeY(-60);
                    }
                }
            }

            this.RefreshScreen?.Invoke(this, EventArgs.Empty);
        }

        public bool CanJumpMethod(int predictOreX, int predictOreYLeft, int predictOreBottom, int predictOreYRight, int jumpCount)
        {
            bool move = false;
            if (((!this.model.Miner.Area.IntersectsWith(this.ore[predictOreX, predictOreYLeft].Area)
                    && !this.model.Miner.Area.IntersectsWith(this.ore[predictOreX, predictOreYRight].Area)
                    && this.ore[predictOreBottom, predictOreYLeft].OreType != "air"
                    && this.ore[predictOreBottom, predictOreYRight].OreType != "air")
                    && jumpCount <= this.maxJump)
                    || ((this.ore[predictOreX, predictOreYLeft].canPass == true
                    && jumpCount <= this.maxJump)
                    && this.ore[predictOreX, predictOreYRight].canPass == true))
            {
                move = true;
            }

            return move;
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
                    this.jumpCount = 5;
                }
                else
                {
                    this.jumpCount = 0;
                }
            }
            else if (!this.model.Miner.Area.IntersectsWith(this.model.Ground.Area) && mapID == 3)
            {
                this.model.Miner.ChangeY(5);
            }

            this.RefreshScreen?.Invoke(this, EventArgs.Empty);
        }

        public void MineGate(int mapID)
        {
            if (this.model.Miner.Area.IntersectsWith(this.model.Gate.Area) && mapID == 0)
            {
                this.ChangeScreen?.Invoke(this, EventArgs.Empty);
            }
            else if (this.model.Miner.Area.IntersectsWith(this.model.MapThreeToOneGate.Area) && mapID == 3)
            {
                this.BackToMapOneScreen?.Invoke(this, EventArgs.Empty);
            }
            else if (this.model.Miner.Area.IntersectsWith(this.model.MapTwoToOneGate.Area) && mapID == 1)
            {
                this.BackToMapOneScreen?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Click()
        {
            // model.Button.Margin.
        }

        public bool IntersectsWithShop() // TODO: Eventként
        {
            if (this.model.Miner.Area.IntersectsWith(this.model.HealthShop.Area))
            {
                return true;
            }
            else if (this.model.Miner.Area.IntersectsWith(this.model.PetrolShop.Area))
            {
                return true;
            }
            else if (this.model.Miner.Area.IntersectsWith(this.model.PickaxShop.Area))
            {
                return true;
            }
            return false;
        }

        public void setCharPosition(double x, double y)
        {
            this.model.Miner.SetXY(x, y); // TODO Model?
        }

        public Ore[,] MapPart()
        {
            int minerPositionX = (int)this.model.Miner.Area.Y / Config.oreHeight;
            int minerPositionY = (int)this.model.Miner.Area.X / Config.oreWidth;

            if (minerPositionX - 2 < 0)
            {
                minerPositionX = 2;
            }

            if (minerPositionY - 2 < 0)
            {
                minerPositionY = 2;
            }

            if ((minerPositionX + 2) * 45 >= Config.Height)
            {
                minerPositionX = this.ore.GetLength(0) - 3;
            }

            if ((minerPositionY + 2) * 45 >= Config.Width)
            {
                minerPositionY = this.ore.GetLength(1) - 3;
            }

            // int topLeft = 0;
            Ore[,] renderedOres = new Ore[5, 5];
            int startingPosY = minerPositionY;
            for (int i = 0; i < renderedOres.GetLength(0); i++)
            {
                for (int j = 0; j < renderedOres.GetLength(1); j++)
                {

                    renderedOres[i, j] = this.ore[minerPositionX - 2, minerPositionY - 2];
                    minerPositionY++;
                }

                minerPositionY = startingPosY;
                minerPositionX++;
            }

            return renderedOres;
        }
    }
}
