using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanPartyHub.Models.DOSBox
{
    public class DOSBoxSettingOverride
    {
        /// <summary>
        /// DOSBox setting
        /// Example: fullscreen 
        /// </summary>
        public string Name { get; set; }

        public string SelectedValue { get; set; }

        /// <summary>
        /// The Section that this DOSBox setting belongs to
        /// Example: "[sdl]"
        /// </summary>
        public string Section { get; set; }
    }
}
