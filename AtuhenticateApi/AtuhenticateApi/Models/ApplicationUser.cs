using Microsoft.AspNetCore.Identity;

namespace AtuhenticateApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public int Age { get; set; }
    }
}
