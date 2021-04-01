using GameModelDll;
using GameLogicDll;
using GameRendererDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Media;

namespace GameControlerDll
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
                tickTimer.Interval = TimeSpan.FromMilliseconds(20);
                tickTimer.Tick += TickTimer_Tick;
                tickTimer.Start();

                win.KeyDown += Win_KeyDown;
            }

            logic.RefreshScreen += (obj, args) => InvalidateVisual();
            InvalidateVisual();
        }

        private void TickTimer_Tick(object sender, EventArgs e)
        {
            logic.Fall();
        }

        private void Win_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left: logic.MoveCharacter(GameLogic.Direction.Left); break;
                case Key.Right: logic.MoveCharacter(GameLogic.Direction.Right); break;
                case Key.Space: logic.MoveCharacter(GameLogic.Direction.Up); break;
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
