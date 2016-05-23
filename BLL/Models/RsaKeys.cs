using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class RsaKeys
    {
        public long SecreteKey { get; set; }

        public long PublicKey { get; set; }

        public long Module { get; set; }
    }
}
