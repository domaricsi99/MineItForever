// <copyright file="Building.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModelDll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Building model.
    /// </summary>
    public class Building : Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Building"/> class.
        /// </summary>
        public Building()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building"/> class.
        /// </summary>
        /// <param name="x">x coordinate.</param>
        /// <param name="y">y coordinate.</param>
        /// <param name="w">width.</param>
        /// <param name="h">height.</param>
        public Building(double x, double y, double w, double h)
            : base(x, y, w, h)
        {
        }

        /// <summary>
        /// Gets or sets building name.
        /// </summary>
        public string Name { get; set; }
    }
}
