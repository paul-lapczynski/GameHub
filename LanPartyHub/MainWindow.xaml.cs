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
        GameHubServer Server;
        GameHubClient Client;

        public MainWindow()
        {
           InitializeComponent();
            icGamesList.ItemsSource = ApplicationManager.Settings.Games.ToList();
            Server = new GameHubServer();
            Client = new GameHubClient();
            Client.Connect();

            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Server.Dispose();
            Client.Dispose();
        }

        private void Doom2MouseDown(object sender, MouseButtonEventArgs e)
        {
            var doomWindow = new Doom2Window(this, "1");
            Application.Current.MainWindow = doomWindow;
            doomWindow.Show();
            Hide();
        }

        private void GameImageClick(object sender, MouseButtonEventArgs e)
        {
            var image = (GameImage)e.Source;
            var game = (Game)image.DataContext;

            //var message = new GameHubMessage
            //{
            //    Status = eMessageType.HandShakeOne,
            //    SenderGamePort = ((IPEndPoint)Server.Server.LocalEndpoint).Port,
            //    GameStarted = false,
            //    Text = game.Name
            //};

            //Server.NotifyClients(message);
        }

        private void War2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var war2Window = new War2Window(this, "2");
            Application.Current.MainWindow = war2Window;
            war2Window.Show();
            Hide();
        }

        private void GameImage_Loaded(object sender, RoutedEventArgs e)
        {
            var image = (GameImage)e.Source;
            var game = (Game)image.DataContext;
            image.Init(game);
        }

        private void MainConfig_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var Window = new DOSBoxConfigWindow(this);
            Application.Current.MainWindow = Window;
            Window.Show();
            Hide();
        }
    }
}
