using System.Security.Claims;

namespace AuthAPI.Models
{
    public class DevUser
    {
        public String? Username { get; set; }


        public DevUser(String? username)
        {
            this.Username = username; 
        }

    }
}