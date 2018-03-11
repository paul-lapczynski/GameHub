using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zeroconf;

namespace LanPartyHub.Managers
{
    public class ConnectionManager : IDisposable
    {
        TcpListener server;
        TcpClient client;

        public ConnectionManager()
        {
            TestSerer();
        }

        public async Task Connect()
        {
            var serverTask = await ResolveServerHost();
            ConnectToServer(serverTask);
        }

        public TcpListener StartServer()
        {
            var server = TcpListener.Create(9125);
            server.Start();
            Listen();

            // TODO need to start the server with the port number
            return server;
        }

        public void Listen()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var client = server.AcceptTcpClient();
                    var stream = client.GetStream();
                    if (!stream.DataAvailable)
                    {
                        continue;
                    }

                    byte[] buffer = new byte[client.ReceiveBufferSize];

                    //---read incoming stream---
                    int bytesRead = stream.Read(buffer, 0, client.ReceiveBufferSize);

                    //---convert the data received into a string---
                    string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received : " + dataReceived);

                    //---write back the text to the client---
                    Console.WriteLine("Sending back : " + dataReceived);
                    stream.Write(buffer, 0, bytesRead);
                }
            }).Start();
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

            var endpoint = new IPEndPoint(ip, 9127);
            client = new TcpClient(endpoint);
            client.Connect(ip, 9125);
            var stream = client.GetStream();

            //---create a TCPClient object at the IP and port no.---
            NetworkStream nwStream = client.GetStream();
            byte[] bytesToSend = Encoding.ASCII.GetBytes("WHAT THE HELL");

            //---send the text---
            Console.WriteLine("Sending : " + "what the hell");
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            //---read back the text---
            client.Close();

            if (client.Connected)
            {
                Console.WriteLine("Connected");
            }
        }

        public void Dispose()
        {
            client.Close();
            client.Dispose();
            server.Stop();
        }

        public void TestClient()
        {

        }

        public void TestSerer()
        {
            var server = TcpListener.Create(8004);
            server.Start();
            var tcpClient = new TcpClient();
            var tcpClient2 = new TcpClient();
            try
            {
                tcpClient.Connect(IPAddress.Loopback, 8004);
                tcpClient2.Connect(IPAddress.Loopback, 8004);

                var networkStream = tcpClient.GetStream();
                var networkStream2 = tcpClient2.GetStream();

                if (networkStream.CanWrite && networkStream.CanRead)
                {
                    var txtSize = "10234";
                    var sendFileSize = Encoding.ASCII.GetBytes(txtSize);

                    networkStream.Write(sendFileSize, 0, txtSize.Length);
                }
                tcpClient.Close();
                tcpClient.Dispose();
                server.Stop();
            }
            catch { }
        }
    }
}
