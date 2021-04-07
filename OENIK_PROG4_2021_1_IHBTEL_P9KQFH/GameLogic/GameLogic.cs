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

    public class GameLogic
    {
        private GameModel model;
        private MapRepository mapRepo;
        private List<Ore> map;
        Ore[,] ore;

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
                    int predictOreX = (int)(this.model.Miner.area.Y / Config.oreHeight) - 1;
                    int predictOreY = ((int)this.model.Miner.area.X - 10) / Config.oreWidth;
                    if (predictOreX < 0)
                    {
                        predictOreX = 0;
                    }

                    if (!this.model.Miner.area.IntersectsWith(this.ore[predictOreX, predictOreY].area) || this.ore[predictOreX, predictOreY].canPass == true)
                    {
                        this.model.Miner.ChangeX(-10);
                    }
                }
                else if (d == Direction.Right && this.model.Miner.Area.Right < Config.Width)
                {
                    int predictOreX = (int)(this.model.Miner.area.Y / Config.oreHeight) - 1;
                    int predictOreY = ((int)this.model.Miner.area.X + 10) / Config.oreWidth;
                    if (predictOreX < 0)
                    {
                        predictOreX = 0;
                    }

                    if (!this.model.Miner.area.IntersectsWith(this.ore[predictOreX, predictOreY].area) || this.ore[predictOreX, predictOreY].canPass == true)
                    {
                        this.model.Miner.ChangeX(10);
                    }
                }
                else if (d == Direction.Up)
                {
                    this.model.Miner.ChangeY(-30);
                }
            }

            RefreshScreen?.Invoke(this, EventArgs.Empty);
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
                int predictOreX = (int)(this.model.Miner.area.Y + Config.MinerHeight + 5) / Config.oreHeight;
                int predictOreY = (int)this.model.Miner.area.X / Config.oreWidth;
                if (predictOreX == -1)
                {
                    predictOreX++;
                }

                if (!this.model.Miner.area.IntersectsWith(this.ore[predictOreX, predictOreY].area) || this.ore[predictOreX, predictOreY].canPass == true)
                {
                    this.model.Miner.ChangeY(5);
                }
            }

            this.RefreshScreen?.Invoke(this, EventArgs.Empty);
        }

        public void MineGate()
        {
            bool a = false;

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
