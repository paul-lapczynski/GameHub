using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanPartyHub.Models
{
    public class MultiplayerOptions
    {
        public int NumberOfPlayers { get; set; }

        public Process Game { get; set; }

        public string GameArguments { get; set; }
    }
}
