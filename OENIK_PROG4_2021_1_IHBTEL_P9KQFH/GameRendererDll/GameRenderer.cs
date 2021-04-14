// <copyright file="GameRenderer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameRendererDll
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using GameLogicDll;
    using GameModelDll;

    public class GameRenderer : IGameRenderer
    {
        GameModel model;
        GameLogic gdll;
        DrawingGroup dg;
        Ore[,] map;
        StackPanel sp;

        public GameRenderer(GameModel model, GameLogic logic)
        {
            this.model = model;
            this.gdll = logic;
            this.map = this.gdll.DrawMap();
            this.dg = new DrawingGroup();
            this.sp = new StackPanel();
        }

        public RectangleGeometry RectangleG(double oreX, double oreY)
        {
            return new RectangleGeometry(new Rect(oreX, oreY, Config.oreWidth, Config.oreHeight));
        }

        public void Draw(DrawingContext ctx, int mapID) // todo mindent kirajzolni, flappybol atirni
        {
            Pen black = new Pen(Brushes.Black, 1);

            this.dg.Children.Clear();
            if (mapID == 1) // MINE
            {
                Ore[,] oreMatrix = this.gdll.MapPart();
                GeometryDrawing background = new GeometryDrawing(
                    Config.bgBrush,
                    new Pen(Config.BorderBrush, Config.BorderSize),
                    new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                this.dg.Children.Add(background);
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
                        }
                    }
                }

                GeometryDrawing mapOneGate = new GeometryDrawing(Config.MapTwoToOneGateBg,
                   black,
                   new RectangleGeometry(this.model.MapTwoToOneGate.Area));

                GeometryDrawing miner = new GeometryDrawing(
                    Config.MinerBgBrush,
                    black,
                    new RectangleGeometry(this.model.Miner.Area));

                this.dg.Children.Add(mapOneGate);
                this.dg.Children.Add(miner);
            }
            else if (mapID == 0) // SHOP AND GATE TO MINE
            {
                GeometryDrawing background = new GeometryDrawing(
                   Config.bgBrush,
                   black,
                   new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                this.dg.Children.Add(background);
                //StackPanel sp = new StackPanel();
                //if (gdll.IntersectsWithShop())
                //{
                //    Button button = new Button()
                //    {
                //        Height = 250,
                //        Width = 900,
                //        Background = Brushes.Black,
                //        Content = gdll.SavedGame(model.Miner),
                //    };
                //    sp.Children.Add(button);

                //    Grid.SetRow(button, 250);
                //    Grid.SetColumn(button, 250);
                //}

                GeometryDrawing gate = new GeometryDrawing(Config.GateBg,
                   black,
                   new RectangleGeometry(this.model.Gate.Area));

                this.dg.Children.Add(gate);

                GeometryDrawing miner = new GeometryDrawing(
                    Config.MinerBgBrush,
                    black,
                    new RectangleGeometry(this.model.Miner.Area));

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
            else if (mapID == 4) // START PAGE --> LOAD SAVE 
            {
                GeometryDrawing background = new GeometryDrawing(
                    Config.bgBrush,
                    black,
                    new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));

                //GeometryDrawing loadBackground = new GeometryDrawing(
                //    Config.ButtonBg,
                //    black,
                //    new RectangleGeometry(this.model.SaveButtonRectangle.Area));

                //GeometryDrawing saveBackground = new GeometryDrawing(
                //    Config.ButtonBg,
                //    black,
                //    new RectangleGeometry(this.model.SaveButtonRectangle.Area));

                Button loadButton = new Button()
                {
                    Height = Config.ButtonHeight,
                    Width = Config.ButtonWidth,
                    Content = this.gdll.LoadGame(),
                    BorderBrush = Brushes.Black,
                    Background = Brushes.Red,
                    Margin = new Thickness(10,10,10,10),
                    SnapsToDevicePixels = true,
                    Visibility = Visibility.Visible,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                };

                Button saveButton = new Button()
                {
                    Content = this.gdll.SaveGame(),
                    BorderBrush = Brushes.Black,
                    Height = Config.ButtonHeight,
                    Width = Config.ButtonWidth,
                    Background = Brushes.Blue,
                };

                

                this.dg.Children.Add(background);
                //this.dg.Children.Add(loadBackground);
                //this.dg.Children.Add(saveBackground);
                this.sp.Children.Add(loadButton);
                this.sp.Children.Add(saveButton);
            }
            
            ctx.DrawDrawing(this.dg);
        }
    }
}