using LanPartyHub.Enumerations.GameHubConnectivity;
using LanPartyHub.Interfaces;
using Newtonsoft.Json;
using System;
using System.Text;

namespace LanPartyHub.Models
{
    public class GameHubMessage : IGameHubMessage
    {
        private static readonly string EndOfMessageSeperator = "[<end-of-message>]";
        private static readonly string[] EndOfMessageSplitter = new string[] { "[<end-of-message>]" };

        public bool GameStarted { get; set; }
        public int SenderGamePort { get; set; }
        public string Text { get; set; }
        public EMessageType Status { get; set; }

        public GameHubMessage()
        {
        }

        public static byte[] GetBytes(GameHubMessage message)
        {
            var jsonMessage = JsonConvert.SerializeObject(message) + EndOfMessageSeperator;
            var jsonBytes = Encoding.ASCII.GetBytes(jsonMessage);
            return jsonBytes;
        }

        public static string[] GetStrings(byte[] bytes)
        {
            var message = Encoding.ASCII.GetString(bytes);
            var jsonMessage = message.Split(new[] { '\0' }, StringSplitOptions.RemoveEmptyEntries)[0];

            var objectStringArray = jsonMessage.Split(EndOfMessageSplitter, StringSplitOptions.RemoveEmptyEntries);

            return objectStringArray;
        }
    }
}
