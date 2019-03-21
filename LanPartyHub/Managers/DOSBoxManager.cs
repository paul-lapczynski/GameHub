using LanPartyHub.Models;
using LanPartyHub.Models.DOSBox;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
namespace LanPartyHub.Managers
{
    /// <summary>
    /// DOSBox Version - 0.74 - 2
    /// </summary>
    public class DOSBoxManager
    {
        private static readonly string exe = "DOSBox.exe";
        private static readonly string workingDirectory = Directory.GetCurrentDirectory() + @"\Content\DOSBox\";
        private static readonly string DOSBoxC = ApplicationManager.Settings.VirtualDOSBoxCDrivePath;
        private static readonly string DefaultDOSBoxConfigJson = AppDomain.CurrentDomain.BaseDirectory + @"\Content\DOSBoxConfigSettings.json";
        private static readonly string WorkingDOSBoxConfig = AppDomain.CurrentDomain.BaseDirectory + @"\Content\tempConfig.conf";

        public DOSBoxManager()
        {
        }

        public static Process StartDOSBox(DOSBoxOptions options)
        {
            return Process.Start(new ProcessStartInfo
            {
                WorkingDirectory = workingDirectory,
                FileName = exe,
                Arguments = GetDOSBoxArguments(options)
            });
        }

        public static Process GetUnstartedProcess(DOSBoxOptions options)
        {
            return new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = workingDirectory,
                    FileName = exe,
                    Arguments = GetDOSBoxArguments(options)
                }
            };
        }

        private static string GetDOSBoxArguments(DOSBoxOptions options)
        {
            var args = new StringBuilder();

            // Game config options
            if (options.GameOptions != null)
            {
                foreach (KeyValue option in options.GameOptions)
                {
                    args.Append(" -c \"SET " + option.Key + "=" + option.Value + "\" ");
                }

                foreach (KeyValue option in options.GameOptions)
                {
                    if (option.Key == "fullscreen")
                    {
                        if (option.Value == "true")
                        {
                            args.Append(" -fullscreen");
                        }
                    }
                }
            }


            // Fullscreen
            //for (int i = 0; i < options.GameOptions.Count; i++)
            //{
            //    if (options.GameOptions[i].Key == "fullscreen") {
            //        if (options.GameOptions[i].Value == "true") {
            //            args.Append(" -fullscreen");
            //        }
            //    } 
            //}

            // Mount drive and start game
            args.Append(" -c \"C:\"");
            args.Append($"-c \"mount c '{DOSBoxC}\\{options.ExeFolderPath}'\"");
            args.Append(" -c \"C:\"");
            args.Append($"-c \"{options.ExeName} {options.Arguments}\"");

            // auto exit dosbox after exe
            //args.Append(" -c \"exit\"");

            return args.ToString();
        }

        public string BuildDOSBoxConfigFile(DOSBoxConfigSettings defaultSettings, List<DOSBoxSettingOverride> overrideSettings)
        {
            var configFileString =
                from s in defaultSettings.Sections
                let settings =
                    from setting in s.Settings
                    join ljos in (overrideSettings ?? new List<DOSBoxSettingOverride>()) on setting.Name equals ljos.Name
                    into temp
                    from joined in temp.DefaultIfEmpty()
                    select $"{setting.Name}={(joined != null ? joined.SelectedValue : setting.DefaultValue)}"
                select $@"{s.Name}{Environment.NewLine}{string.Join(Environment.NewLine, settings.ToList())}{Environment.NewLine}";

            return string.Join(Environment.NewLine, configFileString);
        }

        public void WriteDOSBoxConfigFile(string configFile)
        {
            File.WriteAllText(WorkingDOSBoxConfig, configFile);
        }

        public bool CreatedConfigFileForGame(List<DOSBoxSettingOverride> overrideSettings)
        {
            var defaultSettings = JsonConvert.DeserializeObject<DOSBoxConfigSettings>(File.ReadAllText(DefaultDOSBoxConfigJson));

            var config = BuildDOSBoxConfigFile(defaultSettings, overrideSettings);

            try
            {
                WriteDOSBoxConfigFile(config);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}