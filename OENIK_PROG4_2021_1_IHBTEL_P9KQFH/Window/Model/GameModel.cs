namespace GameWindow.Model
{
    using global::Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class GameModel
    {
        public Character Miner { get; set; }

        public GameModel()
        {
            Miner = new Character(Config.Width / 2, Config.Height / 2, Config.MinerWidth, Config.MinerHeight);
        }
    }
}
