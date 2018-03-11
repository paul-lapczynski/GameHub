using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanPartyHub.Helpers
{
    public static class JsonFileHelper
    {
        public static T ReadAsObject<T>(string path)
        {
            var json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<T>(json);
        }

        public static void CreateFileFromObject<T>(string path, T fromObject)
        {
            var jsonString = JsonConvert.SerializeObject(fromObject);
            File.WriteAllText(path, jsonString);
        }
    }
}
