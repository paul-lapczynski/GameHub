using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanPartyHub.Models.DOSBox
{
    public class DOSBoxConfigSection
    {
        public string Name { get; set; }

        public List<DOSBoxSetting> Settings { get; set; }
    }
}
