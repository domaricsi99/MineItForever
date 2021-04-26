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
        FormattedText formattedText;
        int score;
        Character character;
        Point scoreLocation = new Point(Config.Width / 2, 0);
        Point healthLocation = new Point(Config.Width - 35, 0);
        Point petrolLocation = new Point(Config.Width - 150, 0);
        Point moneyLocation = new Point(0, 0);

        Point shopMessageLocation = new Point(550, Config.ButtonBgHeight - 50);
        Point HealthPriceShopTextLocation = new Point(700, Config.ButtonBgHeight - 50);
        Point PetrolPriceShopTextLocation = new Point(475, Config.ButtonBgHeight - 50);
        Point ReturnShopTextLocation = new Point(Config.Width / 2, Config.Height / 2);
        int returnMessageTimeCounter = 0;
        int num = 1;
        Dictionary<string, Brush> myBrushes = new Dictionary<string, Brush>();

        public GameRenderer(GameModel model, GameLogic logic, Character character)
        {
            this.model = model;
            this.gdll = logic;
            this.map = this.gdll.DrawMap();
            this.dg = new DrawingGroup();
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
                    ib.Viewport = new Rect(0, 0, Config.Width, Config.Height);
                    ib.ViewportUnits = BrushMappingMode.Absolute;
                }

                myBrushes[fname] = ib;
            }

            return myBrushes[fname];
        }

        Brush bgBrush { get { return GetBrush("GameRendererDll.Images.BackGround.bmp", true); } }

        Brush copperBrush { get { return GetBrush("GameRendererDll.Images.copper.bmp", false); } }

        Brush diamondBrush { get { return GetBrush("GameRendererDll.Images.diamond.bmp", false); } }

        Brush dirtBrush { get { return GetBrush("GameRendererDll.Images.dirt.bmp", false); } }

        Brush goldBrush { get { return GetBrush("GameRendererDll.Images.gold.bmp", false); } }

        Brush silverBrush { get { return GetBrush("GameRendererDll.Images.silver.bmp", false); } }

        Brush stoneBrush { get { return GetBrush("GameRendererDll.Images.stone.bmp", false); } }

        Brush ladderBrush { get { return GetBrush("GameRendererDll.Images.ladder.bmp", false); } }

        Brush shopWindowBackgroundBrush { get { return GetBrush("GameRendererDll.Images.shopWindowBackground.bmp", true); } }

        Brush groundBrush { get { return GetBrush("GameRendererDll.Images.ground.bmp", false); } }

        Brush lavaBrush { get { return GetBrush("GameRendererDll.Images.lava.bmp", false); } }

        Brush BuyButtonBrush { get { return GetBrush("GameRendererDll.Images.shop button.bmp", false); } }

        Brush SellButtonBrush { get { return GetBrush("GameRendererDll.Images.sell button.bmp", false); } }

        Brush EndGameButtonBrush { get { return GetBrush("GameRendererDll.Images.Main menu.bmp", false); } }

        public void Draw(DrawingContext ctx, int mapID, string intersectShop) // todo mindent kirajzolni, flappybol atirni
        {
            Pen black = null;//new Pen(Brushes.Black, 1);

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
                this.DrawScoreText(ctx, mapID);

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
                                GeometryDrawing Dirt = new GeometryDrawing(dirtBrush,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(Dirt);
                                break;
                            case "copper":
                                GeometryDrawing Copper = new GeometryDrawing(copperBrush,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(Copper);
                                break;
                            case "silver":
                                GeometryDrawing Silver = new GeometryDrawing(silverBrush,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(Silver);
                                break;
                            case "gold":
                                GeometryDrawing Gold = new GeometryDrawing(goldBrush,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(Gold);
                                break;
                            case "diamond":
                                GeometryDrawing Diamond = new GeometryDrawing(diamondBrush,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(Diamond);
                                break;
                            case "stone":
                                GeometryDrawing Stone = new GeometryDrawing(stoneBrush,
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
                                GeometryDrawing mineGround = new GeometryDrawing(stoneBrush,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(mineGround);
                                break;
                            case "ladder":
                                GeometryDrawing ladder = new GeometryDrawing(ladderBrush,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(ladder);
                                break;
                            case "lava":
                                GeometryDrawing lava = new GeometryDrawing(lavaBrush,
                                black,
                                RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(lava);
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
                   shopWindowBackgroundBrush,
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

                GeometryDrawing ground = new GeometryDrawing(
                    groundBrush,
                    black,
                    new RectangleGeometry(this.model.Ground.Area));

                GeometryDrawing pickaxShop = new GeometryDrawing(
                    Config.PickaxShopBg,
                    black,
                    new RectangleGeometry(this.model.PickaxShop.Area));

                GeometryDrawing healthShop = new GeometryDrawing(
                    Config.HealthShopBg,
                    black,
                    new RectangleGeometry(this.model.SellShop.Area));

                GeometryDrawing petrolShop = new GeometryDrawing(
                    Config.PetrolShopBg,
                    black,
                    new RectangleGeometry(this.model.PetrolAndHealthShop.Area));

                if (intersectShop == "sell")
                {
                    GeometryDrawing ButtonShop = new GeometryDrawing(
                    SellButtonBrush,
                    black,
                    new RectangleGeometry(this.model.ButtonShape.Area));

                    GeometryDrawing ButtonBackg = new GeometryDrawing(
                    Config.ButtonBg,
                    black,
                    new RectangleGeometry(this.model.ButtonBackground.Area));

                    this.dg.Children.Add(ButtonBackg);
                    this.dg.Children.Add(ButtonShop);
                }
                else if (intersectShop == "petrol;Health")
                {
                    GeometryDrawing PetrolButtonShop = new GeometryDrawing(
                    BuyButtonBrush,
                    black,
                    new RectangleGeometry(this.model.PetrolButtonShape.Area));

                    GeometryDrawing HealthButtonShop = new GeometryDrawing(
                    BuyButtonBrush,
                    black,
                    new RectangleGeometry(this.model.HealthButtonShape.Area));

                    GeometryDrawing ButtonBackg = new GeometryDrawing(
                    Brushes.White,
                    black,
                    new RectangleGeometry(this.model.ButtonBackground.Area));

                    this.dg.Children.Add(ButtonBackg);
                    this.dg.Children.Add(PetrolButtonShop);
                    this.dg.Children.Add(HealthButtonShop);
                }
                else if (intersectShop == "pickax")
                {
                    GeometryDrawing ButtonShop = new GeometryDrawing(
                    BuyButtonBrush,
                    black,
                    new RectangleGeometry(this.model.ButtonShape.Area));

                    GeometryDrawing ButtonBackg = new GeometryDrawing(
                    Config.ButtonBg,
                    black,
                    new RectangleGeometry(this.model.ButtonBackground.Area));

                    this.dg.Children.Add(ButtonBackg);
                    this.dg.Children.Add(ButtonShop);
                }

                this.dg.Children.Add(ground);
                this.dg.Children.Add(pickaxShop);
                this.dg.Children.Add(healthShop);
                this.dg.Children.Add(petrolShop);
                this.dg.Children.Add(miner);
            }
            else if (mapID == 2)
            {
                GeometryDrawing background = new GeometryDrawing(
                   Config.bgBrush,
                   black,
                   new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));

                GeometryDrawing endGameButton = new GeometryDrawing(
                    EndGameButtonBrush,
                    black,
                    new RectangleGeometry(this.model.EndGameButton.Area));

                this.dg.Children.Add(background);
                this.dg.Children.Add(endGameButton);
            }

            ctx.DrawDrawing(this.dg);
            if (mapID == 2)
            {
                this.EndGameText(ctx);
            }
            else
            {
                this.DrawScoreText(ctx, mapID);
                this.DrawHealthText(ctx, mapID);
                this.DrawPetrolText(ctx, mapID);
                this.DrawMoneyText(ctx, mapID);
            }

            if (intersectShop == "sell" || intersectShop == "pickax")
            {
                this.ShopText(ctx, intersectShop);
            }
            else if (intersectShop == "petrol;Health")
            {
                this.HealthPriceShopText(ctx);
                this.PetrolPriceShopText(ctx);
            }
        }

        public FormattedText DrawScoreText(DrawingContext ctx, int mapID)
        {
            SolidColorBrush color = Brushes.White;
            if (mapID == 0)
            {
                color = Brushes.Black;
            }

            this.score = this.character.Score;

            Typeface font = new Typeface("Arial");

            this.formattedText = new FormattedText(
                    score.ToString(),
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    20,
                    color, 1);
            ctx.DrawText(this.formattedText, this.scoreLocation);

            return this.formattedText;
        }

        public FormattedText DrawHealthText(DrawingContext ctx, int mapID)
        {
            int health = this.character.Health;
            if (this.character.Health <= 0)
            {
                health = 0;
            }

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

        public FormattedText DrawPetrolText(DrawingContext ctx, int mapID)
        {
            SolidColorBrush color = Brushes.White;
            if (mapID == 0)
            {
                color = Brushes.Black;
            }

            int petrol = this.character.Fuel;

            Typeface font = new Typeface("Arial");

            this.formattedText = new FormattedText(
                    petrol.ToString(),
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    20,
                    color, 1);
            ctx.DrawText(this.formattedText, this.petrolLocation);

            return this.formattedText;
        }

        public FormattedText DrawMoneyText(DrawingContext ctx, int mapID)
        {
            SolidColorBrush color = Brushes.White;
            if (mapID == 0)
            {
                color = Brushes.Black;
            }

            string money = this.character.Money + " $";

            Typeface font = new Typeface("Arial");

            this.formattedText = new FormattedText(
                    money,
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    20,
                    color, 1);
            ctx.DrawText(this.formattedText, this.moneyLocation);

            return this.formattedText;
        }

        public FormattedText ShopText(DrawingContext ctx, string intersectShop)
        {
            string shopMessage = "";
            int money = 0;
            if (intersectShop == "sell")
            {
                if (this.character.Backpack == null)
                {
                    shopMessage = "A táskád üres!";
                }
                else
                {
                    foreach (var item in this.character.Backpack)
                    {
                        money += item.Value;
                    }

                    shopMessage = $"A táskádban {this.character.Backpack.Count} darab érc van! \nÖssz értéke: {money}$ ";
                }
            }
            else if (intersectShop == "pickax")
            {
                if (this.character.PickAxLevel == 4)
                {
                    shopMessage = $"Gratulálok, a legerõsebb csákánnyal rendelkezel!";
                }
                else
                {
                    shopMessage = $"Jelenlegi csákányod ereje: {this.character.PickAxLevel}! \nKövetkezõ csákány ereje:{this.character.PickAxLevel + 1}!";
                }
            }

            Typeface font = new Typeface("Arial");

            this.formattedText = new FormattedText(
                    shopMessage,
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    20,
                    Brushes.Black, 1);
            ctx.DrawText(this.formattedText, this.shopMessageLocation);

            return this.formattedText;
        }

        public FormattedText HealthPriceShopText(DrawingContext ctx)
        {
            string priceShopMessage = "Life: 2$/piece";

            Typeface font = new Typeface("Arial");

            this.formattedText = new FormattedText(
                    priceShopMessage,
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    20,
                    Brushes.Black, 1);
            ctx.DrawText(this.formattedText, this.HealthPriceShopTextLocation); // todo elhelyezés

            return this.formattedText;
        }

        public FormattedText PetrolPriceShopText(DrawingContext ctx)
        {
            string priceShopMessage = "Petrol: 2$/liter";

            Typeface font = new Typeface("Arial");

            this.formattedText = new FormattedText(
                    priceShopMessage,
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    20,
                    Brushes.Black, 1);
            ctx.DrawText(this.formattedText, this.PetrolPriceShopTextLocation); // todo elhelyezés

            return this.formattedText;
        }

        public FormattedText ReturnShopText(DrawingContext ctx)
        {
            string message = gdll.message;

            Typeface font = new Typeface("Arial");

            this.formattedText = new FormattedText(
                    message,
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    20,
                    Brushes.Gray, 1);
            ctx.DrawText(this.formattedText, this.ReturnShopTextLocation); // todo elhelyezés

            return this.formattedText;
        }

        public FormattedText EndGameText(DrawingContext ctx)
        {
            int petrol = this.character.Fuel;

            Typeface font = new Typeface("Arial");

            Point p = new Point(255, 200);

            this.formattedText = new FormattedText(
                    $"GAME OVER\nYour score: {this.character.Score}",
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    40,
                    Brushes.HotPink, 1);
            ctx.DrawText(this.formattedText, p);

            return this.formattedText;
        }
    }
}