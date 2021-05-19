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

    /// <summary>
    /// Game renderer.
    /// </summary>
    public class GameRenderer : IGameRenderer
    {
        private DrawingGroup dg;
        private GameModel model;
        private GameLogic gdll;
        private FormattedText formattedText;
        private int score;
        private Character character;
        private Point scoreLocation = new Point(Config.Width / 2, 0);
        private Point healthLocation = new Point(Config.Width - 70, 0);
        private Point petrolLocation = new Point(Config.Width - 175, 0); // 150
        private Point moneyLocation = new Point(0, 0);

        private Point shopMessageLocation = new Point(485, Config.ButtonBgHeight - 65);
        private Point healthPriceShopTextLocation = new Point(700, Config.ButtonBgHeight - 50);
        private Point petrolPriceShopTextLocation = new Point(475, Config.ButtonBgHeight - 50);
        private Key lastKey = Key.Left;
        private int num = 1;
        private int dragonTickNum = 1;
        private bool b;
        private Dictionary<string, Brush> myBrushes = new Dictionary<string, Brush>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRenderer"/> class.
        /// </summary>
        /// <param name="model">model repo.</param>
        /// <param name="logic">game logic.</param>
        /// <param name="character">current character.</param>
        public GameRenderer(GameModel model, GameLogic logic, Character character)
        {
            this.model = model;
            this.gdll = logic;
            this.dg = new DrawingGroup();
            this.character = character;
        }

        private Brush BgBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.BackGround.bmp", true); }
        }

        private Brush CopperBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.copper.bmp", false); }
        }

        private Brush DiamondBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.diamond.bmp", false); }
        }

        private Brush DirtBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.dirt.bmp", false); }
        }

        private Brush GoldBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.gold.bmp", false); }
        }

        private Brush SilverBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.silver.bmp", false); }
        }

        private Brush StoneBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.stone.bmp", false); }
        }

        private Brush LadderBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.ladder.bmp", false); }
        }

        private Brush Frame1
        {
            get { return this.GetBrush("GameRendererDll.Images.frame1.png", true); }
        }

        private Brush Frame2
        {
            get { return this.GetBrush("GameRendererDll.Images.frame2.png", true); }
        }

        private Brush Frame3
        {
            get { return this.GetBrush("GameRendererDll.Images.frame3.png", true); }
        }

        private Brush Frame4
        {
            get { return this.GetBrush("GameRendererDll.Images.frame4.png", true); }
        }

        private Brush Frame5
        {
            get { return this.GetBrush("GameRendererDll.Images.frame5.png", true); }
        }

        private Brush Frame6
        {
            get { return this.GetBrush("GameRendererDll.Images.frame6.png", true); }
        }

        private Brush Frame7
        {
            get { return this.GetBrush("GameRendererDll.Images.frame7.png", true); }
        }

        private Brush Frame8
        {
            get { return this.GetBrush("GameRendererDll.Images.frame8.png", true); }
        }

        private Brush Frame9
        {
            get { return this.GetBrush("GameRendererDll.Images.frame9.png", true); }
        }

        private Brush Frame10
        {
            get { return this.GetBrush("GameRendererDll.Images.frame10.png", true); }
        }

        private Brush Frame11
        {
            get { return this.GetBrush("GameRendererDll.Images.frame11.png", true); }
        }

        private Brush Frame12
        {
            get { return this.GetBrush("GameRendererDll.Images.frame12.png", true); }
        }

        private Brush MountainBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.MountainOnSide.png", true); }
        }

        private Brush GroundBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.ground.bmp", false); }
        }

        private Brush LavaBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.lava.bmp", false); }
        }

        private Brush BuyButtonBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.shop button.bmp", false); }
        }

        private Brush SellButtonBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.sell button.bmp", false); }
        }

        private Brush EndGameButtonBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.Main menu.bmp", false); }
        }

        private Brush ShopBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.shopbg.bmp", false); }
        }

        private Brush EngGameLogoBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.gameover.bmp", false); }
        }

        private Brush GateBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.gate.bmp", false); }
        }

        private Brush MineGate1Brush
        {
            get { return this.GetBrush("GameRendererDll.Images.gatetop.bmp", false); }
        }

        private Brush MineGate2Brush
        {
            get { return this.GetBrush("GameRendererDll.Images.gatebottom.bmp", false); }
        }

        private Brush MinerRightBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.minerLeft.bmp", false); }
        }

        private Brush MinerLeftBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.minerRight.bmp", false); }
        }

        private Brush PetrolShopBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.petrolShop.png", false); }
        }

        private Brush PickaxeShopBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.PickaxeShop.png", false); }
        }

        private Brush SellShopBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.sellShop.png", false); }
        }

        private Brush HeartBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.heart.png", false); }
        }

        private Brush MuteBrush
        {
            get { return this.GetBrush("GameRendererDll.Images.mute.png", false); }
        }

        private Brush Torch100
        {
            get { return this.GetBrush("GameRendererDll.Images.100%torch.png", false); }
        }

        private Brush Torch80
        {
            get { return this.GetBrush("GameRendererDll.Images.80%torch.png", false); }
        }

        private Brush Torch60
        {
            get { return this.GetBrush("GameRendererDll.Images.60%torch.png", false); }
        }

        private Brush Torch40
        {
            get { return this.GetBrush("GameRendererDll.Images.40%torch.png", false); }
        }

        private Brush Torch20
        {
            get { return this.GetBrush("GameRendererDll.Images.20%torch.png", false); }
        }

        private Brush Torch10
        {
            get { return this.GetBrush("GameRendererDll.Images.10%torch.png", false); }
        }

        private Brush Dragon1Brush
        {
            get { return this.GetBrush("GameRendererDll.Images.Fram1Dragon.png", false); }
        }

        private Brush Dragon2Brush
        {
            get { return this.GetBrush("GameRendererDll.Images.Frame2Dragon.png", false); }
        }

        private Brush Dragon3Brush
        {
            get { return this.GetBrush("GameRendererDll.Images.Frame3Dragon.png", false); }
        }

        private Brush Dragon4Brush
        {
            get { return this.GetBrush("GameRendererDll.Images.Frame4Dragon.png", false); }
        }

        private Brush Dragon5Brush
        {
            get { return this.GetBrush("GameRendererDll.Images.Frame5Dragon.png", false); }
        }

        /// <summary>
        /// Create rectange here due to optimisation.
        /// </summary>
        /// <param name="oreX">x.</param>
        /// <param name="oreY">y.</param>
        /// <returns>rectangle.</returns>
        public RectangleGeometry RectangleG(double oreX, double oreY)
        {
            return new RectangleGeometry(new Rect(oreX, oreY, Config.OreWidth, Config.OreHeight));
        }

        /// <summary>
        /// Draw the models.
        /// </summary>
        /// <param name="ctx">DrawingContext.</param>
        /// <param name="mapID">which map.</param>
        /// <param name="intersectShop">which shop we are intersect.</param>
        /// <param name="k">which key was pressed.</param>
        /// <param name="time">time.</param>
        public void Draw(DrawingContext ctx, int mapID, string intersectShop, Key k, DateTime time)
        {
            Pen black = null;

            this.dg.Children.Clear();
            if (mapID == 1)
            {
                if (this.num == 1)
                {
                    this.character.Fuel--;
                    this.num++;
                }
                else if (this.num == 50)
                {
                    this.num = 1;
                }
                else
                {
                    this.num++;
                }

                Ore[,] oreMatrix = this.gdll.MapPart();
                GeometryDrawing background = new GeometryDrawing(
                    this.BgBrush,
                    new Pen(null, Config.BorderSize),
                    new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));

                GeometryDrawing scoreGround = new GeometryDrawing(
                    Brushes.Gray,
                    new Pen(null, Config.BorderSize),
                    new RectangleGeometry(new Rect(0, 0, Config.Width, 27)));

                this.dg.Children.Add(background);

                this.dg.Children.Add(scoreGround);

                this.DrawScoreText(ctx, mapID);

                for (int i = 0; i < oreMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < oreMatrix.GetLength(1); j++)
                    {
                        switch (oreMatrix[i, j].OreType)
                        {
                            case "air":
                                GeometryDrawing air = new GeometryDrawing(
                                    Config.AirBg,
                                    black,
                                    this.RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(air);
                                break;
                            case "dirt":
                                GeometryDrawing dirt = new GeometryDrawing(
                                    this.DirtBrush,
                                    black,
                                    this.RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(dirt);
                                break;
                            case "copper":
                                GeometryDrawing copper = new GeometryDrawing(
                                    this.CopperBrush,
                                    black,
                                    this.RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(copper);
                                break;
                            case "silver":
                                GeometryDrawing silver = new GeometryDrawing(
                                    this.SilverBrush,
                                    black,
                                    this.RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(silver);
                                break;
                            case "gold":
                                GeometryDrawing gold = new GeometryDrawing(
                                    this.GoldBrush,
                                    black,
                                    this.RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(gold);
                                break;
                            case "diamond":
                                GeometryDrawing diamond = new GeometryDrawing(
                                    this.DiamondBrush,
                                    black,
                                    this.RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(diamond);
                                break;
                            case "stone":
                                GeometryDrawing stone = new GeometryDrawing(
                                    this.StoneBrush,
                                    black,
                                    this.RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(stone);
                                break;
                            case "gate":
                                GeometryDrawing mapOneGate = new GeometryDrawing(
                                    this.GateBrush,
                                    black,
                                    this.RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(mapOneGate);
                                break;
                            case "ground2":
                                GeometryDrawing mineGround = new GeometryDrawing(
                                    this.StoneBrush,
                                    black,
                                    this.RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(mineGround);
                                break;
                            case "ladder":
                                GeometryDrawing ladder = new GeometryDrawing(
                                    this.LadderBrush,
                                    black,
                                    this.RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(ladder);
                                break;
                            case "lava":
                                GeometryDrawing lava = new GeometryDrawing(
                                    this.LavaBrush,
                                    black,
                                    this.RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(lava);
                                break;
                            case "gate2":
                                GeometryDrawing mineGatePart1 = new GeometryDrawing(
                                    this.MineGate1Brush,
                                    black,
                                    this.RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(mineGatePart1);
                                break;
                            case "gate3":
                                GeometryDrawing mineGatePart2 = new GeometryDrawing(
                                    this.MineGate2Brush,
                                    black,
                                    this.RectangleG(oreMatrix[i, j].Area.X, oreMatrix[i, j].Area.Y));
                                this.dg.Children.Add(mineGatePart2);
                                break;
                        }
                    }
                }

                if (k == Key.Left || (k == Key.Up && this.lastKey == Key.Left) || (k == Key.Down && this.lastKey == Key.Left) || (k == Key.Space && this.lastKey == Key.Left))
                {
                    GeometryDrawing miner = new GeometryDrawing(
                    this.MinerLeftBrush,
                    black,
                    new RectangleGeometry(this.character.Area));
                    this.dg.Children.Add(miner);
                    this.lastKey = Key.Left;
                }
                else if (k == Key.Right || (k == Key.Up && this.lastKey == Key.Right) || (k == Key.Down && this.lastKey == Key.Right) || (k == Key.Space && this.lastKey == Key.Right))
                {
                    GeometryDrawing miner = new GeometryDrawing(
                    this.MinerRightBrush,
                    black,
                    new RectangleGeometry(this.character.Area));
                    this.dg.Children.Add(miner);
                    this.lastKey = Key.Right;
                }
            }
            else if (mapID == 0)
            {
                GeometryDrawing scoreGround = new GeometryDrawing(
                    Brushes.Gray,
                    new Pen(null, Config.BorderSize),
                    new RectangleGeometry(new Rect(0, 0, Config.Width, 27)));

                #region min
                if (time.Minute == 0 || time.Minute == 12 || time.Minute == 24 || time.Minute == 36 || time.Minute == 48)
                {
                    GeometryDrawing background = new GeometryDrawing(
                   this.Frame1,
                   black,
                   new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                    this.dg.Children.Add(background);
                }
                else if (time.Minute == 1 || time.Minute == 13 || time.Minute == 25 || time.Minute == 37 || time.Minute == 49)
                {
                    GeometryDrawing background = new GeometryDrawing(
                   this.Frame2,
                   black,
                   new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                    this.dg.Children.Add(background);
                }
                else if (time.Minute == 2 || time.Minute == 14 || time.Minute == 26 || time.Minute == 38 || time.Minute == 50)
                {
                    GeometryDrawing background = new GeometryDrawing(
                   this.Frame3,
                   black,
                   new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                    this.dg.Children.Add(background);
                }
                else if (time.Minute == 3 || time.Minute == 15 || time.Minute == 27 || time.Minute == 39 || time.Minute == 51)
                {
                    GeometryDrawing background = new GeometryDrawing(
                   this.Frame4,
                   black,
                   new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                    this.dg.Children.Add(background);
                }
                else if (time.Minute == 4 || time.Minute == 16 || time.Minute == 28 || time.Minute == 40 || time.Minute == 52)
                {
                    GeometryDrawing background = new GeometryDrawing(
                   this.Frame5,
                   black,
                   new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                    this.dg.Children.Add(background);
                }
                else if (time.Minute == 5 || time.Minute == 17 || time.Minute == 29 || time.Minute == 41 || time.Minute == 53)
                {
                    GeometryDrawing background = new GeometryDrawing(
                   this.Frame6,
                   black,
                   new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                    this.dg.Children.Add(background);
                }
                else if (time.Minute == 6 || time.Minute == 18 || time.Minute == 30 || time.Minute == 42 || time.Minute == 54)
                {
                    GeometryDrawing background = new GeometryDrawing(
                   this.Frame7,
                   black,
                   new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                    this.dg.Children.Add(background);
                }
                else if (time.Minute == 7 || time.Minute == 19 || time.Minute == 31 || time.Minute == 43 || time.Minute == 55)
                {
                    GeometryDrawing background = new GeometryDrawing(
                   this.Frame8,
                   black,
                   new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                    this.dg.Children.Add(background);
                }
                else if (time.Minute == 8 || time.Minute == 20 || time.Minute == 32 || time.Minute == 44 || time.Minute == 56)
                {
                    GeometryDrawing background = new GeometryDrawing(
                   this.Frame9,
                   black,
                   new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                    this.dg.Children.Add(background);
                }
                else if (time.Minute == 9 || time.Minute == 21 || time.Minute == 33 || time.Minute == 45 || time.Minute == 57)
                {
                    GeometryDrawing background = new GeometryDrawing(
                   this.Frame10,
                   black,
                   new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                    this.dg.Children.Add(background);
                }
                else if (time.Minute == 10 || time.Minute == 22 || time.Minute == 34 || time.Minute == 46 || time.Minute == 58)
                {
                    GeometryDrawing background = new GeometryDrawing(
                   this.Frame11,
                   black,
                   new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                    this.dg.Children.Add(background);
                }
                else if (time.Minute == 11 || time.Minute == 23 || time.Minute == 35 || time.Minute == 47 || time.Minute == 59)
                {
                    GeometryDrawing background = new GeometryDrawing(
                   this.Frame12,
                   black,
                   new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                    this.dg.Children.Add(background);
                }

                this.dg.Children.Add(scoreGround);
                #endregion
                GeometryDrawing dragon = null;
                dragon = new GeometryDrawing(
                       Brushes.Transparent,
                       black,
                       new RectangleGeometry(this.model.DragonSymbol.Area));
                if ((this.model.DragonSymbol.Area.X + Config.DragonWidth) >= 0)
                {
                    if (this.dragonTickNum <= 5)
                    {
                        this.dragonTickNum++;

                        dragon = new GeometryDrawing(
                       this.Dragon1Brush,
                       black,
                       new RectangleGeometry(this.model.DragonSymbol.Area));
                        if (this.dragonTickNum == 5)
                        {
                            this.model.DragonSymbol.ChangeX(-10);
                        }
                    }
                    else if (this.dragonTickNum > 5 && this.dragonTickNum <= 10)
                    {
                        this.dragonTickNum++;
                        dragon = new GeometryDrawing(
                       this.Dragon2Brush,
                       black,
                       new RectangleGeometry(this.model.DragonSymbol.Area));
                        if (this.dragonTickNum == 10)
                        {
                            this.model.DragonSymbol.ChangeX(-10);
                        }
                    }
                    else if (this.dragonTickNum > 10 && this.dragonTickNum <= 15)
                    {
                        this.dragonTickNum++;
                        dragon = new GeometryDrawing(
                       this.Dragon3Brush,
                       black,
                       new RectangleGeometry(this.model.DragonSymbol.Area));
                        if (this.dragonTickNum == 15)
                        {
                            this.model.DragonSymbol.ChangeX(-10);
                        }
                    }
                    else if (this.dragonTickNum > 15 && this.dragonTickNum <= 20)
                    {
                        this.dragonTickNum++;
                        dragon = new GeometryDrawing(
                       this.Dragon4Brush,
                       black,
                       new RectangleGeometry(this.model.DragonSymbol.Area));
                        if (this.dragonTickNum == 20)
                        {
                            this.model.DragonSymbol.ChangeX(-10);
                        }
                    }
                    else if (this.dragonTickNum > 20 && this.dragonTickNum <= 25)
                    {
                        this.dragonTickNum++;
                        dragon = new GeometryDrawing(
                       this.Dragon5Brush,
                       black,
                       new RectangleGeometry(this.model.DragonSymbol.Area));
                        if (this.dragonTickNum == 25)
                        {
                            this.model.DragonSymbol.ChangeX(-10);
                            this.dragonTickNum = 1;
                        }
                    }
                }
                else
                {
                    this.model.DragonSymbol.SetXY(875, 75);
                }

                this.dg.Children.Add(dragon);
                GeometryDrawing mountain = new GeometryDrawing(
                   this.MountainBrush,
                   black,
                   new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));
                this.dg.Children.Add(mountain);

                GeometryDrawing gate = new GeometryDrawing(
                    Brushes.Transparent,
                    black,
                    new RectangleGeometry(this.model.Gate.Area));

                this.dg.Children.Add(gate);

                GeometryDrawing ground = new GeometryDrawing(
                    Brushes.Transparent,
                    black,
                    new RectangleGeometry(this.model.Ground.Area));

                GeometryDrawing pickaxShopHouse = new GeometryDrawing(
                    this.PickaxeShopBrush,
                    black,
                    new RectangleGeometry(this.model.PickaxShopHouse.Area));

                GeometryDrawing pickaxShop = new GeometryDrawing(
                    Config.PickaxShopBg,
                    black,
                    new RectangleGeometry(this.model.PickaxShop.Area));

                GeometryDrawing healthShopHouse = new GeometryDrawing(
                    this.PetrolShopBrush,
                    black,
                    new RectangleGeometry(this.model.PetrolAndHealthShopHouse.Area));

                GeometryDrawing healthShop = new GeometryDrawing(
                    Config.HealthShopBg,
                    black,
                    new RectangleGeometry(this.model.SellShop.Area));

                GeometryDrawing sellShopHouse = new GeometryDrawing(
                    this.SellShopBrush,
                    black,
                    new RectangleGeometry(this.model.SellShopHouse.Area));

                GeometryDrawing petrolShop = new GeometryDrawing(
                    Config.PetrolShopBg,
                    black,
                    new RectangleGeometry(this.model.PetrolAndHealthShop.Area));

                if (intersectShop == "sell")
                {
                    this.b = true;
                    GeometryDrawing buttonShop = new GeometryDrawing(
                    this.SellButtonBrush,
                    black,
                    new RectangleGeometry(this.model.ButtonShape.Area));

                    GeometryDrawing buttonBackg = new GeometryDrawing(
                    this.ShopBrush,
                    black,
                    new RectangleGeometry(this.model.ButtonBackground.Area));

                    this.dg.Children.Add(buttonBackg);
                    this.dg.Children.Add(buttonShop);
                }
                else if (intersectShop == "petrol;Health")
                {
                    this.b = true;

                    GeometryDrawing petrolButtonShop = new GeometryDrawing(
                    this.BuyButtonBrush,
                    black,
                    new RectangleGeometry(this.model.PetrolButtonShape.Area));

                    GeometryDrawing healthButtonShop = new GeometryDrawing(
                    this.BuyButtonBrush,
                    black,
                    new RectangleGeometry(this.model.HealthButtonShape.Area));

                    GeometryDrawing buttonBackg1 = new GeometryDrawing(
                    this.ShopBrush,
                    black,
                    new RectangleGeometry(this.model.ButtonBackground.Area));

                    this.dg.Children.Add(buttonBackg1);
                    this.dg.Children.Add(petrolButtonShop);
                    this.dg.Children.Add(healthButtonShop);
                }
                else if (intersectShop == "pickax")
                {
                    this.b = true;

                    GeometryDrawing buttonShop1 = new GeometryDrawing(
                    this.BuyButtonBrush,
                    black,
                    new RectangleGeometry(this.model.ButtonShape.Area));

                    GeometryDrawing buttonBackg2 = new GeometryDrawing(
                    this.ShopBrush,
                    black,
                    new RectangleGeometry(this.model.ButtonBackground.Area));

                    this.dg.Children.Add(buttonBackg2);
                    this.dg.Children.Add(buttonShop1);
                }
                else
                {
                    this.b = false;
                }

                this.dg.Children.Add(ground);
                this.dg.Children.Add(pickaxShop);
                this.dg.Children.Add(pickaxShopHouse);
                this.dg.Children.Add(healthShop);
                this.dg.Children.Add(healthShopHouse);
                this.dg.Children.Add(petrolShop);
                this.dg.Children.Add(sellShopHouse);

                if (k == Key.None || k == Key.Left || (k == Key.Up && this.lastKey == Key.Left) || (k == Key.Down && this.lastKey == Key.Left) || (k == Key.Space && this.lastKey == Key.Left))
                {
                    GeometryDrawing miner = new GeometryDrawing(
                    this.MinerLeftBrush,
                    black,
                    new RectangleGeometry(this.character.Area));
                    this.dg.Children.Add(miner);
                    this.lastKey = Key.Left;
                }
                else if (k == Key.Right || (k == Key.Up && this.lastKey == Key.Right) || (k == Key.Down && this.lastKey == Key.Right) || (k == Key.Space && this.lastKey == Key.Right))
                {
                    GeometryDrawing miner = new GeometryDrawing(
                    this.MinerRightBrush,
                    black,
                    new RectangleGeometry(this.character.Area));
                    this.dg.Children.Add(miner);
                    this.lastKey = Key.Right;
                }
            }
            else if (mapID == 2)
            {
                GeometryDrawing background = new GeometryDrawing(
                   this.EngGameLogoBrush,
                   null,
                   new RectangleGeometry(new Rect(0, 0, Config.Width, Config.Height)));

                GeometryDrawing endGameButton = new GeometryDrawing(
                    this.EndGameButtonBrush,
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
                GeometryDrawing heartSym = new GeometryDrawing(
                    this.HeartBrush,
                    black,
                    new RectangleGeometry(this.model.HeartSymbol.Area));

                GeometryDrawing torch100 = new GeometryDrawing(
                    this.Torch100,
                    black,
                    new RectangleGeometry(this.model.TorchSymbol.Area));

                GeometryDrawing mute = new GeometryDrawing(
                    this.MuteBrush,
                    black,
                    new RectangleGeometry(new Rect(150, 0, 25, 25)));

                this.dg.Children.Add(heartSym);
                this.dg.Children.Add(mute);
                this.dg.Children.Add(torch100);
                ctx.DrawDrawing(this.dg);

                this.DrawScoreText(ctx, mapID);
                this.DrawHealthText(ctx, mapID);
                this.DrawPetrolText(ctx, mapID);
                this.DrawMoneyText(ctx, mapID);
                this.NoMoneyText(ctx, this.b);
            }

            if (intersectShop == "sell" || intersectShop == "pickax")
            {
                this.ShopText(ctx, intersectShop);
                this.NoMoneyText(ctx, this.b);
            }
            else if (intersectShop == "petrol;Health")
            {
                this.HealthPriceShopText(ctx);
                this.NoMoneyText(ctx, this.b);
                this.PetrolPriceShopText(ctx);
            }
        }

        /// <summary>
        /// Draw score to window.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <param name="mapID">which map.</param>
        /// <returns>Score in text.</returns>
        public FormattedText DrawScoreText(DrawingContext ctx, int mapID)
        {
            SolidColorBrush color = Brushes.White;
            if (mapID == 0)
            {
                color = Brushes.White;
            }

            // Money: xy$       Score: xy       Petrol: xy      Healt: xy
            this.score = this.character.Score;
            Typeface font = new Typeface("Arial");

            this.formattedText = new FormattedText(
                    this.score.ToString(),
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    25,
                    color,
                    5);
            ctx.DrawText(this.formattedText, this.scoreLocation);

            return this.formattedText;
        }

        /// <summary>
        /// Draw health to window.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <param name="mapID">which map.</param>
        /// <returns>Health in text.</returns>
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
                    25,
                    Brushes.Red,
                    5);
            ctx.DrawText(this.formattedText, this.healthLocation);

            return this.formattedText;
        }

        /// <summary>
        /// Draw petrol to window.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <param name="mapID">which map.</param>
        /// <returns>Petrol in text.</returns>
        public FormattedText DrawPetrolText(DrawingContext ctx, int mapID)
        {
            SolidColorBrush color = Brushes.White;
            if (mapID == 0)
            {
                color = Brushes.White;
            }

            int petrol = this.character.Fuel;

            Typeface font = new Typeface("Arial");

            this.formattedText = new FormattedText(
                    petrol.ToString(),
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    25,
                    color,
                    5);
            ctx.DrawText(this.formattedText, this.petrolLocation);

            return this.formattedText;
        }

        /// <summary>
        /// Draw money to window.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <param name="mapID">which map.</param>
        /// <returns>Money in text.</returns>
        public FormattedText DrawMoneyText(DrawingContext ctx, int mapID)
        {
            SolidColorBrush color = Brushes.White;
            if (mapID == 0)
            {
                color = Brushes.White;
            }

            string money = this.character.Money + " $";

            Typeface font = new Typeface("Arial");

            this.formattedText = new FormattedText(
                    money,
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    25,
                    color,
                    5);
            ctx.DrawText(this.formattedText, this.moneyLocation);

            return this.formattedText;
        }

        /// <summary>
        /// Draw shop text to window.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <param name="intersectShop">which shop.</param>
        /// <returns>Shop in text.</returns>
        public FormattedText ShopText(DrawingContext ctx, string intersectShop)
        {
            string shopMessage = " ";
            int money = 0;
            if (intersectShop == "sell")
            {
                if (this.character.Backpack == null)
                {
                    shopMessage = "Your bag is empty!";
                }
                else
                {
                    if (this.character.Backpack.Count == 0)
                    {
                        shopMessage = $"Your bag is empty!";
                    }
                    else
                    {
                        foreach (var item in this.character.Backpack)
                        {
                            money += item.Value;
                        }

                        shopMessage = $"You have {this.character.Backpack.Count} pieces of ore in your bag! \nTotal value: {money}$ ";
                    }
                }
            }
            else if (intersectShop == "pickax")
            {
                if (this.character.PickAxLevel == 4)
                {
                    shopMessage = $"Congratulations, you have the\nstrongest pickaxe!";
                }
                else
                {
                    shopMessage = $"Your current pickaxe strength: {this.character.PickAxLevel}! \nNext pickaxe strength:{this.character.PickAxLevel + 1}!\nPickaxe price: {(this.character.PickAxLevel + 1) * 100}";
                }
            }

            Typeface font = new Typeface("Arial");

            this.formattedText = new FormattedText(
                    shopMessage,
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    20,
                    Brushes.White,
                    1);
            ctx.DrawText(this.formattedText, this.shopMessageLocation);

            return this.formattedText;
        }

        /// <summary>
        /// Draw health price to window.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <returns>Health price in text.</returns>
        public FormattedText HealthPriceShopText(DrawingContext ctx)
        {
            string priceShopMessage = "Life: 2$/point";

            Typeface font = new Typeface("Arial");

            this.formattedText = new FormattedText(
                    priceShopMessage,
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    20,
                    Brushes.White,
                    1);
            ctx.DrawText(this.formattedText, this.healthPriceShopTextLocation); // todo elhelyezés

            return this.formattedText;
        }

        /// <summary>
        /// Draw petrol price to window.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <returns>Petrol price in text.</returns>
        public FormattedText PetrolPriceShopText(DrawingContext ctx)
        {
            string priceShopMessage = "Petroleum: 2$/L";

            Typeface font = new Typeface("Arial");

            this.formattedText = new FormattedText(
                    priceShopMessage,
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    20,
                    Brushes.White,
                    1);
            ctx.DrawText(this.formattedText, this.petrolPriceShopTextLocation); // todo elhelyezés

            return this.formattedText;
        }

        /// <summary>
        /// Draw end game text to window.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <returns>End game in text.</returns>
        public FormattedText EndGameText(DrawingContext ctx)
        {
            Typeface font = new Typeface("Arial");

            Point p = new Point(310, 280);

            this.formattedText = new FormattedText(
                    $"Your score: {this.character.Score}",
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    font,
                    40,
                    Brushes.White,
                    1);
            ctx.DrawText(this.formattedText, p);

            return this.formattedText;
        }

        /// <summary>
        /// No Money Text to window.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <param name="b">bool.</param>
        /// <returns>End game in text.</returns>
        public FormattedText NoMoneyText(DrawingContext ctx, bool b)
        {
            string message = this.gdll.ShopMoneyMessage(b);
            Typeface font = new Typeface("Arial");
            Point p = new Point(Config.Width / 8, Config.Height / 3);

            if (b == true)
            {
                if (message == null)
                {
                    message = " ";
                }

                this.formattedText = new FormattedText(
                        message,
                        System.Globalization.CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        font,
                        20,
                        Brushes.White,
                        1);
                ctx.DrawText(this.formattedText, p);

                message = null;
                return this.formattedText;
            }
            else
            {
                message = " ";
                this.formattedText = new FormattedText(
                        message,
                        System.Globalization.CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        font,
                        20,
                        Brushes.White,
                        1);
                ctx.DrawText(this.formattedText, p);
                return this.formattedText;
            }
        }

        /// <summary>
        /// Individual Brush maker.
        /// </summary>
        /// <param name="fname">image path.</param>
        /// <param name="isTiled">to bg true, else false.</param>
        /// <returns>Individual brush.</returns>
        private Brush GetBrush(string fname, bool isTiled)
        {
            if (!this.myBrushes.ContainsKey(fname))
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

                this.myBrushes[fname] = ib;
            }

            return this.myBrushes[fname];
        }
    }
}