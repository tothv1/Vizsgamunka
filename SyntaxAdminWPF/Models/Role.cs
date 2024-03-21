using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxAdminWPF.Models
{
    public class Role
    {
        public int roleid { get; set; }
        public string roleName { get; internal set; }

        public override string ToString()
        {
            return $"{roleName}";
        }

    }
}
