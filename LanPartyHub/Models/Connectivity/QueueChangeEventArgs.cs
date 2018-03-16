using LanPartyHub.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanPartyHub.Models.Connectivity
{
    public class QueueChangeEventArgs : EventArgs
    {
        public List<IGameHubMessage> Messages { get; set; }
    }
}
