using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModelDll
{
    public class Pickax
    {
        enum Name
        {
            x, y, z // todo
        }

        enum Strength
        {
            x = 0, y = 1, z = 2, a = 3 //todo
        }

        public int Price { get; set; }
    }
}
