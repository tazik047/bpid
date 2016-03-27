using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class NotificationDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string FromId { get; set; }

        public string Text { get; set; }

        public string OriginText { get; set; }

        public string Header { get; set; }

        public string RedirectLink { get; set; }
    }
}
