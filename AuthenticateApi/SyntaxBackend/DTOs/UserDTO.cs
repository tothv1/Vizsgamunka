using SyntaxBackEnd.Models;
using System.Text.Json.Serialization;

namespace SyntaxBackEnd.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime Regdate { get; set; }

        [JsonIgnore]
        public virtual Permission Permission { get; set; } = null!;

        public virtual Userstat UserStats { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Userachievement> Userachievements { get; set; } = new List<Userachievement>();

    }
}
