using AuthAPI.Models;
using System.Text.Json.Serialization;

namespace SyntaxBackEnd.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime Regdate { get; set; }
        public virtual UserStat UserStats { get; set; } = null!;

    }
}
