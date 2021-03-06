﻿using LanPartyHub.Enumerations.GameHubConnectivity;
using LanPartyHub.Helpers;
using LanPartyHub.Interfaces;
using LanPartyHub.Models;
using LanPartyHub.Models.Connectivity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace LanPartyHub.Utilities
{
    public class GameHubServer : IDisposable
    {
        private static readonly object _clientsLock = new object();
        private List<GameHubConnectionClient> _clients;
        private CancellationTokenSource _tokenSource;
        private Process DnsHostProcess;

        public GameHubMessageQueue MessageQueue { get; private set; }
        public TcpListener Server { get; private set; }
        public delegate void MessageRecieved(object sender, List<IGameHubMessage> message);
        public event MessageRecieved MessageReceived;

        public GameHubServer()
        {
            _clients = new List<GameHubConnectionClient>();
            _tokenSource = new CancellationTokenSource();
            MessageQueue = new GameHubMessageQueue();
            MessageQueue.MessagesAdded += HandleMessagesAdded;

            Start();
        }

        public void Start()
        {
            Server = TcpListener.Create(0);
            Server.Start();

            var randomTcpPort = TcpListener.Create(0);
            randomTcpPort.Start();
            var portNumber = ((IPEndPoint)randomTcpPort.LocalEndpoint).Port;
            randomTcpPort.Stop();

            var token = _tokenSource.Token;

            DnsHostProcess = Process.Start(new ProcessStartInfo
            {
                FileName = "dns-sd.exe",
                Arguments = $"-R \"{GameHubConnectivity.DnsDiscoveryName}\" \"\" \"local\" {portNumber} {GameHubConnectivity.PortKeyDisplayName}={((IPEndPoint)Server.LocalEndpoint).Port}"
            });

            GameHubConnectivity.Listen(
                HandleClientConnect,
                TimeSpan.FromMilliseconds(100),
                token,
                TaskCreationOptions.LongRunning);
        }

        public void Dispose()
        {
            if(DnsHostProcess != null && !DnsHostProcess.HasExited)
            {
                DnsHostProcess.CloseMainWindow();
            }

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

                        GameHubConnectivity.Subscribe(client, MessageQueue, TimeSpan.FromMilliseconds(200), token);

                        var message = new GameHubMessage
                        {
                            SenderGamePort = ((IPEndPoint)Server.LocalEndpoint).Port,
                            Text = "Welcome to the GameHub Server. I hope you enjoy yourself",
                            Status = EMessageType.InitialServerConnection
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

        private void HandleMessagesAdded(object sender, QueueChangeEventArgs args)
        {
            var messages = args.Messages;

            Console.WriteLine(JsonConvert.SerializeObject(messages));

            MessageReceived(sender, messages);
        }
    }
}