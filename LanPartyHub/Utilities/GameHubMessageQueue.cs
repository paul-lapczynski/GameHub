using LanPartyHub.Interfaces;
using LanPartyHub.Models;
using LanPartyHub.Models.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanPartyHub.Utilities
{
    public class GameHubMessageQueue
    {
        private List<IGameHubMessage> _queue;

        private readonly object _queueLock = new object();

        public delegate void QueueChange(object sender, QueueChangeEventArgs args);

        public event QueueChange MessagesAdded;

        public GameHubMessageQueue()
        {
            _queue = new List<IGameHubMessage>();
        }

        public void Add(IGameHubMessage message)
        {
            lock (_queueLock)
            {
                _queue.Add(message);
            }
        }

        public void AddRange(List<IGameHubMessage> messages)
        {
            lock (_queueLock)
            {
                _queue.AddRange(messages);
            }
        }

        private List<IGameHubMessage> GetAllMessages()
        {
            List<IGameHubMessage> messages = new List<IGameHubMessage>();

            messages.AddRange(_queue);
            _queue.Clear();

            return messages;
        }

        public void QueueChanged()
        {
            var args = new QueueChangeEventArgs();

            lock (_queueLock)
            {
                args.Messages = GetAllMessages();

                if (args.Messages.Any())
                {
                    MessagesAdded(this, args);
                }
            }
        }
    }
}