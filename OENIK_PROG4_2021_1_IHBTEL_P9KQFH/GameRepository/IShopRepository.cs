// <copyright file="IShopRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GameModelDll;

    /// <summary>
    /// Shop repositroy interface.
    /// </summary>
    public interface IShopRepository
    {
        /// <summary>
        /// Pickax list.
        /// </summary>
        /// <returns>list pickax.</returns>
        public List<Pickax> PickaxList();
    }
}
