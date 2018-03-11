using LanPartyHub.Helpers;
using LanPartyHub.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanPartyHub.Managers
{
    static public class ApplicationManager
    {
        static private ApplicationSettings _settings;

        static public ApplicationSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    LoadAppSettings();
                }

                return _settings;
            }

            private set
            {
                _settings = value;
            }
        }

        private static void LoadAppSettings()
        {
            var directory = Directory.GetCurrentDirectory() + @"\Settings.json";
            try
            {
                _settings = JsonFileHelper.ReadAsObject<ApplicationSettings>(directory);
            }
            catch(FileNotFoundException)
            {
                JsonFileHelper.CreateFileFromObject(directory, new ApplicationSettings());
            }
        }
    }
}
