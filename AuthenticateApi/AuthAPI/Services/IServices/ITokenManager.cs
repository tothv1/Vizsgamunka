using AuthAPI.Models;

namespace AuthAPI.Services.IServices
{
    public interface ITokenManager
    {
        public void blackListToken(string token);
        public string GenerateToken(RegisteredUser user);
    }
}
