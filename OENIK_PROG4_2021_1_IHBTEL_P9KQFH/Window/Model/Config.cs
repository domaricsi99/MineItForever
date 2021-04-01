namespace GameWindow.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media;

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
        public static int MinerHeight = 60;
    }
}
