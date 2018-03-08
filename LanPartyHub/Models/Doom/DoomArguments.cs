using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanPartyHub.Models.Doom
{
    public class DoomArguments
    {
        public int? NumberOfPlayers { get; set; }

        public bool? IsServer { get; set; }

        public string StartLevel { get; set; }

        public int? ServerPortNumber { get; set; }

        public string CustomWAD { get; set; }

        public bool? Multiplayer { get; set; }
    }
}
