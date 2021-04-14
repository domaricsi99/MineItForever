using System;
using System.Windows.Media;

namespace GameModelDll
{
    public static class Config
    {
        public static double Width = 900;
        public static double Height = 400;
        public static int BorderSize = 4;

        public static Brush bgBrush = Brushes.White;
        public static Brush BorderBrush = Brushes.DarkGray;

        public static Brush MinerBgBrush = Brushes.Black;
        public static Brush MinerLineBrush = Brushes.Blue;

        public static int MinerWidth = 30;
        public static int MinerHeight = 45;

        public static Brush BgGroundBrush = Brushes.RosyBrown;
        public static Brush GroundLineBrush = Brushes.DarkGreen;
        public static int GroundWidth = (int)Config.Width; // 700
        public static int GroundHeight = 30;

        public static Brush airBg = Brushes.Transparent;
        public static Brush dirtBg = Brushes.Brown;
        public static Brush copperBg = Brushes.Orange;
        public static Brush silverBg = Brushes.Silver;
        public static Brush goldBg = Brushes.Gold;
        public static Brush diamondBg = Brushes.LightCyan;
        public static Brush stoneBg = Brushes.DarkGray;

        public static Brush oreLine = Brushes.Black;
        public static int oreWidth = 45;
        public static int oreHeight = 45;
        public static int oreX = 0;
        public static int oreY = 0;

        public static Brush GateBg = Brushes.Pink;
        public static Brush MapThreetoOneGateBg = Brushes.Green;
        public static Brush MapTwoToOneGateBg = Brushes.Red;
        public static int GateWidth = 30;
        public static int GateHeight = 60;

        public static Brush PickaxShopHouseBg = Brushes.DarkBlue;
        public static Brush HealthShopHouseBg = Brushes.DarkCyan;
        public static Brush PetrolShopHouseBg = Brushes.DarkGoldenrod;
        public static int BuildingWidth = 90;
        public static int BuildingHeight = 110;

        public static Brush PickaxShopBg = Brushes.Red;
        public static Brush HealthShopBg = Brushes.Orange;
        public static Brush PetrolShopBg = Brushes.PaleGreen;
        public static int ShopWidth = 45;
        public static int ShopHeight = 45;

        public static Brush ButtonBg = Brushes.Transparent;
        public static int ButtonWidth = 90;
        public static int ButtonHeight = 60;
    }
}
