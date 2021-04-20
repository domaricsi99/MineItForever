// <copyright file="GameRenderer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameRendererDll
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using GameLogicDll;
    using GameModelDll;

    public class GameRenderer : IGameRenderer
    {
        GameModel model;
        GameLogic gdll;
        DrawingGroup dg;
        Ore[,] map;
        StackPanel sp;
        FormattedText formattedText;
        int score;
        Character character;
        Point scoreLocation = new Point(Config.Width / 2, 0);
        Point healthLocation = new Point(Config.Width - 35, 0);
        Point petrolLocation = new Point(Config.Width - 150, 0);
        int num = 1;
        Dictionary<string, Brush> myBrushes = new Dictionary<string, Brush>();

        public GameRenderer(GameModel model, GameLogic logic, Character character)
        {
            this.model = model;
            this.gdll = logic;
            this.map = this.gdll.DrawMap();
            this.dg = new DrawingGroup();
            this.sp = new StackPanel();
            this.character = character;
        }

        public RectangleGeometry RectangleG(double oreX, double oreY)
        {
            return new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight));
        }

        Brush GetBrush(string fname, bool isTiled)
        {
            if (!myBrushes.ContainsKey(fname))
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream(fname);
                bmp.EndInit();
                ImageBrush ib = new ImageBrush(bmp);
                if (isTiled)
                {
                    ib.TileMode = TileMode.Tile;
                    ib.Viewport = new Rect(0, 0, Config.Width,Config.Height);
                    ib.ViewportUnits = BrushMappingMode.Absolute;
                }

                myBrushes[fname] = ib;
            }

            return myBrushes[fname];
        }

        Brush bgBrush { get { return GetBrush("GameRendererDll.Images.BackGround.bmp", true); } }

        public void Draw(DrawingContext ctx, int mapID) // todo mindent kirajzolni, flappybol atirni
        {
            Pen black = new Pen(Brushes.Black, 1);

            this.dg.Children.Clear();
            if (mapID == 1) // MINE
            {
                if (this.num == 1)
                {
                    this.character.Fuel--;
                    this.num++;
                }
                else if (this.num == 50) // TODO beallit
                {
                    this.num = 1;
                }
                else
                {
                    this.num++;
                }

                Ore[,] oreMatrix = this.gdll.MapPart();
                GeometryDrawing background = new GeometryDrawing(
                    bgBrush,
                    new Pen(Config.BorderBrush, Config.BorderSize),
                    new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                this.dg.Children.Add(background);
                this.DrawScoreText(ctx);

                for (int i = 0; i < oreMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < oreMatrix.GetLength(1); j++)
                    {
                        switch (oreMatrix[i, j].OreType)
                        {
                            case "air":
                                GeometryDrawing Air = new GeometryDrawing(Config.airBg,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(Air);
                                break;
                            case "dirt":
                                GeometryDrawing Dirt = new GeometryDrawing(Config.dirtBg,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(Dirt);
                                break;
                            case "copper":
                                GeometryDrawing Copper = new GeometryDrawing(Config.copperBg,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(Copper);
                                break;
                            case "silver":
                                GeometryDrawing Silver = new GeometryDrawing(Config.silverBg,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(Silver);
                                break;
                            case "gold":
                                GeometryDrawing Gold = new GeometryDrawing(Config.goldBg,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(Gold);
                                break;
                            case "diamond":
                                GeometryDrawing Diamond = new GeometryDrawing(Config.diamondBg,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(Diamond);
                                break;
                            case "stone":
                                GeometryDrawing Stone = new GeometryDrawing(Config.stoneBg,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(Stone);
                                break;
                            case "gate":
                                GeometryDrawing mapOneGate = new GeometryDrawing(Config.MapTwoToOneGateBg,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(mapOneGate);
                                break;
                            case "ground2":
                                GeometryDrawing mineGround = new GeometryDrawing(Brushes.DarkOliveGreen,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(mineGround);
                                break;
                            case "ladder":
                                GeometryDrawing ladder = new GeometryDrawing(Config.LadderBg,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(ladder);
                                break;
                        }
                    }
                }

                GeometryDrawing miner = new GeometryDrawing(
                    Config.MinerBgBrush,
                    black,
                    new RectangleGeometry(character.Area));

                this.dg.Children.Add(miner);
            }
            else if (mapID == 0) // SHOP AND GATE TO MINE
            {
                GeometryDrawing background = new GeometryDrawing(
                   Config.bgBrush,
                   black,
                   new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                this.dg.Children.Add(background);

                GeometryDrawing gate = new GeometryDrawing(Config.GateBg,
                   black,
                   new RectangleGeometry(this.model.Gate.Area));

                this.dg.Children.Add(gate);

                GeometryDrawing miner = new GeometryDrawing(
                    Config.MinerBgBrush,
                    black,
                    new RectangleGeometry(this.character.Area));

                GeometryDrawing pickaxShopHouse = new GeometryDrawing(
                    Config.PickaxShopHouseBg,
                    black,
                    new RectangleGeometry(this.model.PickaxShopHouse.Area)
                    );

                GeometryDrawing healthShopHouse = new GeometryDrawing(
                    Config.HealthShopHouseBg,
                    black,
                    new RectangleGeometry(this.model.HealthShopHouse.Area)
                    );

                GeometryDrawing petrolShopHouse = new GeometryDrawing(
                    Config.PetrolShopHouseBg,
                    black,
                    new RectangleGeometry(this.model.PetrolShopHouse.Area)
                    );

                GeometryDrawing ground = new GeometryDrawing(
                    Config.BgGroundBrush,
                    black,
                    new RectangleGeometry(this.model.Ground.Area));

                GeometryDrawing pickaxShop = new GeometryDrawing(
                    Config.PickaxShopBg,
                    black,
                    new RectangleGeometry(this.model.PickaxShop.Area)
                    );

                GeometryDrawing healthShop = new GeometryDrawing(
                    Config.HealthShopBg,
                    black,
                    new RectangleGeometry(this.model.HealthShop.Area)
                    );

                GeometryDrawing petrolShop = new GeometryDrawing(
                    Config.PetrolShopBg,
                    black,
                    new RectangleGeometry(this.model.PetrolShop.Area)
                    );
                this.dg.Children.Add(pickaxShopHouse);
                this.dg.Children.Add(healthShopHouse);
                this.dg.Children.Add(petrolShopHouse);
                this.dg.Children.Add(ground);
                this.dg.Children.Add(pickaxShop);
                this.dg.Children.Add(healthShop);
                this.dg.Children.Add(petrolShop);
                this.dg.Children.Add(miner);
            }

            ctx.DrawDrawing(this.dg);
            this.DrawScoreText(ctx);
            this.DrawHealthText(ctx);
            this.DrawPetrolText(ctx);
        }

        private FormattedText DrawScoreText(DrawingContext ctx)
        {
            this.score = this.character.Score;

            Typeface font = new Typeface("Arial");

            this.formattedText = new FormattedText(
                    score.ToString(),
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    20,
                    Brushes.Gray, 1);
            ctx.DrawText(this.formattedText, this.scoreLocation);

            return this.formattedText;
        }

        private FormattedText DrawHealthText(DrawingContext ctx)
        {
            int health = this.character.Health;

            Typeface font = new Typeface("Arial");

            this.formattedText = new FormattedText(
                    health.ToString(),
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    20,
                    Brushes.Red, 1);
            ctx.DrawText(this.formattedText, this.healthLocation);

            return this.formattedText;
        }

        private FormattedText DrawPetrolText(DrawingContext ctx)
        {
            int petrol = this.character.Fuel;

            Typeface font = new Typeface("Arial");

            this.formattedText = new FormattedText(
                    petrol.ToString(),
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    20,
                    Brushes.Gray, 1);
            ctx.DrawText(this.formattedText, this.petrolLocation);

            return this.formattedText;
        }
    }
}