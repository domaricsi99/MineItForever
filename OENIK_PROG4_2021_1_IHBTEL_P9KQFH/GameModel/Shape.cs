using System;
using System.Windows;

namespace GameModelDll
{
    public class Shape
    {
        public Rect area;

        public Rect Area
        {
            get { return area; }
        }

        public void SetXY(double x, double y)
        {
            area.X = x;
            area.Y = y;
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
