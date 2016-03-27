using DAL.Abstract.Repositories;
using DAL.Concrete.EntityFramework;
using Models;

namespace DAL.Concrete.Repositories
{
    internal class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
