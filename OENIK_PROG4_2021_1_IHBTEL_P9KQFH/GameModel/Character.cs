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

        public int PickAxLevel { get; set; }

        public List<Ore> Backpack;

        public double DX { get; set; }

        public double DY { get; set; }

        public Character()
        {
        }

        public Character(double x, double y, double w, double h)
            : base (x,y,w,h)
        {
        }
    }
}
