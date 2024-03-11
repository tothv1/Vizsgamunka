using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxAdminWPF.Models
{
    public class User
    {
        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsLoggedIn { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime LastLogin { get; set; }
        public int RoleId { get; set; }
        public int UserStatsId { get; set; }

    }
}
