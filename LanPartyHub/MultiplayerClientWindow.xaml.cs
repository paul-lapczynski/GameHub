using LanPartyHub.Enumerations.GameHubConnectivity;
using LanPartyHub.Helpers;
using LanPartyHub.Interfaces;
using LanPartyHub.Managers;
using LanPartyHub.Models;
using LanPartyHub.Models.Connectivity;
using LanPartyHub.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for MultiplayerIntermediateWindow.xaml
    /// </summary>
    public partial class MultiplayerClientWindow : Window
    {
        private const string NumberOfPlayersReplacement = "[NumberOf]";
        private List<string> Players = new List<string>();
        GameHubClient Client = new GameHubClient();
        public int gameIp;

        MultiplayerOptions Options;

        public MultiplayerClientWindow(MultiplayerOptions options)
        {
            InitializeComponent();
            Options = options;
            Init();
        }

        private void Init()
        {
            Client.MessageReceived += MessageReceieved;
            Client.Connect();
        }

        private void MessageReceieved(object sender, List<IGameHubMessage> messages)
        {
            messages.ForEach(msg =>
            {
                switch (msg.Status)
                {
                    case eMessageType.InitialServerConnection:
                        {
                            Players.Add(msg.Text);
                            gameIp = msg.SenderGamePort;

                            Dispatcher.Invoke(() =>
                            {
                                PlayersConnected_Text.Text = string.Join("\n", Players);
                            });

                            Client.SendMessageToServer(new GameHubMessage
                            {
                                Status = eMessageType.PlayerJoined
                            });

                            break;
                        }
                    case eMessageType.PlayerJoined:
                        {
                            Players = JsonConvert.DeserializeObject<List<string>>(msg.Text);

                            Dispatcher.Invoke(() =>
                            {
                                PlayersConnected_Text.Text = string.Join("\n", Players);
                            });

                            break;
                        }
                    case eMessageType.GameStarted:
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Options.Game.StartInfo.Arguments += msg.Text;
                                Options.Game.Start();
                            });

                            break;
                        }
                }
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Client.Dispose();
            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }
    }
}
