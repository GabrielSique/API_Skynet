﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Intefaces
{
    public interface ICryptoService
    {
        string Decode(string data);
        string Encode(string data);
    }
}
