using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab1_des.Models
{
    public class RSAKeysViewModel
    {
        public long SecreteKey { get; set; }

        public long PublicKey { get; set; }

        public long Module { get; set; }
    }
}