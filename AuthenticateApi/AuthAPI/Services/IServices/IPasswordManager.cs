namespace AuthAPI.Services.IServices
{
    public interface IPasswordManager
    {
        public bool CheckPassword(string password);

        public string generateNewPassword(int length);
        bool PasswordMatch(string password, string passwordRepeate);
    }
}
