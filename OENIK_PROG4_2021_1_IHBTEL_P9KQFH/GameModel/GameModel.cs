using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModelDll
{
    public class GameModel
    {
        public Character Miner { get; set; }

        public Shape Ground { get; set; }

        public Shape Gate { get; set; }

        public GameModel()
        {
            this.Miner = new Character(Config.Width / 2, Config.Height / 2, Config.MinerWidth, Config.MinerHeight);
            this.Ground = new Shape(0, Config.Height - Config.GroundHeight, Config.GroundWidth, Config.GroundHeight);
            this.Gate = new Shape(Config.Width - Config.GateWidth, Config.Height - (Config.GroundHeight + Config.GateHeight), Config.GateWidth, Config.GateHeight);
        }
    }
}
