using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Lab1_des.Models
{
    public class MessageViewModel
    {
        public string Text { get; set; }

        public bool IsForMe { get; set; }

        public User Sender { get; set; }

        public DateTime Date { get; set; }
    }
}