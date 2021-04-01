// <copyright file="Shape.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Model
{
    using System.Windows;

    abstract public class Shape
    {
        private Rect area;

        public Rect Area
        {
            get { return area; }
        }

        public void SetXY(double x, double y)
        {
            area.X = x;
            area.Y = y;
        }

        public Shape(double x, double y, double w, double h)
        {
            area = new Rect(x, y, w, h);
        }

        public void ChangeX(double dif)
        {
            area.X += dif;
        }

        public void ChangeY(double dif)
        {
            area.Y += dif;
        }
    }
}
