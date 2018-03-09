using LanPartyHub.Models;
using LanPartyHub.Models.Doom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LanPartyHub.Managers
{
    public class DoomManager
    {
        public DoomInfo DoomInfo { get; }

        public DoomManager()
        {
            DoomInfo = GetDoomInfo();
        }

        public List<KeyValue> GetEpisodeView()
        {
            var list = from e in DoomInfo.Games[0].Episodes
                       select new KeyValue
                       {
                           Key = e.Number.ToString(),
                           Value = "Episode " + e.Number + " - " + e.Name
                       };

            return list.ToList();
        }

        public List<KeyValue> GetLevelView(int? episodeNumber)
        {
            var episodes = DoomInfo.Games[0].Episodes;

            if (episodeNumber.HasValue)
            {
                episodes = episodes.Where(x => x.Number == episodeNumber.Value).ToList();
            }

            var list = from e in episodes
                       from l in e.Levels
                       select new KeyValue
                       {
                           Value = "Level " + l.Number + " - " + l.Name,
                           Key = l.Number.ToString()
                       };

            return list.ToList();
        }

        public List<KeyValue> GetSkillLevelView()
        {
            var lvls = DoomInfo.Games[0].SkillLevels;
            var list = from lvl in lvls
                       select new KeyValue
                       {
                           Value = lvl.Name,
                           Key = lvl.Skill
                       };

            return list.ToList();
        }

        public List<int> GetNumberOfPlayers()
        {
            var players = new List<int>();
            for (var i = 1; i < 5; i++)
            {
                players.Add(i);
            }

            return players;
        }

        public string GetDoomArguments(DoomArguments arguments)
        {
            var args = new StringBuilder();

            if (!string.IsNullOrEmpty(arguments.StartLevel))
            {
                args.Append($" -warp {arguments.StartLevel}");
            }

            if(arguments.Multiplayer.HasValue && arguments.Multiplayer.Value)
            {
                if (arguments.Altdeath.HasValue && arguments.Altdeath.Value)
                {
                    args.Append($" -altdeath");
                }

                if (arguments.Deathmath.HasValue && arguments.Deathmath.Value)
                {
                    args.Append($" -deathmatch");
                }

                if (arguments.NumberOfPlayers.HasValue)
                {
                    args.Append($" -nodes {arguments.NumberOfPlayers}");
                }

                if (arguments.Timer.HasValue)
                {
                    args.Append($" -timer {arguments.Timer.Value}");
                }
            }

            if(arguments.Turbo.HasValue && arguments.Turbo.Value)
            {
                args.Append($" -turbo {arguments.TurboPercentage}");
            }

            if (!string.IsNullOrEmpty(arguments.SkillLevel))
            {
                args.Append($" -skill {arguments.SkillLevel}");
            }

            return args.ToString();
        }

        private DoomInfo GetDoomInfo()
        {
            var directory = Directory.GetCurrentDirectory() + @"\Content\DoomLevels.json";

            using (var fs = new FileStream(directory, FileMode.Open))
            {
                using (var r = new StreamReader(fs))
                {
                    var str = new StringBuilder();
                    str.Append(r.ReadToEnd());

                    var doomInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<DoomInfo>(str.ToString());

                    return doomInfo;
                }
            }
        }
    }
}
