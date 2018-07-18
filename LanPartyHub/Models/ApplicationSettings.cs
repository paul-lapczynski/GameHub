using System.Collections.Generic;

namespace LanPartyHub.Models
{
    public class ApplicationSettings
    {       
        public string VirtualDOSBoxCDrivePath { get; set; }
        public List<DOSBoxSetting> DOSBoxSettings { get; set; }
    }
}
