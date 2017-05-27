﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldemShared.Security
{
    public class SecurityHandler : ISecurity
    {
        public long GenerateNewSessionId()
        {
            return DateTime.Now.ToFileTime();
        }
    }
}
