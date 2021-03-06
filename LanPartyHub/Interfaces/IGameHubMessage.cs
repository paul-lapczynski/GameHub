﻿using LanPartyHub.Enumerations;
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
        int SenderGamePort { get; set; }

        string Text { get; set; }

        EMessageType Status { get; set; }
    }
}
