using LanPartyHub.Enumerations.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LanPartyHub.Models
{
    public class Game
    {
        /// <summary>
        /// The Folder Path Relative to the Virtual DOSBox C: Drive
        /// </summary>
        public string FolderPath { get; set; }
        
        /// <summary>
        /// Executable name that DOSBox will execute.
        /// Example: DOOM2.EXE
        /// </summary>
        public string ExecutableName { get; set; }

        /// <summary>
        /// Display Name for the Game
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Unique id for each game GameHub contains
        /// </summary>
        public string GameId { get; set; }

        /// <summary>
        /// Image path relative to the GameHub Application Directory
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Startup type for an individual game
        /// </summary>
        public eStartupType StartupType { get; set; }
    }
}
