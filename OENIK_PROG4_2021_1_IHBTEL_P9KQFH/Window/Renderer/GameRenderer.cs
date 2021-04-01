using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using GameWindow.Model;

namespace GameWindow.Renderer
{
    class GameRenderer
    {
        GameModel model;

        public GameRenderer(GameModel model)
        {
            this.model = model;
        }

        public void Draw(DrawingContext ctx ) // todo mindent kirajzolni, flappybol atirni 
        {
            DrawingGroup dg = new DrawingGroup();

            GeometryDrawing background = new GeometryDrawing(Config.bgBrush,
                new Pen(Config.BorderBrush, Config.BorderSize),
                new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));

            GeometryDrawing miner = new GeometryDrawing(Config.MinerBgBrush,
                new Pen(Config.MinerBgBrush,1),
                new RectangleGeometry(model.Miner.Area));

            dg.Children.Add(background);
            dg.Children.Add(miner);

            ctx.DrawDrawing(dg);
        }

    }
}
