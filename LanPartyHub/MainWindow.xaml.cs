using LanPartyHub.Enumerations.GameHubConnectivity;
using LanPartyHub.Managers;
using LanPartyHub.Models;
using LanPartyHub.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LanPartyHub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameHubServer gg;
        GameHubClient gg2;

        public MainWindow()
        {
            InitializeComponent();
            gg = new GameHubServer();
            gg2 = new GameHubClient();
            gg2.Connect();
        }

        private void Doom2MouseDown(object sender, MouseButtonEventArgs e)
        {
            gg.Dispose();
            //var doomWindow = new Doom2Window(this, 1);
            //Application.Current.MainWindow = doomWindow;
            //doomWindow.Show();
            //Hide();
        }

        private void GameImageClick(object sender, MouseButtonEventArgs e)
        {
            var message = new GameHubMessage
            {
                Status = eMessageType.HandShakeOne,
                SenderGamePort = ((IPEndPoint)gg.Server.LocalEndpoint).Port,
                GameStarted = false,
                Text = "From Dynamc image"
            };

            gg.NotifyClients(message);
            //var doomWindow = new Doom2Window(this, (e.Source as GameImage).GameId);
            //Application.Current.MainWindow = doomWindow;
            //doomWindow.Show();
            //Hide();
        }

        private void War2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var message = new GameHubMessage
            {
                Status = eMessageType.HandShakeOne,
                Text = "War 2 Mouse Down"
            };

            gg.NotifyClients(message);
        }

        private void Doom2MouseEnter(object sender, MouseEventArgs e)
        {
        }

        private void InitGameImage(object sender, RoutedEventArgs e)
        {
            var gg = (GameImage)e.Source;
            GameInage.Init(ApplicationManager.Settings.Games[0]);
        }
    }
}
