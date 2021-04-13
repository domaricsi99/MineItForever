using GameModelDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRepository
{
    class ShopRepsitory : IShopRepository
    {
        public List<Pickax> PickaxList()
        {
            List<Pickax> pickaxes = new List<Pickax>();

            pickaxes.Add(new Pickax()
            {
                Name = "cheap",
                Price = 0,
                Strength = 0,
            });

            pickaxes.Add(new Pickax()
            {
                Name = "basic",
                Price = 10,
                Strength = 1,
            });

            pickaxes.Add(new Pickax()
            {
                Name = "intermediate",
                Price = 20,
                Strength = 2,
            });

            pickaxes.Add(new Pickax()
            {
                Name = "advanced",
                Price = 30,
                Strength = 3,
            });

            pickaxes.Add(new Pickax()
            {
                Name = "professional",
                Price = 40,
                Strength = 4,
            });

            return pickaxes;
        }
    }
}
