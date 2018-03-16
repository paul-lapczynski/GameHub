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

namespace LanPartyHub.Managers
{
    /// <summary>
    /// WARNING: DEPRECIATED
    /// </summary>
    public class ConnectionManager : IDisposable
    {
        TcpClient client;

        TcpListener server;
        TcpClient serverClient;
        List<TcpClient> tcpClientConnection;

        /// <summary>
        ///  Remove before deploying. This is temporary because of client/server is being run locally
        /// </summary>
        object clientLock = new object();

        public ConnectionManager()
        {
            tcpClientConnection = new List<TcpClient>();
        }

        public async Task Connect()
        {
            var serverTask = await ResolveServerHost();
            ConnectToServer(serverTask);
        }

        public TcpListener StartServer()
        {
            server = TcpListener.Create(9125);
            server.Start();

            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        if (server.Pending())
                        {
                            serverClient = server.AcceptTcpClient();
                        }

                        if (serverClient != null && serverClient.Connected && serverClient.GetStream().DataAvailable)
                        {
                            var stream = new StreamReader(serverClient.GetStream());
                            var gotcha = stream.ReadToEnd();
                            Console.WriteLine($"Got Data: {gotcha}");
                        }
                    }
                    catch
                    {
                        serverClient.Close();
                        serverClient.Dispose();
                        continue;
                    }


                    Thread.Sleep(100);
                }
            });

            // TODO need to start the server with the port number
            return server;
        }

        public async Task<IZeroconfHost> ResolveServerHost()
        {
            var options = new ResolveOptions("_http._tcp.local.")
            {
                Retries = 10,
                RetryDelay = TimeSpan.FromMilliseconds(500)
            };

            var responses = await ZeroconfResolver.ResolveAsync(options);

            var serverHost = responses.First(x => x.DisplayName == "LanPartyHub-Server");

            return serverHost;
        }

        public async Task ConnectToServer(IZeroconfHost serverHost)
        {
            var port = serverHost.Services.First().Value.Properties.First().First(x => x.Key == "port");

            var ip = IPAddress.Parse(serverHost.IPAddress);

            //var endpoint = new IPEndPoint(IPAddress.Loopback, 9127);

            Task.Run(() =>
            {
                while (true)
                {
                    lock (clientLock)
                    {
                        if (client == null || !client.Connected)
                        {
                            client = new TcpClient();
                            client.Connect(ip, 9125);
                        }

                        var stream = client.GetStream();
                        if (stream.CanWrite)
                        {
                            var writer = new StreamWriter(stream);
                            writer.WriteLine("Thread 1");
                            writer.Flush();
                            writer.Close();
                        }
                        Thread.Sleep(300);
                    }
                }
            });

            Task.Run(() =>
            {
                while (true)
                {
                    lock (clientLock)
                    {
                        if (client == null || !client.Connected)
                        {
                            client = new TcpClient();
                            client.Connect(ip, 9125);
                        }

                        var stream = client.GetStream();
                        if (stream.CanWrite)
                        {
                            var writer = new StreamWriter(stream);
                            writer.WriteLine("Thread 2");
                            writer.Flush();
                            writer.Close();
                        }
                        Thread.Sleep(300);
                    }
                }
            });
        }

        public void Dispose()
        {
            client.Close();
            client.Dispose();
            serverClient.Close();
            serverClient.Dispose();
            server.Stop();
        }
    }
}
