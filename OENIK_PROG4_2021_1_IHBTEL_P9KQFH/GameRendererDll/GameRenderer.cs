using GameModelDll;
using System;
using System.Windows;
using System.Windows.Media;

namespace GameRendererDll
{
    public class GameRenderer
    {
        GameModel model;

        public GameRenderer(GameModel model)
        {
            this.model = model;
        }

        public void Draw(DrawingContext ctx) // todo mindent kirajzolni, flappybol atirni
        {
            DrawingGroup dg = new DrawingGroup();

            GeometryDrawing background = new GeometryDrawing(Config.bgBrush,
                new Pen(Config.BorderBrush, Config.BorderSize),
                new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));

            GeometryDrawing miner = new GeometryDrawing(Config.MinerBgBrush,
                new Pen(Config.MinerBgBrush, 1),
                new RectangleGeometry(model.Miner.Area));

            GeometryDrawing Ground = new GeometryDrawing(Config.BgGroundBrush,
                new Pen(Config.BgGroundBrush, 1),
                new RectangleGeometry(model.Ground.Area));

            dg.Children.Add(background);
            dg.Children.Add(miner);
            dg.Children.Add(Ground);

            ctx.DrawDrawing(dg);
        }

    }
}
