﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Abstract
{
    public interface IKeyService
    {
        string GenerateSimpleKey();

        RsaKeys GenerateRsaKey();
    }
}
