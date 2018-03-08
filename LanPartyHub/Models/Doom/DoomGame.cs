using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanPartyHub.Models.Doom
{
    public class DoomGame
    {
        public string Name { get; set; }

        public List<DoomEpisode> Episodes { get; set; }
    }
}
