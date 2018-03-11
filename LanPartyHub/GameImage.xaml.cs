﻿using LanPartyHub.Models;
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
            var uriSource = new Uri($"/LanPartyHub;component/{game.ImagePath}", UriKind.Relative);
            GameIcon.Stretch = Stretch.UniformToFill;
            GameIcon.Source = new BitmapImage(uriSource);
        }

        public int GameId
        {
            get
            {
                return _game.GameId;
            }
        }
    }
}
