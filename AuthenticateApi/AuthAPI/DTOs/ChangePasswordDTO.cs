namespace AuthAPI.DTOs
{
    public class ChangePasswordDTO
    {
        public string Email { get; set; } = null!;
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string NewPasswordAgain { get; set; } = null!;
    }
}
