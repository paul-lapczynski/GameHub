using LanPartyHub.Helpers;
using LanPartyHub.Models;
using System.IO;

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

        static public void SaveSettings()
        {
            var directory = Directory.GetCurrentDirectory() + @"..\..\..\Settings.json";
            try
            {
                JsonFileHelper.CreateFileFromObject(directory, Settings);
            }
            catch
            {
                
            }
        }

        private static void LoadAppSettings()
        {
            var directory = Directory.GetCurrentDirectory() + @"..\..\..\Settings.json";
            
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
