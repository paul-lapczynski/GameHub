using LanPartyHub.Helpers;
using LanPartyHub.Models;
using System;
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
                    LoadGameSettings();
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
            var directory = Directory.GetCurrentDirectory() + @"..\..\..\Games.json";
            try
            {
                JsonFileHelper.CreateFileFromObject(directory, Settings);
            }
            catch
            {
            }
        }

        private static void LoadGameSettings()
        {
            var directory = Directory.GetCurrentDirectory() + @"..\..\..\Games.json";
            var gg = AppDomain.CurrentDomain.BaseDirectory;
            
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
