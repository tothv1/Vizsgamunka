namespace AuthAPI.DTOs
{
    public class UpdateUserDTO
    {
        public string userid { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime Regdate { get; set; }
        public int Roleid { get; set; }
        public bool IsLoggedIn { get; set; }


    }
}
