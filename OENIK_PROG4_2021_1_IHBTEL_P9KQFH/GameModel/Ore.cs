// <copyright file="Ore.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModelDll
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Ore model.
    /// </summary>
    public class Ore : Shape
    {
        /// <summary>
        /// Gets or sets a value indicating whether hurt.
        /// </summary>
        public bool Hurt { get; set; }

        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets socer.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets ore type.
        /// </summary>
        public string OreType { get; set; }

        /// <summary>
        /// Gets or sets level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether can pass.
        /// </summary>
        public bool CanPass { get; set; }

        /// <summary>
        /// Gets or sets break level.
        /// </summary>
        public int BreakLevel { get; set; }
    }
}
