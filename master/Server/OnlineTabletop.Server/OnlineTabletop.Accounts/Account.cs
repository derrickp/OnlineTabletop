﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.Accounts
{
    public class Account
    {
        public string _id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string hash { get; set; }

        public string salt { get; set; }
    }
}
