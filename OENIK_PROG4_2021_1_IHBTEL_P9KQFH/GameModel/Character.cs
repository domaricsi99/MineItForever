using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModelDll
{
    public class Character : Shape
    {
        public int Health { get; set; }

        public int Fuel { get; set; }

        public int Money { get; set; }

        public int Score { get; set; }

        public Pickax PickAx { get; set; }

        public List<Ore> Backpack;

        public double DX { get; set; }

        public double DY { get; set; }

        public Character(double x, double y, double w, double h)
            : base(x, y, w, h)
        {
            Health = 100;
            Fuel = 240;
            Money = 0;
            Score = 0;
            PickAx = new Pickax(); // todo
            Backpack = new List<Ore>();

            DX = 5;
            DY = 5;
        }
    }
}
