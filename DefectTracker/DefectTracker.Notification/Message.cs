﻿namespace DefectTracker.Notification
{
    public class Message
    {
        public string FromName { get; set; }
        public string ToName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
    }
}
