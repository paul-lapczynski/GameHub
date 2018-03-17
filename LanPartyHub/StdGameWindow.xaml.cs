using LanPartyHub.Managers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;

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

        public StdGameWindow(MainWindow main, string gameId)
        {
            _main = main;
            stdGameManager = new StdGameManager(gameId);

            InitializeComponent();
            Title = ApplicationManager.Settings.Games.First( game => game.GameId == gameId ).Name;
            StartGameButton.Content = "Start " + Title;
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

        private void StartStdGame(object sender, RoutedEventArgs e)
        {
            stdgame = DOSBoxManager.StartDOSBox(stdGameManager.GetDOSBoxOptions(this));
        }
    }
}
