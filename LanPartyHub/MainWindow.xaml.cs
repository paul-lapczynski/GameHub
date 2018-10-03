using LanPartyHub.Enumerations.Game;
using LanPartyHub.Managers;
using LanPartyHub.Models;
using LanPartyHub.Utilities;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace LanPartyHub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            icGamesList.ItemsSource = ApplicationManager.Settings.Games.ToList();

            foreach (var process in Process.GetProcessesByName("dns-sd.exe"))
            {
                process.Kill();
            }
        }

        private void Doom2MouseDown(object sender, MouseButtonEventArgs e)
        {
            var doomWindow = new Doom2Window(this, (e.Source as GameImage).GameId);
            doomWindow.Owner = Application.Current.MainWindow;
            doomWindow.Show();
            Hide();
        }

        private void GameImageClick(object sender, MouseButtonEventArgs e)
        {
            var image = (GameImage)e.Source;
            var game = (Game)image.DataContext;
            // Standard Startup - uses StdGameWindow
            if (game.StartupType == eStartupType.Standard)
            {
                StdGame_MouseDown(sender, e);
            }
            // Custom Startup - uses different game window
            else if (game.StartupType == eStartupType.Custom)
            {
                Doom2MouseDown(sender, e);
            }
        }

        private void StdGame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var stdGameWindow = new StdGameWindow(this, (e.Source as GameImage).GameId);
            stdGameWindow.Owner = Application.Current.MainWindow;
            stdGameWindow.Show();
            Hide();
        }

        private void GameImage_Loaded(object sender, RoutedEventArgs e)
        {
            var image = (GameImage)e.Source;
            var game = (Game)image.DataContext;
            image.Init(game);
        }

        private void MainConfig_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var Window = new DOSBoxConfigWindow(this);
            Window.Owner = Application.Current.MainWindow;
            Window.Show();
            Hide();
        }

        private void MainConfig_MouseEnter(object sender, MouseEventArgs e)
        {
            MainConfig.Spin = true;
            MainConfig.SpinDuration = 5;
        }

        private void MainConfig_MouseLeave(object sender, MouseEventArgs e)
        {
            MainConfig.Spin = false;
        }
    }
}
