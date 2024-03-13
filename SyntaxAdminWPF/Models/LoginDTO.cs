namespace SyntaxAdminWPF.Models
{
    public class LoginDTO
    {

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public LoginDTO(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public LoginDTO() { }

    }
}