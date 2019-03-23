using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanPartyHub.Enumerations
{
    namespace GameHubConnectivity
    {
        public enum EMessageType
        {
            InitialClientConnection = 1,
            InitialServerConnection = 2,
            PlayerJoined = 3,
            PlayerLeft = 4,
            GameStarted = 5,
        }
    }

    namespace Game
    {
        public enum EProvider
        {
            Windows = 1,
            DOSBox = 2,
            MacOS = 3,
            Wine = 4
        }

        public enum EStartupType
        {
            Standard = 1,
            Doom = 2
        }
    }
}
