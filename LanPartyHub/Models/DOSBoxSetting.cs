using LanPartyHub.Enumerations.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LanPartyHub.Models
{
    public class DOSBoxSetting
    {
        /// <summary>
        /// DOSBox setting
        /// Example: fullscreen 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Available option values to set the DOSBox setting
        /// Example: "true","false" 
        /// </summary>
        public List<string> Values { get; set; }

        /// <summary>
        /// Desription of the DOSBox setting
        /// </summary>
        public string Description { get; set; }
    }
}
