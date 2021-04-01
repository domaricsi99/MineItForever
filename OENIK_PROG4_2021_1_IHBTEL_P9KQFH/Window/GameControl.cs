using GameWindow.Logic;
using GameWindow.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using GameWindow.Model;
using System.Windows.Input;
using System.Windows.Media;

namespace GameWindow
{
    public class GameControl : FrameworkElement
    {
        GameModel model;
        GameLogic logic;
        GameRenderer renderer;
        DispatcherTimer tickTimer;

        public GameControl()
        {
            Loaded += PongControl_Loaded;
        }

        private void PongControl_Loaded(object sender, RoutedEventArgs e)
        {
            model = new GameModel();
            logic = new GameLogic(model);
            renderer = new GameRenderer(model);
            Window win = Window.GetWindow(this);

            if (win != null)
            {
                tickTimer = new DispatcherTimer();
                tickTimer.Interval = TimeSpan.FromMilliseconds(40);
                tickTimer.Start();

                win.KeyDown += Win_KeyDown;
            }

            logic.RefreshScreen += (obj, args) => InvalidateVisual();
            InvalidateVisual();
        }

        private void Win_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left: logic.MoveCharacter(GameLogic.Direction.Left); break;
                case Key.Right: logic.MoveCharacter(GameLogic.Direction.Right); break;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (renderer != null)
            {
                renderer.Draw(drawingContext);
            }
        }
    }
}
