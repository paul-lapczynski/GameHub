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
            HandShakeOne = 1,
            HandShakeTwo = 2,
            HandShakeThree = 3,
            Normal = 4,
            GameStarted = 5
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
    }
}
