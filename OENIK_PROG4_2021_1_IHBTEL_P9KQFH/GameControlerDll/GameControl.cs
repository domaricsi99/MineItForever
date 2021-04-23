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
    using GameLogicDll;
    using GameModelDll;
    using GameRendererDll;
    using GameRepository;

    /// <summary>
    /// Control the game.
    /// </summary>
    public class GameControl : FrameworkElement, IGameControl
    {
        private GameModel model;
        private GameLogic logic;

        // private GameDataBase db;
        private GameRenderer renderer;
        private DispatcherTimer tickTimer;

        public string NameW { get; set; }

        // private Repo repo;
        private int mapID = 0; // !!!!
        private string intersectShop;

        private MapRepository mapRepo;
        private CharacterRepository charRepo;
        public Character character;

        public GameControl()
        {
            this.Loaded += this.GameControl_Loaded;
        }

        public void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.model = new GameModel();
            this.mapRepo = new MapRepository();
            this.charRepo = new CharacterRepository();
            this.character = this.charRepo.StartGame();
            //this.charRepo.SaveGame(this.character);
            this.logic = new GameLogic(this.model, this.mapRepo, this.charRepo, this.character);
            this.renderer = new GameRenderer(this.model, this.logic, this.character);
            Window win = Window.GetWindow(this);

            if (win != null)
            {
                this.tickTimer = new DispatcherTimer();
                this.tickTimer.Interval = TimeSpan.FromMilliseconds(20);
                this.tickTimer.Tick += this.TickTimer_Tick;
                this.tickTimer.Start();
                win.KeyDown += this.Win_KeyDown;
            }

            this.logic.RefreshScreen += (obj, args) => this.InvalidateVisual();
            this.logic.ChangeScreen += (obj, args) =>
            {
                this.mapID = 1;
                this.logic.setCharPosition(60, 120 - Config.MinerHeight);
            };

            this.MouseLeftButtonDown += this.GameControl_MouseLeftButtonDown;

            this.MouseRightButtonDown += this.GameControl_MouseRightButtonDown;

            this.logic.ShopScreen += (obj, args) =>
            {
                this.mapID = 0;
            };

            this.logic.BackToMapOneScreen += (obj, args) =>
            {
                this.mapID = 0;
                this.logic.setCharPosition(Config.Width - 60, Config.Height - this.model.Ground.Area.Height - Config.MinerHeight); // lehet szar
            };

            this.logic.EndGameEvent += (obj, args) =>
            {
                this.mapID = 2;
            };

            this.logic.BackToMainMenuEvent += (obj, args) =>
            {
                win.Close();
                //TODO StartPage meghívás
            };

            this.InvalidateVisual();
        }

        private void GameControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point cursorPos = e.GetPosition(this);
            this.logic.PickUpLadder(cursorPos, this.mapID);
        }

        private void GameControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point cursorPos = e.GetPosition(this);
            this.logic.DropLadder(cursorPos, this.mapID);
            this.logic.Click(cursorPos, this.mapID, this.intersectShop);
        }

        public void TickTimer_Tick(object sender, EventArgs e)
        {
            this.logic.Fall(this.mapID);
            this.logic.MineGate(this.mapID);
            this.intersectShop = this.logic.IntersectsWithShop();
            this.logic.EndGame();
        }

        public void Win_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left: this.logic.MoveCharacter(Direction.Left, this.mapID); break;
                case Key.Right: this.logic.MoveCharacter(Direction.Right, this.mapID); break;
                case Key.Space: this.logic.MoveCharacter(Direction.Up, this.mapID); break;
                case Key.Down: this.logic.MoveCharacter(Direction.Down, this.mapID); break;
                case Key.Up: this.logic.MoveCharacter(Direction.Climb, this.mapID); break;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.renderer != null)
            {
                this.renderer.Draw(drawingContext, this.mapID, this.intersectShop);
            }
        }
    }
}
