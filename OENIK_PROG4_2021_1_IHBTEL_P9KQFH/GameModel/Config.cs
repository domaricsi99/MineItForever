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
        /// <summary>
        /// Window width.
        /// </summary>
        public const double Width = 900;

        /// <summary>
        /// Window hegiht.
        /// </summary>
        public const double Height = 400;

        /// <summary>
        /// Window border size.
        /// </summary>
        public const int BorderSize = 4;

        /// <summary>
        /// Map delimeter.
        /// </summary>
        public const int MapDelimeter = 20;

        /// <summary>
        /// Miner width.
        /// </summary>
        public const int MinerWidth = 30;

        /// <summary>
        /// Miner height.
        /// </summary>
        public const int MinerHeight = 40;

        /// <summary>
        /// Ground width.
        /// </summary>
        public const int GroundWidth = (int)Config.Width;

        /// <summary>
        /// Ground height.
        /// </summary>
        public const int GroundHeight = 50; // 30

        /// <summary>
        /// Ore width.
        /// </summary>
        public const int OreWidth = 45;

        /// <summary>
        /// Ore height.
        /// </summary>
        public const int OreHeight = 45;

        /// <summary>
        /// Ore x coordinate.
        /// </summary>
        public const int OreX = 0;

        /// <summary>
        /// Ore y coordinate.
        /// </summary>
        public const int OreY = 0;

        /// <summary>
        /// Gatw width.
        /// </summary>
        public const int GateWidth = 10; // 30

        /// <summary>
        /// Gate height.
        /// </summary>
        public const int GateHeight = 60;

        /// <summary>
        /// Building width.
        /// </summary>
        public const int BuildingWidth = 100;

        /// <summary>
        /// Building height.
        /// </summary>
        public const int BuildingHeight = 120;

        /// <summary>
        /// Shop intersect width.
        /// </summary>
        public const int ShopWidth = 40;

        /// <summary>
        /// Shop intersect height.
        /// </summary>
        public const int ShopHeight = 40;

        /// <summary>
        /// Button width.
        /// </summary>
        public const int ButtonWidth = 90;

        /// <summary>
        /// Button height.
        /// </summary>
        public const int ButtonHeight = 45;

        /// <summary>
        /// Button background width.
        /// </summary>
        public const int ButtonBgWidth = 400;

        /// <summary>
        /// Button background height.
        /// </summary>
        public const int ButtonBgHeight = 150;

        /// <summary>
        /// End game button width.
        /// </summary>
        public const int EndGameButtonWidth = 180;

        /// <summary>
        /// End game button height.
        /// </summary>
        public const int EndGameButtonHeight = 50;

        /// <summary>
        /// Heart symbol height and width.
        /// </summary>
        public const int HeartWH = 25;

        /// <summary>
        /// Dragon width.
        /// </summary>
        public const int DragonWidth = 150;

        /// <summary>
        /// Dragon height.
        /// </summary>
        public const int DragonHeight = 100;

        /// <summary>
        /// Air background.
        /// </summary>
        public static readonly Brush AirBg = Brushes.Transparent;

        /// <summary>
        /// Pickax shop background.
        /// </summary>
        public static readonly Brush PickaxShopBg = Brushes.Transparent;

        /// <summary>
        /// Health shop background.
        /// </summary>
        public static readonly Brush HealthShopBg = Brushes.Transparent;

        /// <summary>
        /// Petrol shop background.
        /// </summary>
        public static readonly Brush PetrolShopBg = Brushes.Transparent;
    }
}
