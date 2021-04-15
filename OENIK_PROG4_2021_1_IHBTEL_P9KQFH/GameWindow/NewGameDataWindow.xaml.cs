using GameModelDll;
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

namespace GameWindow
{
    /// <summary>
    /// Interaction logic for NewGameDataWindow.xaml
    /// </summary>
    public partial class NewGameDataWindow : Window
    {
        private Character Character;

        public Character character
        {
            get { return Character; }
            set { Character = value; }
        }

        public NewGameDataWindow()
        {
            character = new Character();
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            MainWindow mw = new MainWindow();
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
