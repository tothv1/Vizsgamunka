using AuthAPI.Services.IServices;
using System.Text;

namespace AuthAPI.Services.ConfirmationKeyGenerators
{
    public class ConfirmationKeyGenerator : IConfirmationKeyGenerate
    {
        public string GenerateConfirmationKey(string email, string passwordHash)
        {
            return BCrypt.Net.BCrypt.HashPassword((email + passwordHash), 4);
        }

        public string GeneratePasswordResetCode()
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            sb.Append(random.Next(1000, 9999));

            return sb.ToString();
        }
    }
}
