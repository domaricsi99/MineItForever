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
    using GameRepository;

    public class GameLogic
    {
        private readonly Repo repo;

        public enum Direction
        {
            Left, Right, Up, Down
        }

        public event EventHandler RefreshScreen;

        public GameLogic(Repo repo)
        {
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

        public void Fall()
        {
            if (!model.Miner.Area.IntersectsWith(model.Ground.Area))
            {
                model.Miner.ChangeY(3);
            }

            RefreshScreen?.Invoke(this, EventArgs.Empty);
        }
    }
}
