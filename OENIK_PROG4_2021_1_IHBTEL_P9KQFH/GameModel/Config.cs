// <copyright file="Config.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameModelDll
{
    using System.Windows.Media;

    /// <summary>
    /// Fix modell size.
    /// </summary>
    public static class Config
    {
        public static double Width = 900;
        public static double Height = 400;
        public static int BorderSize = 4;
        public static int MapDelimeter = 20;

        public static int MinerWidth = 30;
        public static int MinerHeight = 40;

        public static int GroundWidth = (int)Config.Width;
        public static int GroundHeight = 30;

        public static Brush airBg = Brushes.Transparent;
        public static int oreWidth = 45;
        public static int oreHeight = 45;
        public static int oreX = 0;
        public static int oreY = 0;

        public static int GateWidth = 30;
        public static int GateHeight = 60;

        public static int BuildingWidth = 90;
        public static int BuildingHeight = 110;

        public static Brush PickaxShopBg = Brushes.Transparent;
        public static Brush HealthShopBg = Brushes.Transparent;
        public static Brush PetrolShopBg = Brushes.Transparent;
        public static int ShopWidth = 45;
        public static int ShopHeight = 45;

        public static int ButtonWidth = 90;
        public static int ButtonHeight = 45;
        public static int ButtonBgWidth = 400;
        public static int ButtonBgHeight = 150;

        public static int EndGameButtonWidth = 180;
        public static int EndGameButtonHeight = 50;
    }
}
