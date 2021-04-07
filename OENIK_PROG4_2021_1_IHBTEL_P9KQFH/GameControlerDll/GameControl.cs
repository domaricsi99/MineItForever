﻿// <copyright file="GameControl.cs" company="PlaceholderCompany">
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

        // private GameDataBase db;
        private GameRenderer renderer;
        private DispatcherTimer tickTimer;

        // private Repo repo;
        private int mapID = 0;
        private MapRepository mapRepo;

        public GameControl()
        {
            this.Loaded += this.GameControl_Loaded;
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.model = new GameModel();
            this.mapRepo = new MapRepository();
            this.logic = new GameLogic(this.model, this.mapRepo);
            this.renderer = new GameRenderer(this.model, this.logic);
            Window win = Window.GetWindow(this);

            if (win != null)
            {
                this.tickTimer = new DispatcherTimer();
                this.tickTimer.Interval = TimeSpan.FromMilliseconds(20); // 20
                this.tickTimer.Tick += this.TickTimer_Tick;
                this.tickTimer.Start();

                win.KeyDown += this.Win_KeyDown;
            }

            this.logic.RefreshScreen += (obj, args) => this.InvalidateVisual();
            this.logic.ChangeScreen += (obj, args) =>
            {
                this.mapID = 1;
                this.logic.setCharPosition(0, 100 - Config.MinerHeight);
            };
            this.InvalidateVisual();
        }

        private void TickTimer_Tick(object sender, EventArgs e)
        {
            this.logic.Fall(mapID);
            this.logic.MineGate();
        }

        private void Win_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left: this.logic.MoveCharacter(GameLogic.Direction.Left); break;
                case Key.Right: this.logic.MoveCharacter(GameLogic.Direction.Right); break;
                case Key.Space: this.logic.MoveCharacter(GameLogic.Direction.Up); break;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.renderer != null)
            {
                this.renderer.Draw(drawingContext, this.mapID);
            }
        }
    }
}
