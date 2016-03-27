using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab1_des.Models
{
    public class NotificationUser
    {
        public string Id { get; set; }

        public ConcurrentDictionary<string, byte> ConnectionIds { get; set; } 
    }
}