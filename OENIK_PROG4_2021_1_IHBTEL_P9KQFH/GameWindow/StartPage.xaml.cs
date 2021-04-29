// <copyright file="StartPage.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace GameWindow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using GameRepository;

    /// <summary>
    /// Interaction logic for StartPage.xaml.
    /// </summary>
    public partial class StartPage : Window
    {
        private CharacterRepository characterRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartPage"/> class.
        /// </summary>
        public StartPage()
        {
            this.characterRepository = new CharacterRepository();
            this.InitializeComponent();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGameDataWindow newGameDataWindow = new NewGameDataWindow();
            this.Hide();
            newGameDataWindow.ShowDialog();
        }

        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {
            LoadGameDataWindow loadGameDataWindow = new LoadGameDataWindow();
            this.Hide();
            loadGameDataWindow.ShowDialog();
        }

        private void Highscore_Click(object sender, RoutedEventArgs e)
        {
            HighscoreWindow hsWindow = new HighscoreWindow();
            this.Hide();
            hsWindow.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }
    }
}
