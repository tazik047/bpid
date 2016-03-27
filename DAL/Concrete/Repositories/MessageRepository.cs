using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstract.Repositories;
using DAL.Concrete.EntityFramework;
using Models;

namespace DAL.Concrete.Repositories
{
    internal class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(DbContext context) : base(context)
        {
        }
    }
}
