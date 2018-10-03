using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanPartyHub.Enumerations
{
    namespace GameHubConnectivity
    {
        public enum eMessageType
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
        public enum eProvider
        {
            Windows = 1,
            DOSBox = 2,
            MacOS = 3,
            Wine = 4
        }

        public enum eStartupType
        {
            Standard = 1,
            Custom = 2
        }
    }
}
