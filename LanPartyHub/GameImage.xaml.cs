using LanPartyHub.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LanPartyHub
{
    /// <summary>
    /// Interaction logic for GameImage.xaml
    /// </summary>
    public partial class GameImage : UserControl
    {
        private Game _game;

        public GameImage()
        {
            InitializeComponent();
        }

        public void Init(Game game)
        {
            _game = game;       
        }

        public string GameId
        {
            get
            {
                return _game.GameId;
            }
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            // Create a new BitmapImage.
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri( _game.ImagePath );
            b.EndInit();
            GameIcon.Source = b;
        }
    }
}
