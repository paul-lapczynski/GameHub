using LanPartyHub.Enumerations;
using LanPartyHub.Enumerations.GameHubConnectivity;
using LanPartyHub.Helpers;
using LanPartyHub.Interfaces;
using LanPartyHub.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LanPartyHub.Utilities
{
    public class GameHubServer : IDisposable
    {
        private static readonly object _clientsLock = new object();
        private List<GameHubConnectionClient> _clients;
        public GameHubMessageQueue MessageQueue { get; private set; }
        private CancellationTokenSource _tokenSource;
        private Process DnsHostProcess;
        public TcpListener Server { get; private set; }

        public GameHubServer()
        {
            _clients = new List<GameHubConnectionClient>();
            _tokenSource = new CancellationTokenSource();
            MessageQueue = new GameHubMessageQueue();

            Start();
        }

        public void Start()
        {
            Server = TcpListener.Create(9125);
            Server.Start();

            var token = _tokenSource.Token;

            DnsHostProcess = Process.Start(new ProcessStartInfo
            {
                FileName = "dns-sd.exe",
                Arguments = $"-R \"GameHub-Discovery\" \"\" \"local\" {5467} ApplicationPort={((IPEndPoint)Server.LocalEndpoint).Port}"
            });

            GameHubConnectivity.Listen(
                HandleClientConnect,
                TimeSpan.FromMilliseconds(100),
                token,
                TaskCreationOptions.LongRunning);
        }


        public void Dispose()
        {
            DnsHostProcess.Close();
            _tokenSource.Cancel();
        }

        private void HandleClientConnect(CancellationToken token)
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    if (Server.Pending())
                    {
                        var client = await Server.AcceptTcpClientAsync();
                        var gameHubClient = new GameHubConnectionClient
                        {
                            Connection = client
                        };

                        lock (_clientsLock)
                        {
                            _clients.Add(gameHubClient);
                        }

                        var message = new GameHubMessage
                        {
                            SenderGamePort = ((IPEndPoint)Server.LocalEndpoint).Port,
                            Text = "Welcome to the GameHub Server. I hope you enjoy yourself",
                            Status = eMessageType.Normal
                        };

                        NotifyClient(gameHubClient, message);
                    }
                }
                catch
                {
                    return;
                }
            }, token);
        }

        public void NotifyClients(GameHubMessage message)
        {
            Parallel.ForEach(_clients, (GameHubConnectionClient client) => { NotifyClient(client, message); });
        }

        public void NotifyClient(GameHubConnectionClient client, GameHubMessage message)
        {
            try
            {
                if (client.Connection.Connected)
                {
                    GameHubConnectivity.SendMessageToClient(client, message);
                }
            }
            catch
            {
                return;
            }
        }
    }
}