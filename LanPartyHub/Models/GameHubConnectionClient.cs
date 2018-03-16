using LanPartyHub.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LanPartyHub.Models
{
    public class GameHubConnectionClient
    {
        public string DisplayName { get; set; }

        public Guid ClientId { get; set; }

        public TcpClient Connection { get; set; }

        public CancellationTokenSource SubscribeSoure { get; set; }

        public GameHubMessageQueue MessageQueue { get; set; }
    }
}