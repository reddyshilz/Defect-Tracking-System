using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectTracker.Notification
{
    public abstract class NotificationSystem
    {
        private readonly List<INotifyEngine> _notifyEngines;

        public Message Message { get; set; }

        protected NotificationSystem()
        {
            _notifyEngines = new List<INotifyEngine>();
        }

        // Constructor

        public void AttachEngine(INotifyEngine engine)
        {
            _notifyEngines.Add(engine);
        }

        public void DetachEngine(INotifyEngine engine)
        {
            _notifyEngines.Remove(engine);
        }

        public void Send()
        {
            foreach (var engine in _notifyEngines)
            {
                engine.SendNotification(this.Message);
            }
        }

    }
}
