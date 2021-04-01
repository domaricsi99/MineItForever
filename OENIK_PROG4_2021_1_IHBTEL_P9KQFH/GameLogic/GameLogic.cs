using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameModelDll;

namespace GameLogicDll
{
    public class GameLogic
    {
        GameModel model;

        public enum Direction
        {
            Left, Right, Up, Down
        }

        public event EventHandler RefreshScreen;

        public GameLogic(GameModel model)
        {
            this.model = model;
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
