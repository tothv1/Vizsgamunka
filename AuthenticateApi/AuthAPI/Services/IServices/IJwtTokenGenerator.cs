using AuthAPI.Models;

namespace AtuhenticateApi.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(RegisteredUser registeredUser, IEnumerable<string> roles);
    }
}
