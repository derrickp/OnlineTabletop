﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.DTOs
{
    public class BasicPlayerDTO
    {
        public string id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string email { get; set; }
    }
}
