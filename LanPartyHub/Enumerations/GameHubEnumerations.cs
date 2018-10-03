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
            HandShakeOne = 1,
            HandShakeTwo = 2,
            HandShakeThree = 3,
            Normal = 4,
            GameStarted = 5,
            InitialConnection = 6
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
            Custom = 2
        }
    }
}
