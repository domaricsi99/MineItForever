using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModelDll
{
    public class Ore //: Shape
    {
        enum Type
        {
            Silver, Gold, Copper, Diamond, Lava, Stone, Dirt
        }

        enum MiningLevel
        {
            Zero = 0, One = 1, Two = 2, Three = 3
        }

        public bool Hurt { get; set; }

        public int Value { get; set; }

        public int Score { get; set; }

    }
}
