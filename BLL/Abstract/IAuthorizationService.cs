using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IAuthorizationService
    {
        bool Login(string code, string pathToFolder, string redirectUrl);

        void Logout();
    }
}
