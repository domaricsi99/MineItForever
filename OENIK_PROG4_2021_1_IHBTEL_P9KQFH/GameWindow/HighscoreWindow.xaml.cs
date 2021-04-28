// <copyright file="HighscoreWindow.xaml.cs" company="PlaceholderCompany">
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
    using GameModelDll;
    using GameRepository;

    /// <summary>
    /// Interaction logic for HighscoreWindow.xaml.
    /// </summary>
    public partial class HighscoreWindow : Window
    {
        private CharacterRepository repo = new CharacterRepository();

        /// <summary>
        /// Initializes a new instance of the <see cref="HighscoreWindow"/> class.
        /// </summary>
        public HighscoreWindow()
        {
            this.AllChar = this.repo.LoadHighscore();
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets all character.
        /// </summary>
        public List<Character> AllChar { get; }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            StartPage sp = new StartPage();
            this.Hide();
            sp.ShowDialog();
        }
    }
}
