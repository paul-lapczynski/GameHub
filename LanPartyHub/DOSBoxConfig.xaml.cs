using LanPartyHub.Managers;
using LanPartyHub.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace LanPartyHub
{
    /// <summary>
    /// Interaction logic for War2Window.xaml
    /// </summary>
    public partial class DOSBoxConfigWindow : Window
    {
        MainWindow _main;

        public DOSBoxConfigWindow(MainWindow main)
        {
            _main = main;

            InitializeComponent();
            DOSBoxCPath.Text = ApplicationManager.Settings.VirtualDOSBoxCDrivePath;
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
            string GameName;
            string ID;
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
                GameName = new DirectoryInfo(FilePath).Name;
                newGame.Name = GameName;
                newGame.FolderPath = FilePath;
                newGame.ExecutableName = FileName;
                newGame.GameId = Guid.NewGuid().ToString();
            }

            //Open file picker to select game image
            openFileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                InitialDirectory = ApplicationManager.Settings.VirtualDOSBoxCDrivePath,
                Title = "Select Game Image"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                newGame.ImagePath = openFileDialog.FileName;
            }

            //Update games list
            if (!string.IsNullOrEmpty(newGame.Name) && !string.IsNullOrEmpty(newGame.ImagePath))
            {
                ApplicationManager.Settings.Games.Add(newGame);
                ApplicationManager.SaveSettings();
            }
        }
    }
}
