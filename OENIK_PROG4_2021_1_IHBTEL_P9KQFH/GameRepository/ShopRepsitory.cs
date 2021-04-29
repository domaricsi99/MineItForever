// <copyright file="ShopRepsitory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameRepository
{
    using System.Collections.Generic;
    using GameModelDll;

    /// <summary>
    /// Shop repository.
    /// </summary>
    public class ShopRepsitory : IShopRepository
    {
        /// <summary>
        /// Pickax list.
        /// </summary>
        /// <returns>list pickax.</returns>
        public List<Pickax> PickaxList()
        {
            List<Pickax> pickaxes = new List<Pickax>();

            pickaxes.Add(new Pickax()
            {
                Name = "cheap",
                Price = 0,
                Level = 0,
            });

            pickaxes.Add(new Pickax()
            {
                Name = "basic",
                Price = 100,
                Level = 1,
            });

            pickaxes.Add(new Pickax()
            {
                Name = "intermediate",
                Price = 200,
                Level = 2,
            });

            pickaxes.Add(new Pickax()
            {
                Name = "advanced",
                Price = 300,
                Level = 3,
            });

            pickaxes.Add(new Pickax()
            {
                Name = "professional",
                Price = 400,
                Level = 4,
            });

            return pickaxes;
        }
    }
}
