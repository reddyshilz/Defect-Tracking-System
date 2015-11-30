using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DefectTracker.Notification;

namespace DefectTracker.Models
{
    public class NotificationMessage 
    {
        public string FromName { get; set; }
        public string ToName { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
    }
}