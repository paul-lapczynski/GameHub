using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanPartyHub.Models.Doom
{
    public class DoomEpisode
    {
        public string Name { get; set; }

        public int Number { get; set; }

        public List<DoomLevel> Levels { get; set; }
    }
}
