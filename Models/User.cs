using System.Collections.Generic;

namespace Models
{
    public class User
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string ContentType { get; set; }

        public virtual ICollection<Message> IncomingMessages { get; set; }

        public virtual ICollection<Message> OutcomingMessages { get; set; }
    }
}