// <copyright file="GameRenderer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameRendererDll
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using GameLogicDll;
    using GameModelDll;

    public class GameRenderer : IGameRenderer
    {

        GameModel model;
        GameLogic gdll;
        DrawingGroup dg;
        Ore[,] map;

        public GameRenderer(GameModel model, GameLogic logic)
        {
            this.model = model;
            this.gdll = logic;
            this.map = this.gdll.DrawMap();
            this.dg = new DrawingGroup();
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

        public RectangleGeometry RectangleG(double oreX, double oreY)
        {
            return new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight));
        }

        public void Draw(DrawingContext ctx, int mapID) // todo mindent kirajzolni, flappybol atirni
        {
            Pen black = new Pen(Brushes.Black, 1);
            this.dg.Children.Clear();
            if (mapID % 2 == 1)
            {
                Ore[,] cica = this.gdll.MapPart();
                GeometryDrawing background = new GeometryDrawing(
                    Config.bgBrush,
                    new Pen(Config.BorderBrush, Config.BorderSize),
                    new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                this.dg.Children.Add(background);
                for (int i = 0; i < cica.GetLength(0); i++)
                {
                    for (int j = 0; j < cica.GetLength(1); j++)
                    {
                        switch (cica[i, j].OreType)
                        {
                            case "air":
                                GeometryDrawing Air = new GeometryDrawing(Config.airBg,
                                black,
                                RectangleG(cica[i, j].Area.X, cica[i, j].Area.Y));
                                dg.Children.Add(Air);
                                break;
                            case "dirt":
                                GeometryDrawing Dirt = new GeometryDrawing(Config.dirtBg,
                                black,
                                RectangleG(cica[i, j].Area.X, cica[i, j].Area.Y));
                                dg.Children.Add(Dirt);
                                break;
                            case "copper":
                                GeometryDrawing Copper = new GeometryDrawing(Config.copperBg,
                                black,
                                RectangleG(cica[i, j].Area.X, cica[i, j].Area.Y));
                                dg.Children.Add(Copper);
                                break;
                            case "silver":
                                GeometryDrawing Silver = new GeometryDrawing(Config.silverBg,
                                black,
                                RectangleG(cica[i, j].Area.X, cica[i, j].Area.Y));
                                dg.Children.Add(Silver);
                                break;
                            case "gold":
                                GeometryDrawing Gold = new GeometryDrawing(Config.goldBg,
                                black,
                                RectangleG(cica[i, j].Area.X, cica[i, j].Area.Y));
                                dg.Children.Add(Gold);
                                break;
                            case "diamond":
                                GeometryDrawing Diamond = new GeometryDrawing(Config.diamondBg,
                                black,
                                RectangleG(cica[i, j].Area.X, cica[i, j].Area.Y));
                                dg.Children.Add(Diamond);
                                break;
                        }
                    }
                }

                GeometryDrawing miner = new GeometryDrawing(
                    Config.MinerBgBrush,
                    black,
                    new RectangleGeometry(this.model.Miner.Area));
                dg.Children.Add(miner);
            }
            else
            {
                GeometryDrawing background = new GeometryDrawing(
                    Config.bgBrush,
                    black,
                    new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));

                GeometryDrawing gate = new GeometryDrawing(Config.GateBg,
                   black,
                   new RectangleGeometry(this.model.Gate.Area));

                dg.Children.Add(background);
                dg.Children.Add(gate);

                GeometryDrawing miner = new GeometryDrawing(
                    Config.MinerBgBrush,
                    black,
                    new RectangleGeometry(this.model.Miner.Area));

                GeometryDrawing ground = new GeometryDrawing(
                    Config.BgGroundBrush,
                    black,
                    new RectangleGeometry(this.model.Ground.Area));

                dg.Children.Add(miner);
                dg.Children.Add(ground);

            }

            ctx.DrawDrawing(dg);
        }
    }
}
