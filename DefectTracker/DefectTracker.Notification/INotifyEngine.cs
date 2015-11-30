using System.Collections.Generic;

namespace DefectTracker.Notification
{
    public interface INotifyEngine
    {
        void SendNotification(Message message);
    }
}