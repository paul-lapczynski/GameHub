using LanPartyHub.Managers;
using LanPartyHub.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LanPartyHub
{
    /// <summary>
    /// Interaction logic for GameConfig.xaml
    /// </summary>
    public partial class GameConfigWindow : Window
    {
        StdGameWindow _main;
        private string _gameid;
        private Game _game;

        public GameConfigWindow(StdGameWindow main, string gameId)
        {
            _main = main;
            _gameid = gameId;
            _game = ApplicationManager.Settings.Games.First(game => game.GameId == _gameid);
            InitializeComponent();
            GamePath.Text = _game.FolderPath + "\\" + _game.ExecutableName;
            GameImagePath.Text = _game.ImagePath;
            if (_game.GameSettings != null && _game.GameSettings.Count > 0)
            {
                List<KeyValue> settings = new List<KeyValue>();
                _game.GameSettings.ToList().ForEach(item =>
                {
                    var newitem = new KeyValue();
                    newitem.Key = item.Key;
                    newitem.Value = item.Key + " - " + item.Value;
                    settings.Add(newitem);
                });

                listSettings.ItemsSource = settings.ToList();
            }

            SettingDropdown.ItemsSource = ApplicationManager.Settings.DOSBoxSettings.ToList();
        }

        private void Unload(object sender, RoutedEventArgs e)
        {
            _main.Show();
        }

        private void GameConfigSave(object sender, RoutedEventArgs e)
        {
            if (GamePath.Text.Length > 0) {
                _game.FolderPath = GamePath.Text;
            }
            if (GameImagePath.Text.Length > 0) {
                _game.ImagePath = GameImagePath.Text;
            }
            
            ApplicationManager.SaveSettings();
        }

        private void SettingsDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = ((LanPartyHub.Models.DOSBoxSetting)SettingDropdown.SelectedItem).Key;
            var item = ApplicationManager.Settings.DOSBoxSettings.First(DOSBoxSetting => DOSBoxSetting.Key == selected);
            List<string> valuesList = item.Values;
            ValueDropdown.ItemsSource = valuesList;
            SettingDescription.Text = "Description: " + item.Description;
        }

        private void GameConfigAddSetting(object sender, RoutedEventArgs e)
        {
            var key = ((LanPartyHub.Models.DOSBoxSetting)SettingDropdown.SelectedItem).Key.ToString();
            var value = ValueDropdown.SelectedValue.ToString();
            if (key.Length > 0 && value.Length > 0) {
                var setting = new KeyValue();
                setting.Key = key;
                setting.Value = value;
                _game.GameSettings.Add(setting);
                ApplicationManager.SaveSettings();

                List<KeyValue> settings = new List<KeyValue>();
                _game.GameSettings.ToList().ForEach(item =>
                {
                    var newitem = new KeyValue();
                    newitem.Key = item.Key;
                    newitem.Value = item.Key + " - " + item.Value;
                    settings.Add(newitem);
                });

                listSettings.ItemsSource = settings.ToList();
            }
        }

        private void GameConfigRemoveSetting(object sender, RoutedEventArgs e)
        {
            var setting = new KeyValue();
            _game.GameSettings.ToList().ForEach(item =>
            {
                if (((LanPartyHub.Models.DOSBoxSetting)SettingDropdown.SelectedItem).Key.ToString() == item.Key)
                {
                    setting = item;
                }
            });

            _game.GameSettings.Remove(setting);
            ApplicationManager.SaveSettings();

            List<KeyValue> settings = new List<KeyValue>();
            _game.GameSettings.ToList().ForEach(item =>
            {
                var newitem = new KeyValue();
                newitem.Key = item.Key;
                newitem.Value = item.Key + " - " + item.Value;
                settings.Add(newitem);
            });

            listSettings.ItemsSource = settings.ToList();
        }
    }
}
