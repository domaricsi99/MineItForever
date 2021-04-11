using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GameRendererDll
{
    interface IGameRenderer
    {
        public RectangleGeometry RectangleG(double oreX, double oreY);

        public void Draw(DrawingContext ctx, int mapID);
    }
}
