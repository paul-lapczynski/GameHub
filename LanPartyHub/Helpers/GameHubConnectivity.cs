﻿using LanPartyHub.Enumerations;
using LanPartyHub.Enumerations.GameHubConnectivity;
using LanPartyHub.Interfaces;
using LanPartyHub.Models;
using LanPartyHub.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zeroconf;

namespace LanPartyHub.Helpers
{
    public static class GameHubConnectivity
    {
        public static readonly string PortKeyDisplayName = "ApplicationPort";

        public static readonly string DnsDiscoveryName = "GameHub-Discovery";

        public static void Listen(
           Action<CancellationToken> action,
           TimeSpan pollInterval,
           CancellationToken token,
           TaskCreationOptions taskCreationOptions = TaskCreationOptions.None)
        {
            Task.Factory.StartNew(
                () =>
                {
                    while (true)
                    {
                        try
                        {
                            action(token);

                            if (token.WaitHandle.WaitOne(pollInterval))
                            {
                                break;
                            }
                        }
                        catch
                        {
                            return;
                        }
                    }
                },
                token,
                taskCreationOptions,
                TaskScheduler.Default);
        }

        public static void Subscribe(
            TcpClient client,
            GameHubMessageQueue MessageQueue,
            TimeSpan pollInterval,
            CancellationToken token,
            TaskCreationOptions taskCreationOptions = TaskCreationOptions.None)
        {
            Task.Factory.StartNew(
               () =>
               {
                   while (true)
                   {
                       try
                       {
                           if (client != null && client.Connected && client.GetStream().DataAvailable)
                           {
                               ReadMessagesFromConnection(client, MessageQueue);
                           }

                           if (token.WaitHandle.WaitOne(pollInterval))
                           {
                               break;
                           }
                       }
                       catch
                       {
                           client.Close();
                       }
                   }
               },
               token,
               taskCreationOptions,
               TaskScheduler.Default);
        }

        public static void ReadMessagesFromConnection(TcpClient client, GameHubMessageQueue queue)
        {
            var messages = new List<IGameHubMessage>();
            var stream = client.GetStream();
            var jsonBytes = new byte[client.ReceiveBufferSize];

            stream.Read(jsonBytes, 0, jsonBytes.Length);

            var jsonMessage = GameHubMessage.GetStrings(jsonBytes);

            try
            {
                jsonMessage.ToList().ForEach(message =>
                {
                    messages.Add(JsonConvert.DeserializeObject<GameHubMessage>(message));
                });
            }
            catch (JsonSerializationException)
            {
                // Something was probably wrong with the message
            }
            catch (Exception)
            {
                Console.WriteLine("Error in decoding message");
            }

            queue.AddRange(messages);
            queue.QueueChanged();
        }

        public static void SendMessageToClient(GameHubConnectionClient client, GameHubMessage message)
        {
            SendMessageToClient(client.Connection, message);
        }

        public static void SendMessageToClient(TcpClient client, GameHubMessage message)
        {
            var jsonBytes = GameHubMessage.GetBytes(message);
            var stream = client.GetStream();

            if (stream.CanWrite)
            {
                stream.Write(jsonBytes, 0, jsonBytes.Length);
                stream.Flush();
            }
        }

        public static async Task<IZeroconfHost> ResolveServerHost(int retries = 10)
        {
            IZeroconfHost host = null;

            while (retries > 0)
            {
                host = await InternalResolveServerHost();

                if (host != null)
                {
                    break;
                }

                Thread.Sleep(TimeSpan.FromMilliseconds(50));
            }

            return host;
        }

        private static async Task<IZeroconfHost> InternalResolveServerHost()
        {
            var options = new ResolveOptions("_http._tcp.local.");
            var responses = await ZeroconfResolver.ResolveAsync(options);
            var serverHost = responses.FirstOrDefault(x => x.DisplayName == DnsDiscoveryName);

            return serverHost;
        }

        public static int GetZeroconfigHostIp(IZeroconfHost host)
        {
            var port = host.Services.First().Value.Properties.First().First(x => x.Key == PortKeyDisplayName);

            var intPort = int.Parse(port.Value);

            return intPort;
        }
    }
}
