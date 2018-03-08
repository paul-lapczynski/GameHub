using LanPartyHub.Models.DOSBox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanPartyHub.Managers
{
    /// <summary>
    /// DOSBox Version - 0.74
    /// </summary>
    public class DOSBoxManager
    {
        private static readonly string exe = "DOSBox.exe";
        private static readonly string workingDirectory = Directory.GetCurrentDirectory() + @"\Content\DOSBox\";
        private static readonly string DOSBoxC = "D:\\Games\\DOSBoxC\\";

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

        private static string GetDOSBoxArguments(DOSBoxOptions options)
        {
            var args = new StringBuilder($"-c \"mount c '{DOSBoxC}\\{options.ExeFolderPath}'\"");
            args.Append(" -c \"C:\"");
            args.Append($"-c \"{options.ExeName} {options.Arguments}\"");

            if (options.Fullscreen)
            {
                args.Append(" -fullscreen");
            }

            return args.ToString();
        }
    }
}