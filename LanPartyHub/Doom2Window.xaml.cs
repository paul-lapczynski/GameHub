using LanPartyHub.Managers;
using LanPartyHub.Models;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace LanPartyHub
{
    /// <summary>
    /// Interaction logic for Doom2Window.xaml
    /// </summary>
    public partial class Doom2Window : Window
    {
        MainWindow _main;
        DoomManager doomManager;
        Process doom;

        public Doom2Window(MainWindow main, string gameId)
        {
            _main = main;
            doomManager = new DoomManager(gameId);

            InitializeComponent();

            SetupCombos();
            Closed += Doom2Window_Closed;
        }

        private void Doom2Window_Closed(object sender, EventArgs e)
        {
            if (doom != null && !doom.HasExited)
            {
                doom.Kill();
            }
        }

        private void Unload(object sender, RoutedEventArgs e)
        {
            _main.Show();
        }

        private void EpisodeDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LevelDropdown.ItemsSource = doomManager.GetLevelView(int.Parse(((KeyValue)EpisodeDropdown.SelectedItem).Key));
        }

        private void StartDoom(object sender, RoutedEventArgs e)
        {
            doom = DOSBoxManager.StartDOSBox(doomManager.GetDOSBoxOptions(this));
        }

        private void SetupCombos()
        {
            EpisodeDropdown.DisplayMemberPath = "Value";
            EpisodeDropdown.SelectedValuePath = "Key";
            LevelDropdown.SelectedValuePath = "Key";
            LevelDropdown.DisplayMemberPath = "Value";
            SkillLevelDropdown.DisplayMemberPath = "Value";
            SkillLevelDropdown.SelectedValuePath = "Key";

            SkillLevelDropdown.ItemsSource = doomManager.GetSkillLevelView();
            EpisodeDropdown.ItemsSource = doomManager.GetEpisodeView();
            PlayersDropdown.ItemsSource = doomManager.GetNumberOfPlayers();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Minutes != null)
            {
                Minutes.Content = 10 - Math.Floor(e.NewValue) + " Minutes";
            }
        }

        private void TurboSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TurboPercentage != null)
            {
                TurboPercentage.Content = Math.Floor(e.NewValue) + "%";
            }
        }

        private void Button_JoinClick(object sender, RoutedEventArgs e)
        {
            var dosBoxOptions = doomManager.GetDOSBoxOptions(this, false);

            var multiplayerWindow = new MultiplayerClientWindow(new MultiplayerOptions
            {
                Game = DOSBoxManager.GetUnstartedProcess(dosBoxOptions),
            });
            multiplayerWindow.Show();
            //Hide();
        }

        private void Button_StartClick(object sender, RoutedEventArgs e)
        {
            var dosBoxOptions = doomManager.GetDOSBoxOptions(this);

            var multiplayerWindow = new MultiplayerHostWindow(new MultiplayerOptions
            {
                NumberOfPlayers = 2,
                Game = DOSBoxManager.GetUnstartedProcess(dosBoxOptions),
                GameArguments = dosBoxOptions.Arguments
            });

            multiplayerWindow.Show();
            //Hide();
        }
    }
}
