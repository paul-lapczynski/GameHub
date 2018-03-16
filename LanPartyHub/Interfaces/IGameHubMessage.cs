using LanPartyHub.Enumerations;
using LanPartyHub.Enumerations.GameHubConnectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanPartyHub.Interfaces
{
    public interface IGameHubMessage
    {
        bool GameStarted { get; set; }

        int SenderGamePort { get; set; }

        string Text { get; set; }

        eMessageType Status { get; set; }
    }
}
