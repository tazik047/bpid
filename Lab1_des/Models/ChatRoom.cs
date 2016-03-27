using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Lab1_des.Models
{
    public class ChatRoom
    {
        public User Recipient { get; set; }

        public User CurrentUser { get; set; }

        public IEnumerable<MessageViewModel> Messages { get; set; }
    }
}