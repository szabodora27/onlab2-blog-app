using System;

namespace Blog.Dal.Logging
{
    public class EventLog
    {
        public int Id { get; set; }

        public int? EventId { get; set; }

        public string LogLevel { get; set; }

        public string Message { get; set; }

        public DateTime? CreatedTime { get; set; }
    }
}
