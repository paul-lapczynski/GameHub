using LanPartyHub.Models.DOSBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanPartyHub.Models
{
    public class DOSBoxConfigSettings
    {
        public string VirtualDOSBoxCDrive { get; set; }

        public List<DOSBoxConfigSection> Sections { get; set; }
    }
}
