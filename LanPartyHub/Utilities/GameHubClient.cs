using LanPartyHub.Helpers;
using LanPartyHub.Models.Connectivity;
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

namespace LanPartyHub.Models
{
    public class GameHubClient : IDisposable
    {
        public string DisplayName { get; set; }

        public Guid ClientId { get; private set; }

        public TcpClient Connection { get; set; }

        private CancellationTokenSource _subscribeSoure;

        public object ReadWriteBlocker = new object();

        public GameHubMessageQueue MessageQueue { get; private set; }

        public GameHubClient()
        {
            MessageQueue = new GameHubMessageQueue();
            MessageQueue.MessagesAdded += HandleMessagesAdded;
            ClientId = Guid.NewGuid();
        }

        public void Connect()
        {
            Task.Factory.StartNew(async () =>
            {
                var notFound = true;
                while (notFound)
                {
                    var host = await GameHubConnectivity.ResolveServerHost();
                    if(host.IPAddress == null)
                    {
                        continue;
                    }

                    var ipAddress = IPAddress.Parse(host.IPAddress);
                    var ip = GameHubConnectivity.GetZeroconfigHostIp(host);
                    InternalConnect(ipAddress, ip);
                    notFound = false;
                }
            });
        }

        private void InternalConnect(IPAddress serverIpAddress, int serverIp)
        {
            Connection = new TcpClient();
            try
            {
                Connection.ReceiveBufferSize = 4096;
                Connection.Connect(serverIpAddress, serverIp);
                Subscribe();
            }
            catch
            {
                // I should probably doo something hurrrr
            }
        }

        public void Subscribe()
        {
            _subscribeSoure = new CancellationTokenSource();

            var token = _subscribeSoure.Token;

            GameHubConnectivity.Subscribe(Connection, MessageQueue, TimeSpan.FromMilliseconds(100), token);
        }

        public async Task<int> FindServerPort()
        {
            var host = await GameHubConnectivity.ResolveServerHost();
            var port = GameHubConnectivity.GetZeroconfigHostIp(host);

            return port;
        }

        private void HandleMessagesAdded(object sender, QueueChangeEventArgs args)
        {
            var messages = args.Messages;

            Console.WriteLine(JsonConvert.SerializeObject(messages));
        }

        public void Dispose()
        {
            _subscribeSoure.Cancel();
            _subscribeSoure.Dispose();
            Connection.Close();
            Connection.Dispose();
        }
    }
}
