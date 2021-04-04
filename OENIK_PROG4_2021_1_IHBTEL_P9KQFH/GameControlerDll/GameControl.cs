// <copyright file="GameControl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameControlerDll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using GameData;
    using GameLogicDll;
    using GameModelDll;
    using GameRendererDll;
    using GameRepository;

    /// <summary>
    /// Control the game.
    /// </summary>
    public class GameControl : FrameworkElement
    {
        private GameModel model;
        private GameLogic logic;
        private GameDataBase db;
        private GameRenderer renderer;
        private DispatcherTimer tickTimer;
        Repo repo;

        public GameControl()
        {
            Loaded += PongControl_Loaded;
        }

        private void PongControl_Loaded(object sender, RoutedEventArgs e)
        {
            db = new GameDataBase();
            model = new GameModel();
            repo = new Repo(db);
            logic = new GameLogic(model, repo);
            renderer = new GameRenderer(model,logic);
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
