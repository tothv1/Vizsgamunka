namespace AuthAPI.DTOs
{
    public class RegisterDTO
    {
      
        public string Username { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
