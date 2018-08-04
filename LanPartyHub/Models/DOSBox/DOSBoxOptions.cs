using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanPartyHub.Models.DOSBox
{
    public class DOSBoxOptions
    {
        public string ExeName { get; set; }

        public string ExeFolderPath { get; set; }

        public string Arguments { get; set; }

        public bool Fullscreen { get; set; }

        public List<KeyValue> GameOptions { get; set; }
    }
}