using AuthAPI.DTOs;
using AuthAPI.Models;
using AuthAPI.Services.ConfirmationKeyGenerators;
using AuthAPI.Services.IServices;
using AuthAPI.Services.PasswordStrengthChecker;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Services.AuthServices
{
    public class AuthService : IAuth
    {

        private readonly ITokenManager _tokenManager;
        private readonly IPasswordStrengthChecker _passwordStrengthChecker;
        private readonly IConfirmationKeyGenerate _confirmationKeyGenerate;
        private readonly IEmailSenderService _emailSenderService;

        public AuthService(ITokenManager tokenManager, IPasswordStrengthChecker passwordStrengthChecker, IConfirmationKeyGenerate confirmationKeyGenerate, IEmailSenderService emailSenderService)
        {
            this._tokenManager = tokenManager;
            this._passwordStrengthChecker = passwordStrengthChecker;
            this._confirmationKeyGenerate = confirmationKeyGenerate;
            this._emailSenderService = emailSenderService;
        }

        public async Task<Object> Login(LoginDTO loginDto)
        {
            try
            {
                var token = "";

                await using (var context = new AuthContext())
                {
                    var user = context.RegisteredUsers.FirstOrDefault(user => user.Username == loginDto.UserName);

                    if (user == null)
                    {
                        return ResponseObject.create("Hibás felhasználónév, vagy jelszó!", null!, 400);
                    }

                    if (user.Hash == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Hash))
                    {
                        return ResponseObject.create("Hibás felhasználónév, vagy jelszó!", null!, 400);
                    }
                    token = _tokenManager.GenerateToken(user);
                }

                return ResponseObject.create("Sikeres bejelentkezés", token, 200);
            }
            catch (Exception ex)
            {

                return ResponseObject.create("Hibás felhasználónév, vagy jelszó!", ex.Message, 400);
            }
        }

        public async Task<object> Logout(string token)
        {
            try
            {
                _tokenManager.blackListToken(token);
                return ResponseObject.create("Hibás felhasználónév, vagy jelszó!", "Sikeresen kijelentkeztél!", 400);
            }
            catch (Exception ex)
            {
                return ResponseObject.create("Hibás token!", ex.Message, 400);
            }
        }

        public async Task<Object> Register(RegisterDTO register)
        {
            try
            {
                var context = new AuthContext();

                if (!_passwordStrengthChecker.CheckPassword(register.Password))
                {
                    return ResponseObject.create("Nem elég erős a jelszó!", "null pass", 400);
                }
                if (context.RegisteredUsers.FirstOrDefault(user => user.Username == register.Username) != null)
                {
                    return ResponseObject.create("Ez a felhasználónév már foglalt!", "null user", 400);
                }
                if (context.RegisteredUsers.FirstOrDefault(user => user.Email == register.Email) != null)
                {
                    return ResponseObject.create("Ez az email cím már foglalt!", "null email", 400);
                }
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(register.Password);
                string userId = Guid.NewGuid().ToString();

                var registry = new Registry
                {
                    TempUserid = userId,
                    TempUsername = register.Username,
                    TempFullname = register.Fullname,
                    TempEmail = register.Email,
                    TempHash = passwordHash,
                    TempRegdate = DateTime.UtcNow,
                    TempRoleid = 1,
                    TempConfirmationKey = _confirmationKeyGenerate.GenerateConfirmationKey(register.Email, passwordHash)
                };

                context.Add(registry);
                context.SaveChanges();

                _emailSenderService.sendMailWithFropsiEmailServer(register.Email, "Megerősítő email",
                    "A fiókját megerősítheti a következő linken:\n" +
                    $"http://localhost:5159/Auth/confirmAccount?confirmKey={registry.TempConfirmationKey}");

                return ResponseObject.create("Sikeres regisztráció", registry, 200);
            }
            catch (Exception ex)
            {
                return ResponseObject.create("Sikertelen regisztráció!", ex.Message, 400);
            }
        }

        public Task<Object> Unregister()
        {
            throw new NotImplementedException();
        }
    }
}