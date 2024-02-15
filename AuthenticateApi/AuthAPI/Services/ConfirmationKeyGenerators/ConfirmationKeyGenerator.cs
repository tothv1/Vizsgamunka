using AuthAPI.Services.IServices;

namespace AuthAPI.Services.ConfirmationKeyGenerators
{
    public class ConfirmationKeyGenerator : IConfirmationKeyGenerate
    {
        public string GenerateConfirmationKey(string email, string passwordHash)
        {
            return BCrypt.Net.BCrypt.HashPassword((email + passwordHash), 4);
        }
    }
}
