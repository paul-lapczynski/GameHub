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
        DoomInfoManager doomInfoManager;
        Process doom;

        public Doom2Window(MainWindow main)
        {
            _main = main;
            doomInfoManager = new DoomInfoManager();
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
            LevelDropdown.ItemsSource = doomInfoManager.GetLevelView(int.Parse(((KeyValue)EpisodeDropdown.SelectedItem).Key));
        }

        private void StartDoom(object sender, RoutedEventArgs e)
        {
            doom = DOSBoxManager.StartDOSBox(new DOSBoxOptions
            {
                ExeFolderPath = $"DOOMIIDO\\",
                ExeName = "DOOM2.EXE",
                Fullscreen = true,
                Arguments = doomInfoManager.GetDoomArguments(new DoomArguments
                {
                    StartLevel = ((KeyValue)LevelDropdown.SelectedItem).Key
                })
            });

            doom.Exited += Doom_Exited;
            Hide();
        }

        private void Doom_Exited(object sender, EventArgs e)
        {
            Show();
        }

        private void SetupCombos()
        {
            EpisodeDropdown.DisplayMemberPath = "Value";
            EpisodeDropdown.SelectedValuePath = "Key";
            LevelDropdown.SelectedValuePath = "Key";
            LevelDropdown.DisplayMemberPath = "Value";

            EpisodeDropdown.ItemsSource = doomInfoManager.GetEpisodeView();

        }
    }
}
