using System;

namespace Models
{
    public class Message
    {
        public long Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string FromId { get; set; }

        public virtual User From { get; set; }

        public string ToId { get; set; }

        public virtual User To { get; set; }
    }
}