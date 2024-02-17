using AuthAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Services.IServices
{
    public interface IAuth
    {
        public Task<Object> Register(RegisterDTO register);
        public Task<Object> Login(LoginDTO loginDto);
        public Task<Object> Unregister();
        public Task<Object> Logout(string token);

    }
}