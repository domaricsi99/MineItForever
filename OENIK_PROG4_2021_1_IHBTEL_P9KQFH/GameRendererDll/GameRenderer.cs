using GameModelDll;
using System;
using System.Windows;
using System.Windows.Media;
using GameLogicDll;

namespace GameRendererDll
{
    public class GameRenderer
    {
        GameModel model;
        GameLogic gdll;

        public GameRenderer(GameModel model, GameLogic logic)
        {
            this.model = model;
            this.gdll = logic;
        }

        public void Draw(DrawingContext ctx) // todo mindent kirajzolni, flappybol atirni
        {
            DrawingGroup dg = new DrawingGroup();
            Ore[,] map = gdll.DrawMap();
            int oreX = 1;
            int oreY = 50;

            GeometryDrawing background = new GeometryDrawing(Config.bgBrush,
                new Pen(Config.BorderBrush, Config.BorderSize),
                new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));


            //GeometryDrawing miner = new GeometryDrawing(Config.MinerBgBrush,
            //    new Pen(Config.MinerBgBrush, 1),
            //    new RectangleGeometry(model.Miner.Area));

            //GeometryDrawing Ground = new GeometryDrawing(Config.BgGroundBrush,
            //    new Pen(Config.BgGroundBrush, 1),
            //    new RectangleGeometry(model.Ground.Area));

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    switch (map[i,j].OreType)
                    {
                        case "air":
                            GeometryDrawing Air = new GeometryDrawing(Config.airBg,
                            new Pen(Config.airBg, 1),
                            new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
                            dg.Children.Add(Air);
                            break;
                        case "dirt":
                            GeometryDrawing Dirt = new GeometryDrawing(Config.dirtBg,
                            new Pen(Config.dirtBg, 1),
                            new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
                            dg.Children.Add(Dirt);
                            break;
                        case "copper":
                            GeometryDrawing Copper = new GeometryDrawing(Config.copperBg,
                            new Pen(Config.copperBg, 1),
                            new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
                            dg.Children.Add(Copper);
                            break;
                        case "silver":
                            GeometryDrawing Silver = new GeometryDrawing(Config.silverBg,
                            new Pen(Config.silverBg, 1),
                            new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
                            dg.Children.Add(Silver);
                            break;
                        case "gold":
                            GeometryDrawing Gold = new GeometryDrawing(Config.goldBg,
                            new Pen(Config.goldBg, 1),
                            new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
                            dg.Children.Add(Gold);
                            break;
                        case "diamond":
                            GeometryDrawing Diamond = new GeometryDrawing(Config.diamondBg,
                            new Pen(Config.diamondBg, 1),
                            new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
                            dg.Children.Add(Diamond);
                            break;
                    }
                    oreX = oreX + 45;
                }
                oreY = oreY + 45;
            }

            dg.Children.Add(background);
            //dg.Children.Add(miner);
            //dg.Children.Add(Ground);

            ctx.DrawDrawing(dg);
        }

    }
}
