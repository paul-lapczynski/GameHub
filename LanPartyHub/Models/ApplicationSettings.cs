using System.Collections.Generic;

namespace LanPartyHub.Models
{
    // TODO
    public class ApplicationSettings
    {       
        public string VirtualDOSBoxCDrivePath { get; set; }
        public List<DOSBoxSetting> DOSBoxSettings { get; set; }
    }
}
