using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxAdminWPF.Models
{
    public class UpdateUserDTO
    {
        /*"userid": "string",
          "username": "string",
          "fullname": "string",
          "email": "string",
          "regdate": "2024-03-12T09:42:35.305Z",
          "roleid": 0,
          "isLoggedIn": true*/
        public string userid { get; set; } = null!;
        public string fullname { get; set; } = null!;
        public string username { get; set; } = null!;
        public string email { get; set; } = null!;
        public bool isLoggedIn { get; set; }
        public DateTime regdate { get; set; }
        public int roleid { get; set; } = -1;



    }
}
