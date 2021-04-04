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
            int delimeter = 5;
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

        public void Fall()
        {
            if (!model.Miner.Area.IntersectsWith(model.Ground.Area))
            {
                model.Miner.ChangeY(3);
            }

            this.RefreshScreen?.Invoke(this, EventArgs.Empty);
        }

        public void MineGate()
        {
            // bool a = false;
            if (model.Miner.Area.IntersectsWith(model.Gate.Area))
            {
                // a = true;
            }
        }
    }
}
