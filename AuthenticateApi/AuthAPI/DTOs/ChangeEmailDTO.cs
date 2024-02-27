namespace AuthAPI.DTOs
{
    public class ChangeEmailDTO
    {
        public string OldEmail { get; set; } = null!;
        public string NewEmail { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
