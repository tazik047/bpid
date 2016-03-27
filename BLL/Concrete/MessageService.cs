using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Abstract;
using DAL.Abstract.UnitOfWork;
using Models;

namespace BLL.Concrete
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SendMessage(Message message)
        {
            _unitOfWork.MessageRepository.Create(message);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<Message> GetMessages(string fromId, string toId)
        {
            return _unitOfWork.MessageRepository.Get(m => m.FromId == fromId && m.ToId == toId);
        }
    }
}
