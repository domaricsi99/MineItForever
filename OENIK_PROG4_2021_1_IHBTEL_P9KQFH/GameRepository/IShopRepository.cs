using GameModelDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRepository
{
    interface IShopRepository
    {
        public List<Pickax> PickaxList();
    }
}
