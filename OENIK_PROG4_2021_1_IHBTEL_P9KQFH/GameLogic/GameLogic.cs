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
        GameModel model;
        Repo repo;

        public enum Direction
        {
            Left, Right, Up, Down
        }

        public event EventHandler RefreshScreen;

        public event EventHandler ChangeScreen;

        public GameLogic(GameModel model, Repo repo)
        {
            this.model = model;
            this.repo = repo;
        }

        public void MoveCharacter(Direction d)
        {
            // miner.Area.Left < 0 || miner.Area.Right > Config.Width
            if (d == Direction.Left && model.Miner.Area.Left > 0)
            {
                model.Miner.ChangeX(-10);
            }
            else if (d == Direction.Right && model.Miner.Area.Right < Config.Width)
            {
                model.Miner.ChangeX(+10);
            }
            else if (d == Direction.Up)
            {
                model.Miner.ChangeY(-30);
            }

            RefreshScreen?.Invoke(this, EventArgs.Empty);
        }

        public Ore[,] DrawMap()
        {
            List<Ore> map = this.repo.GameMapRepository.DrawMap();
            int delimeter = 20;
            Ore[,] oreMatrix = new Ore[map.Count/delimeter, delimeter];
            int counter = 0;
            for (int i = 0; i < oreMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < oreMatrix.GetLength(1); j++)
                {
                    oreMatrix[i, j] = map[counter];
                    counter++;
                }
            }

            return oreMatrix;
        }

        public void Fall(int mapID)
        {
            List<Ore> ores = this.repo.GameMapRepository.DrawMap();
            int numOfIntersect = 0;
            if (!model.Miner.Area.IntersectsWith(model.Ground.Area) && mapID == 0)
            {
                model.Miner.ChangeY(3);
            }
            else if (mapID == 1)
            {
                foreach (var item in ores)
                {
                    if (this.model.Miner.area.IntersectsWith(item.area))
                    {
                        numOfIntersect++;
                    }
                }

                if (numOfIntersect == 0)
                {
                    this.model.Miner.ChangeY(3);
                }
            }

            this.RefreshScreen?.Invoke(this, EventArgs.Empty);
        }

        public void MineGate()
        {
            bool a = false;

            if (model.Miner.Area.IntersectsWith(model.Gate.Area))
            {
                this.ChangeScreen?.Invoke(this, EventArgs.Empty);
            }
        }

        public void setCharPosition(double x, double y)
        {
            model.Miner.SetXY(x, y); // TODO Model?
        }
    }
}
