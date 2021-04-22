using GameModelDll;
using GameRepository;
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
    /// Interaction logic for HighscoreWindow.xaml
    /// </summary>
    public partial class HighscoreWindow : Window
    {
        public List<Character> AllChar { get; set; }

        CharacterRepository repo = new CharacterRepository();

        public HighscoreWindow()
        {
            this.AllChar = this.repo.LoadHighscore();
            this.InitializeComponent();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
