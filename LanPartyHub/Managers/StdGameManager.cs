using LanPartyHub.Models.DOSBox;
using System.Linq;

namespace LanPartyHub.Managers
{
    public class StdGameManager
    {
        private string _gameId;

        public StdGameManager(string gameId)
        {
            _gameId = gameId;
        }
        public DOSBoxOptions GetDOSBoxOptions(StdGameWindow context)
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
