// <copyright file="Shape.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModelDll
{
    using System;
    using System.Windows;

    /// <summary>
    /// Shape model.
    /// </summary>
    public class Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public Rect area;

        /// <summary>
        /// Initializes a new instance of the <see cref="Shape"/> class.
        /// </summary>
        public Shape()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shape"/> class.
        /// </summary>
        /// <param name="x">x coordinate.</param>
        /// <param name="y">y coordinate.</param>
        /// <param name="w">width.</param>
        /// <param name="h">height.</param>
        public Shape(double x, double y, double w, double h)
        {
            this.area.X = x;
            this.area.Y = y;
            this.area.Width = w;
            this.area.Height = h;
        }

        /// <summary>
        /// Gets or sets area.
        /// </summary>
        public Rect Area
        {
            get { return this.area; }
            set { this.area = value; }
        }

        /// <summary>
        /// Set x and y coordinate.
        /// </summary>
        /// <param name="x">x coordinate.</param>
        /// <param name="y">y coordinate.</param>
        public void SetXY(double x, double y)
        {
            this.area.X = x;
            this.area.Y = y;
        }

        /// <summary>
        /// Change x coordinate.
        /// </summary>
        /// <param name="dif">difference.</param>
        public void ChangeX(double dif)
        {
            this.area.X += dif;
        }

        /// <summary>
        /// Change y coordinate.
        /// </summary>
        /// <param name="dif">difference.</param>
        public void ChangeY(double dif)
        {
            this.area.Y += dif;
        }
    }
}
