// <copyright file="NewGameDataWindow.xaml.cs" company="PlaceholderCompany">
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
    /// Interaction logic for NewGameDataWindow.xaml.
    /// </summary>
    public partial class NewGameDataWindow : Window
    {
        private CharacterRepository repo = new CharacterRepository();

        /// <summary>
        /// Initializes a new instance of the <see cref="NewGameDataWindow"/> class.
        /// </summary>
        public NewGameDataWindow()
        {
            this.InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            MainWindow mw = new MainWindow();
            this.repo.NewCharacter(this.newName.Text);
            this.Hide();
            mw.ShowDialog();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            StartPage sp = new StartPage();
            this.Hide();
            sp.ShowDialog();
        }
    }
}
