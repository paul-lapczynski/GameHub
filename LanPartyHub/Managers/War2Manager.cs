using LanPartyHub.Models.DOSBox;
using System.Linq;

namespace LanPartyHub.Managers
{
    public class War2Manager
    {
        private int _gameId;

        public War2Manager(int gameId)
        {
            _gameId = gameId;
        }
        public DOSBoxOptions GetDOSBoxOptions(War2Window context)
        {
            var gameSettings = ApplicationManager.Settings.Games.First(x => x.GameId == _gameId);

            return new DOSBoxOptions
            {
                ExeFolderPath = gameSettings.FolderPath,
                ExeName = gameSettings.ExecutableName,
                Fullscreen = true
            };
        }
    }
}
