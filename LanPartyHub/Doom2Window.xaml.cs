using LanPartyHub.Managers;
using LanPartyHub.Models;
using LanPartyHub.Models.Doom;
using LanPartyHub.Models.DOSBox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public Doom2Window(MainWindow main)
        {
            _main = main;
            doomManager = new DoomManager();
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

            doom = DOSBoxManager.StartDOSBox(new DOSBoxOptions
            {
                ExeFolderPath = $"DOOMIIDO\\",
                ExeName = "DOOM2.EXE",
                Fullscreen = true,
                Arguments = doomManager.GetDoomArguments(new DoomArguments
                {
                    StartLevel = ((KeyValue)LevelDropdown.SelectedItem).Key,
                    Turbo = TurboCheckbox.IsChecked,
                    TurboPercentage = (int)Math.Floor(TurboSlider.Value),
                    Multiplayer = MultiplayerCheckbox.IsChecked,
                    Deathmath = DeathmatchCheckbox.IsChecked,
                    Altdeath = WeaponReaspon.IsChecked,
                    NumberOfPlayers = (int?)PlayersDropdown.SelectedValue,
                    SkillLevel = (string)SkillLevelDropdown.SelectedValue,
                    Timer = (int)Math.Floor(TimerSlider.Value),
                    UseTimer = UseTimerCheckbox.IsChecked
                })
            });
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
            if(Minutes != null)
            {
                Minutes.Content = 10 - Math.Floor(e.NewValue) + " Minutes";
            }
        }

        private void TurboSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(TurboPercentage != null)
            {
                TurboPercentage.Content = Math.Floor(e.NewValue) + "%";
            }
        }
    }
}
