using LanPartyHub.Managers;
using LanPartyHub.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace LanPartyHub
{
    /// <summary>
    /// Interaction logic for DOSBoxConfig.xaml
    /// </summary>
    public partial class DOSBoxConfigWindow : Window
    {
        MainWindow _main;
        public DOSBoxConfigWindow(MainWindow main)
        {
            _main = main;
            InitializeComponent();
            DOSBoxCPath.Text = ApplicationManager.Settings.VirtualDOSBoxCDrivePath;
            listGames.ItemsSource = ApplicationManager.Settings.Games.ToList();
        }

        private void Unload(object sender, RoutedEventArgs e)
        {
            _main.Show();
        }

        private void DOSBoxConfigSave(object sender, RoutedEventArgs e)
        {
            ApplicationManager.Settings.VirtualDOSBoxCDrivePath = DOSBoxCPath.Text.ToString();
            ApplicationManager.SaveSettings();
        }

        private void DOSBoxConfigAddGame(object sender, RoutedEventArgs e)
        {
            string FilePath;
            string FileName;
            string FolderName;
            var newGame = new Game();

            //Open file picker to select game executable
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                InitialDirectory = ApplicationManager.Settings.VirtualDOSBoxCDrivePath,
                Title = "Select Game Executable"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = Path.GetDirectoryName(openFileDialog.FileName);
                FileName = Path.GetFileName(openFileDialog.FileName);
                FolderName = new DirectoryInfo(FilePath).Name;
                newGame.Name = FolderName;
                newGame.FolderPath = FolderName;
                newGame.ExecutableName = FileName;
                newGame.GameId = Guid.NewGuid().ToString();
                newGame.GameSettings = [];
                newGame.StartupType = Enumerations.Game.EStartupType.Standard;
            }

            if (!string.IsNullOrEmpty(newGame.Name))
            {
                //Open file picker to select game image
                openFileDialog = new OpenFileDialog()
                {
                    Multiselect = false,
                    Filter = "Image files (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp|All files (*.*)|*.*",
                    InitialDirectory = ApplicationManager.Settings.VirtualDOSBoxCDrivePath,
                    Title = "Select Game Image"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    newGame.ImagePath = openFileDialog.FileName;
                }

                //Update games list
                if (!string.IsNullOrEmpty(newGame.ImagePath))
                {
                    ApplicationManager.Settings.Games.Add(newGame);
                    ApplicationManager.SaveSettings();
                    _main.icGamesList.ItemsSource = ApplicationManager.Settings.Games.ToList();
                    listGames.ItemsSource = ApplicationManager.Settings.Games.ToList();
                }
            }
            
        }

        private void DOSBoxConfigRemoveGame(object sender, RoutedEventArgs e)
        {
            Game game = new Game();
            game = (Game)listGames.SelectedItem;
            ApplicationManager.Settings.Games.Remove(game);
            ApplicationManager.SaveSettings();
            _main.icGamesList.ItemsSource = ApplicationManager.Settings.Games.ToList();
            listGames.ItemsSource = ApplicationManager.Settings.Games.ToList();
        }
    }
}
