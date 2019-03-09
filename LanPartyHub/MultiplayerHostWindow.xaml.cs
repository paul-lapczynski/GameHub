using LanPartyHub.Enumerations.GameHubConnectivity;
using LanPartyHub.Helpers;
using LanPartyHub.Interfaces;
using LanPartyHub.Models;
using LanPartyHub.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for MultiplayerHostWindow.xaml
    /// </summary>
    public partial class MultiplayerHostWindow : Window
    {
        GameHubServer Server;

        private List<string> Players = new List<string>();

        MultiplayerOptions Options;

        public MultiplayerHostWindow(MultiplayerOptions options)
        {
            InitializeComponent();

            Server = new GameHubServer();
            Server.MessageReceived += MessageReceieved;
            Options = options;
        }

        private void MessageReceieved(object sender, List<IGameHubMessage> messages)
        {
            messages.ForEach(msg =>
            {
                switch (msg.Status)
                {
                    //case EMessageType.InitialClientConnection:
                    //    {
                    //        Players.Add($"Pablo {Players.Count}");
                    //        Dispatcher.Invoke(() =>
                    //        {
                    //            Waiting_Text.Text = msg.Text;
                    //        });

                    //        //Server.NotifyClients(new GameHubMessage
                    //        //{
                    //        //    Status = EMessageType.InitialConnection,
                    //        //    Text = JsonConvert.SerializeObject(Players)
                    //        //});

                    //        break;
                    //    }
                    case EMessageType.PlayerJoined:
                        {
                            if (Server != null)
                            {
                                Players.Add($"Player {Players.Count + 1}");

                                Dispatcher.Invoke(() =>
                                {
                                    PlayersConnected_Text.Text = string.Join("\n", Players);
                                });
                            }

                            Server.NotifyClients(new GameHubMessage
                            {
                                Status = EMessageType.PlayerJoined,
                                Text = JsonConvert.SerializeObject(Players)
                            });

                            break;
                        }

                        //case EMessageType.Normal:
                        //    {
                        //        if (Server != null)
                        //        {
                        //            Players.Add(msg.Text);

                        //            Dispatcher.Invoke(() =>
                        //            {
                        //                PlayersConnected_Text.Text = string.Join("\n", Players);
                        //            });

                        //        }
                        //        break;
                        //    }
                }
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Server.NotifyClients(new GameHubMessage
            {
                Status = EMessageType.GameStarted,
                Text = Options.GameArguments
            });

            Options.Game.Start();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Server.Dispose();
            base.OnClosing(e);
        }
    }
}
