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
        private readonly IPasswordManager _passwordStrengthChecker;
        private readonly IConfirmationKeyGenerate _confirmationKeyGenerate;
        private readonly IEmailSenderService _emailSenderService;

        public AuthService(ITokenManager tokenManager, IPasswordManager passwordStrengthChecker, IConfirmationKeyGenerate confirmationKeyGenerate, IEmailSenderService emailSenderService)
        {
            this._tokenManager = tokenManager;
            this._passwordStrengthChecker = passwordStrengthChecker;
            this._confirmationKeyGenerate = confirmationKeyGenerate;
            this._emailSenderService = emailSenderService;
        }

        public async Task<Object> ConfirmAccount(string confirmKey)
        {
            try
            {
                var context = new AuthContext();

                var keyCheck = context.Registries.FirstOrDefault(key => key.TempConfirmationKey.Equals(confirmKey));

                if (keyCheck == null)
                {
                    return ResponseObject.create("Hibás kulcs, vagy nem létező fiók!", 400);
                }

                var RegisteredUser = new RegisteredUser
                {
                    Userid = keyCheck!.TempUserid,
                    Email = keyCheck.TempEmail,
                    Username = keyCheck.TempUsername,
                    Fullname = keyCheck.TempFullname,
                    Hash = keyCheck.TempHash,
                    Regdate = keyCheck.TempRegdate,
                    Roleid = 2,
                };

                await context.AddAsync(RegisteredUser);
                await context.SaveChangesAsync();

                context.Registries.Remove(keyCheck);
                await context.SaveChangesAsync();

                return ResponseObject.create("Sikeresen megerősítetted a fiókodat, mostmár beléphetsz!", 204);

            }
            catch (Exception ex)
            {
                return ResponseObject.create(ex.Message, 400);
            }
        }

        //Bejelentkezés logika
        public async Task<Object> Login(LoginDTO loginDto)
        {
            try
            {
                var token = "";

                await using (var context = new AuthContext())
                {
                    var user = context.RegisteredUsers.FirstOrDefault(user => user.Username == loginDto.UserName);

                    if (user!.Hash == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Hash))
                    {
                        return ResponseObject.create("Hibás felhasználónév, vagy jelszó!", null!, 400);
                    }

                    if (user!.IsLoggedIn)
                    {
                        await Logout(context.LoggedInUsers.FirstOrDefault(u => u.Userid == user.Userid)!.Token);
                        return ResponseObject.create("Már be vagy jelentkezve egy másik gépen, minden egyéb eszközön kijelentkeztetünk!", 400);
                    }

                    token = _tokenManager.GenerateToken(user);

                    user.IsLoggedIn = true;
                    context.Update(user);

                    await context.AddAsync(new LoggedInUser()
                    {
                        Userid = user.Userid,
                        Token = token
                    });
                    await context.SaveChangesAsync();

                }

                return ResponseObject.create("Sikeres bejelentkezés", token, 200);
            }
            catch (Exception ex)
            {
                return ResponseObject.create("Hibás felhasználónév, vagy jelszó!", ex.Message, 400);
            }
        }


        //Kijelentkezés logika
        public async Task<object> Logout(string token)
        {
            try
            {
                _tokenManager.blackListToken(token);

                await using (var context = new AuthContext())
                {
                    var loggedInUser = context.LoggedInUsers.FirstOrDefault(user => user.Token == token);

                    if (loggedInUser == null)
                    {
                        return ResponseObject.create("Nem vagy bejelentkezve!", null!, 400);
                    }
                    var currentUser = context.RegisteredUsers.FirstOrDefault(user => user.Userid == loggedInUser.Userid);

                    if(currentUser == null)
                    {
                        return ResponseObject.create("Nem vagy bejelentkezve!", null!, 400);
                    }
                    currentUser!.IsLoggedIn = false;
                    context.Update(currentUser);

                    context.Remove(loggedInUser);
                    await context.SaveChangesAsync();
                }

                return ResponseObject.create("Sikeresen kijelentkeztél!", null!, 200);
            }
            catch (Exception ex)
            {
                return ResponseObject.create("Hibás token!", ex.Message, 400);
            }
        }

        //Regisztrációs logika
        public async Task<Object> Register(RegisterDTO register)
        {
            try
            {
                var context = new AuthContext();

                if (!_passwordStrengthChecker.CheckPassword(register.Password))
                {
                    return ResponseObject.create("Nem elég erős a jelszó!", "null pass", 400);
                }
                if (context.Registries.FirstOrDefault(user => user.TempUsername == register.Username) != null || context.RegisteredUsers.FirstOrDefault(user => user.Username == register.Username) != null)
                {
                    return ResponseObject.create("Ez a felhasználónév már foglalt!", "null user", 400);
                }
                if (!_emailSenderService.isValidEmail(register.Email))
                {
                    return ResponseObject.create("Érvényes emailt adj meg!", "null email", 400);
                }
                if (context.Registries.FirstOrDefault(user => user.TempEmail == register.Email) != null || context.RegisteredUsers.FirstOrDefault(user => user.Email == register.Email) != null)
                {
                    return ResponseObject.create("Ez az email cím már foglalt!", "null email", 400);
                }
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(register.Password);
                string userId = Guid.NewGuid().ToString();

                DateTime expireDate = DateTime.UtcNow;

                var registry = new Registry
                {
                    TempUserid = userId,
                    TempUsername = register.Username,
                    TempFullname = register.Fullname,
                    TempEmail = register.Email,
                    TempHash = passwordHash,
                    TempRegdate = DateTime.UtcNow,
                    TempRoleid = 1,
                    TempUserExpire = expireDate.AddHours(24),
                    TempConfirmationKey = _confirmationKeyGenerate.GenerateConfirmationKey(register.Email, passwordHash)
                };


                string message = "A fiókját megerősítheti a következő linken:"+$"http://localhost:5159/Auth/confirmAccount?confirmKey={registry.TempConfirmationKey}";

                if (!_emailSenderService.sendMailWithFropsiEmailServer(register.Email, "Megerősítő email", message)) {
                    return ResponseObject.create("Erre az emailre nem tudunk levelet küldeni!", "null email", 400);
                }

                await context.AddAsync(registry);
                await context.SaveChangesAsync();

                return ResponseObject.create("Sikeres regisztráció", registry, 200);
            }
            catch (Exception ex)
            {
                return ResponseObject.create("Sikertelen regisztráció!", ex.Message, 400);
            }
        }

        public async Task<object> Unregister(UnregisterDTO unregisterDTO)
        {
            try
            {
                await using var context = new AuthContext();

                var requestedUser = context.RegisteredUsers.FirstOrDefault(u => u.Email == unregisterDTO.Email);

                if (requestedUser == null)
                {
                    return ResponseObject.create("Hibás email", 400);
                }

                var userVerify = context.LoggedInUsers.FirstOrDefault(r => r.Userid == requestedUser.Userid);

                if (userVerify == null)
                {
                    return ResponseObject.create("Hibás email", 400);
                }

                if (!BCrypt.Net.BCrypt.Verify(unregisterDTO.Password, requestedUser!.Hash))
                {
                    return ResponseObject.create("Hibás jelszó!", 400);
                }

                context.Remove(requestedUser);
                await context.SaveChangesAsync();

                return ResponseObject.create("Sikeresen törölted a felhasználódat!", 200);
            }
            catch (Exception ex)
            {
                return ResponseObject.create("Sikertelen felhasználó törlés!", ex.Message, 400);
            }
        }
    }
}