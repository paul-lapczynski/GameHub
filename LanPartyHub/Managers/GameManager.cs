using LanPartyHub.Helpers;
using LanPartyHub.Models;
using System.IO;

namespace LanPartyHub.Managers
{
    static public class GameManager
    {
        static private GameSettings _settings;

        static public GameSettings Settings
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
            var directory = Directory.GetCurrentDirectory() + @"\Games.json";
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
            var directory = Directory.GetCurrentDirectory() + @"\Games.json";
            
            try
            {
                _settings = JsonFileHelper.ReadAsObject<GameSettings>(directory);
            }
            catch(FileNotFoundException)
            {
                JsonFileHelper.CreateFileFromObject(directory, new GameSettings());
            }
        }
    }
}
