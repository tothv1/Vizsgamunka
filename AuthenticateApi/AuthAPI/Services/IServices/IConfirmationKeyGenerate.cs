namespace AuthAPI.Services.IServices
{
    public interface IConfirmationKeyGenerate
    {
        string GenerateConfirmationKey(string email, string passwordHash);

        string GeneratePasswordResetCode();
    }
}
