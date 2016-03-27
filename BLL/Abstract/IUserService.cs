using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace BLL.Abstract
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();

        User GetUser(string email);

        User GetUserOrDefault(string email);

        void RegisterUser(User user);
    }
}
