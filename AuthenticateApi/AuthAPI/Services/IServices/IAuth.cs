using AuthAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Services.IServices
{
    public interface IAuth
    {
        public Task<Object> Register(RegisterDTO register);
        public Task<Object> Login(LoginDTO loginDto);
        public Task<Object> ConfirmAccount(string confirmKey);
        public Task<Object> IsValidKey(string confirmKey);
        public Task<Object> Unregister(UnregisterDTO unregisterDTO);
        public Task<Object> DeleteUser(string userId);
        public Task<Object> Logout(string token);

    }
}