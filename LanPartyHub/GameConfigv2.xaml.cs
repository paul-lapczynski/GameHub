using LanPartyHub.Managers;
using LanPartyHub.Models;
using LanPartyHub.Models.DOSBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for GameConfigv2.xaml
    /// </summary>
    public partial class GameConfigv2 : Window
    {
        private string _gameId { get; set; }

        public GameConfigv2(string gameId)
        {
            InitializeComponent();
            Init(gameId);
            Unloaded += Unload;
        }

        public void Init(string gameId)
        {
            _gameId = gameId;
            var game = GameManager.Settings.Games.First(g => g.GameId == gameId);

            icCards.ItemsSource = DOSBoxManager.GetSettingsForGame(game, DOSBoxManager.DefaultSettings);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var game = GameManager.Settings.Games.First(g => g.GameId == _gameId);

            game.DOSBoxOverrides = icCards.ItemsSource.Cast<DOSBoxSettingForConfig>().Select(setting => new DOSBoxSettingOverride
            {
                Name = setting.Name,
                SelectedValue = setting.SelectedValue,
                Section = setting.Section
            }).ToList();

            GameManager.SaveSettings();

            Close();
        }

        private void Unload(object sender, RoutedEventArgs e)
        {
            Owner.Show();
        }
    }
}