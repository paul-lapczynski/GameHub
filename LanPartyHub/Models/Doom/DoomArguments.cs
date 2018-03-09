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

        public string StartLevel { get; set; }

        public string CustomWAD { get; set; }

        public bool? Multiplayer { get; set; }

        public bool? Deathmath { get; set; }

        public int? Timer { get; set; }

        public bool? UseTimer { get; set; }

        public bool? Turbo { get; set; }

        public int TurboPercentage { get; set; }

        public bool? Altdeath { get; set; }

        public string SkillLevel { get; set; }
    }
}
