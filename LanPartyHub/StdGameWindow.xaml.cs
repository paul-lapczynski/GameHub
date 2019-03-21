using LanPartyHub.Managers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LanPartyHub
{
    /// <summary>
    /// Interaction logic for StdGameWindow.xaml
    /// </summary>
    public partial class StdGameWindow : Window
    {
        MainWindow _main;
        StdGameManager stdGameManager;
        Process stdgame;
        private string _gameid;

        public StdGameWindow(MainWindow main, string gameId)
        {
            _main = main;
            _gameid = gameId;
            stdGameManager = new StdGameManager(gameId);

            InitializeComponent();
            Title = GameManager.Settings.Games.First( game => game.GameId == gameId ).Name;
            StartGameText.Text = Title;
            Closed += StdGameWindow_Closed;
        }

        private void StdGameWindow_Closed(object sender, EventArgs e)
        {
            if (stdgame != null && !stdgame.HasExited)
            {
                stdgame.Kill();
            }
        }

        private void Unload(object sender, RoutedEventArgs e)
        {
            _main.Show();
        }

        private void GameImage_Loaded(object sender, RoutedEventArgs e)
        {
            // Create a new BitmapImage.
            var image = GameManager.Settings.Games.First(game => game.GameId == _gameid).ImagePath;
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(image);
            b.EndInit();
            GameImage.Source = b;
        }

        private void StartStdGame(object sender, RoutedEventArgs e)
        {
            stdgame = DOSBoxManager.StartDOSBox(stdGameManager.GetDOSBoxOptions(this));
        }

        private void GameConfig_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //var Window = new GameConfigWindow(this, _gameid);
            var Window = new GameConfigv2();
            Window.Owner = Application.Current.MainWindow;
            Window.Show();
            Hide();
        }

        private void GameConfig_MouseEnter(object sender, MouseEventArgs e)
        {
            GameConfig.Spin = true;
            GameConfig.SpinDuration = 5;
        }

        private void GameConfig_MouseLeave(object sender, MouseEventArgs e)
        {
            GameConfig.Spin = false;
        }
    }
}
