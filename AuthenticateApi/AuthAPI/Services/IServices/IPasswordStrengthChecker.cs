namespace AuthAPI.Services.IServices
{
    public interface IPasswordStrengthChecker
    {
        public bool CheckPassword(string password);
    }
}
