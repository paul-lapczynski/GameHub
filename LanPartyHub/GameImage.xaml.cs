using LanPartyHub.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(_game.ImagePath);
            image.EndInit();
            GameIcon.Source = image;
        }
    }
}
