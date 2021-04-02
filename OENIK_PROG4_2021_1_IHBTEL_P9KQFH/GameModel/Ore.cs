using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModelDll
{
    public class Ore //: Shape
    {
        public bool Hurt { get; set; }

        public int Value { get; set; }

        public int Score { get; set; }

        public string OreType { get; set; }

        public int Level { get; set; }

        public bool canPass { get; set; }
    }
}
