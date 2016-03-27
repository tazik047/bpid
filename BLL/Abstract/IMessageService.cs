using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace BLL.Abstract
{
    public interface IMessageService
    {
        void SendMessage(Message message);

        IEnumerable<Message> GetMessages(string fromId, string toId);
    }
}
