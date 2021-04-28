// <copyright file="IMapRepository.cs" company="PlaceholderCompany">
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
    /// Map repository interface.
    /// </summary>
    public interface IMapRepository
    {
        /// <summary>
        /// Strinng ore list.
        /// </summary>
        /// <param name="character">Current character.</param>
        /// <returns>List Ore.</returns>
        public List<Ore> StringToOreList(Character character);

        /// <summary>
        /// Make air.
        /// </summary>
        /// <returns>Air.</returns>
        public Ore MakeAir();

        /// <summary>
        /// Make ladder.
        /// </summary>
        /// <returns>Ladder.</returns>
        public Ore MakeLadder();

        /// <summary>
        /// Make random Map.
        /// </summary>
        /// <returns>Random map.</returns>
        List<string> RandomMap();
    }
}
