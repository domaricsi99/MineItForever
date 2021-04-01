using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameWindow.Model;

namespace GameWindow.Logic
{
    class GameLogic
    {
        GameModel model;

        public enum Direction
        {
            Left, Right
        }

        public event EventHandler RefreshScreen;

        public GameLogic(GameModel model)
        {
            this.model = model;
        }

        public void MoveCharacter(Direction d)
        {
            if (d == Direction.Left)
            {
                model.Miner.ChangeX(-10);
            }
            else
            {
                model.Miner.ChangeX(+10);
            }

            RefreshScreen?.Invoke(this, EventArgs.Empty);
        }
    }
}
