using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanPartyHub.Models
{
    public class ApplicationSettings
    {
        public List<Game> Games { get; set; }
        
        public string VirtualDOSBoxCDrivePath { get; set; }
    }
}
