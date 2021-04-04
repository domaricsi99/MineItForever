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

        //public void Gdll_ChangeScreen(DrawingContext ctx)
        //{
        //    DrawingGroup dg = new DrawingGroup(); // Wasteful! TODO:
        //    Ore[,] map = this.gdll.DrawMap();
        //    int oreX = 0;
        //    int oreY = 0;

        //    GeometryDrawing background = new GeometryDrawing(
        //        Config.bgBrush,
        //        new Pen(Config.BorderBrush, Config.BorderSize),
        //        new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
        //    dg.Children.Add(background);
             
        //    for (int i = 0; i < map.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < map.GetLength(1); j++)
        //        {
        //            switch (map[i, j].OreType)
        //            {
        //                case "air":
        //                    GeometryDrawing Air = new GeometryDrawing(Config.airBg,
        //                    new Pen(Config.airBg, 1),
        //                    new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
        //                    dg.Children.Add(Air);
        //                    break;
        //                case "dirt":
        //                    GeometryDrawing Dirt = new GeometryDrawing(Config.dirtBg,
        //                    new Pen(Config.dirtBg, 1),
        //                    new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
        //                    dg.Children.Add(Dirt);
        //                    break;
        //                case "copper":
        //                    GeometryDrawing Copper = new GeometryDrawing(Config.copperBg,
        //                    new Pen(Config.copperBg, 1),
        //                    new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
        //                    dg.Children.Add(Copper);
        //                    break;
        //                case "silver":
        //                    GeometryDrawing Silver = new GeometryDrawing(Config.silverBg,
        //                    new Pen(Config.silverBg, 1),
        //                    new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
        //                    dg.Children.Add(Silver);
        //                    break;
        //                case "gold":
        //                    GeometryDrawing Gold = new GeometryDrawing(Config.goldBg,
        //                    new Pen(Config.goldBg, 1),
        //                    new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
        //                    dg.Children.Add(Gold);
        //                    break;
        //                case "diamond":
        //                    GeometryDrawing Diamond = new GeometryDrawing(Config.diamondBg,
        //                    new Pen(Config.diamondBg, 1),
        //                    new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
        //                    dg.Children.Add(Diamond);
        //                    break;
        //            }

        //            oreX += 45;
        //        }

        //        oreY += 45;
        //        oreX = 0;
        //    }
            
        //    ctx.DrawDrawing(dg);
        //}

        public void Draw(DrawingContext ctx) // todo mindent kirajzolni, flappybol atirni
        {


            DrawingGroup dg = new DrawingGroup(); // Wasteful! TODO:
            Ore[,] map = this.gdll.DrawMap();
            int oreX = 0;
            int oreY = 0;

            GeometryDrawing background = new GeometryDrawing(
                Config.bgBrush,
                new Pen(Config.BorderBrush, Config.BorderSize),
                new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
            GeometryDrawing gate = new GeometryDrawing(Config.GateBg,
               new Pen(Config.GateBg, 1),
               new RectangleGeometry(model.Gate.Area));

            dg.Children.Add(background);
            dg.Children.Add(gate);

            gdll.MineGate();

            //if (help == true)
            //{
            //    for (int i = 0; i < map.GetLength(0); i++)
            //    {
            //        for (int j = 0; j < map.GetLength(1); j++)
            //        {
            //            switch (map[i, j].OreType)
            //            {
            //                case "air":
            //                    GeometryDrawing Air = new GeometryDrawing(Config.airBg,
            //                    new Pen(Config.airBg, 1),
            //                    new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
            //                    dg.Children.Add(Air);
            //                    break;
            //                case "dirt":
            //                    GeometryDrawing Dirt = new GeometryDrawing(Config.dirtBg,
            //                    new Pen(Config.dirtBg, 1),
            //                    new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
            //                    dg.Children.Add(Dirt);
            //                    break;
            //                case "copper":
            //                    GeometryDrawing Copper = new GeometryDrawing(Config.copperBg,
            //                    new Pen(Config.copperBg, 1),
            //                    new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
            //                    dg.Children.Add(Copper);
            //                    break;
            //                case "silver":
            //                    GeometryDrawing Silver = new GeometryDrawing(Config.silverBg,
            //                    new Pen(Config.silverBg, 1),
            //                    new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
            //                    dg.Children.Add(Silver);
            //                    break;
            //                case "gold":
            //                    GeometryDrawing Gold = new GeometryDrawing(Config.goldBg,
            //                    new Pen(Config.goldBg, 1),
            //                    new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
            //                    dg.Children.Add(Gold);
            //                    break;
            //                case "diamond":
            //                    GeometryDrawing Diamond = new GeometryDrawing(Config.diamondBg,
            //                    new Pen(Config.diamondBg, 1),
            //                    new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight)));
            //                    dg.Children.Add(Diamond);
            //                    break;
            //            }

            //            oreX += 45;
            //        }

            //        oreY += 45;
            //        oreX = 0;
            //    }
            //}
            //else
            //{
                GeometryDrawing miner = new GeometryDrawing(Config.MinerBgBrush,
               new Pen(Config.MinerBgBrush, 1),
               new RectangleGeometry(model.Miner.Area));

                GeometryDrawing Ground = new GeometryDrawing(Config.BgGroundBrush,
                   new Pen(Config.BgGroundBrush, 1),
                   new RectangleGeometry(model.Ground.Area));

                dg.Children.Add(miner);
                dg.Children.Add(Ground);
            //}

            ctx.DrawDrawing(dg);
        }

    }
}
