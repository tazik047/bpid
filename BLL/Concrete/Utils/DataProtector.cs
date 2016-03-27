using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Microsoft.Owin.Security.DataProtection;

namespace BLL.Concrete.Utils
{
    public class DataProtector : IDataProtector
    {
        private readonly string _purpose;

        public DataProtector(string purpose)
        {
            _purpose = purpose;
        }

        public byte[] Protect(byte[] userData)
        {
            return MachineKey.Protect(userData, _purpose);
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            try
            {
                return MachineKey.Unprotect(protectedData, _purpose);
            }
            catch
            {
                return null;
            }
        }
    }
}
