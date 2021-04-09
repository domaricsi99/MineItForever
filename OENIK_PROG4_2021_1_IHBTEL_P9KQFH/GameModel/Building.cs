using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModelDll
{
    public class Building : Shape
    {
        public string Name { get; set; }

        public Building()
        {

        }

        public Building(double x, double y, double w, double h)
            : base(x, y, w, h)
        {

        }
    }
}
