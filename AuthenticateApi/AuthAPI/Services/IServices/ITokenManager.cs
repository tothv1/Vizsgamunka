using AuthAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace AuthAPI.Services.IServices
{
    public interface ITokenManager
    {
        public void blackListToken(string token);
        public string GenerateToken(RegisteredUser user);
        //public void RefreshToken(string token);
        public JwtSecurityToken JwtDecode(string token);
    }
}
