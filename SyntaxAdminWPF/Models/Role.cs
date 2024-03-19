﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxAdminWPF.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = null!;


        public override string ToString()
        {
            return $"{RoleName}";
        }

    }
}
