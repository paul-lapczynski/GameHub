using LanPartyHub.Models;
using LanPartyHub.Models.DOSBox;
using System.Diagnostics;
using System.IO;
using System.Text;
namespace LanPartyHub.Managers
{
    /// <summary>
    /// DOSBox Version - 0.74
    /// </summary>
    public class DOSBoxManager
    {
        private static readonly string exe = "DOSBox.exe";
        private static readonly string workingDirectory = Directory.GetCurrentDirectory() + @"\Content\DOSBox\";
        private static readonly string DOSBoxC = ApplicationManager.Settings.VirtualDOSBoxCDrivePath;

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
            if(options.GameOptions != null)
            {
                foreach (KeyValue option in options.GameOptions)
                {
                    args.Append(" -c \"SET " + option.Key + "=" + option.Value + "\" ");
                }

                foreach(KeyValue option in options.GameOptions)
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
    }
}