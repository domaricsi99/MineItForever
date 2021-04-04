using System;
using System.Windows.Media;

namespace GameModelDll
{
    public static class Config
    {
        public static double Width = 700;
        public static double Height = 300;
        public static int BorderSize = 4;

        public static Brush bgBrush = Brushes.White;
        public static Brush BorderBrush = Brushes.DarkGray;

        public static Brush MinerBgBrush = Brushes.Black;
        public static Brush MinerLineBrush = Brushes.Blue;

        public static int MinerWidth = 30;
        public static int MinerHeight = 45;

        public static Brush BgGroundBrush = Brushes.Brown;
        public static Brush GroundLineBrush = Brushes.DarkGreen;
        public static int GroundWidth = 600;// 700
        public static int GroundHeight = 30;

        public static Brush airBg = Brushes.Black;
        public static Brush dirtBg = Brushes.Brown;
        public static Brush copperBg = Brushes.Orange;
        public static Brush silverBg = Brushes.Silver;
        public static Brush goldBg = Brushes.Gold;
        public static Brush diamondBg = Brushes.LightCyan;

        public static Brush oreLine = Brushes.Black;
        public static int oreWidth = 45;
        public static int oreHeight = 45;
    }
}
