﻿using AuthAPI.DTOs;
using AuthAPI.Models;
using AuthAPI.Services.ConfirmationKeyGenerators;
using AuthAPI.Services.IServices;
using AuthAPI.Services.PasswordStrengthChecker;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using SyntaxBackEnd.Models;
using System.Linq.Expressions;
using System.Net;
using System.Text.Json.Nodes;

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

        //Fiók megerősítés
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
                context.Registries.Remove(keyCheck);
                await context.SaveChangesAsync();

                var gameController = new GameController.Controllers.GameController();
                var gameContext = new GameContext();

                var userStat = new Userstat()
                {
                    UserStatId = 0,
                    Kills = 0,
                    Deaths = 0,
                    Timesplayed = 0,
                };
                var gameUser = new SyntaxBackEnd.DTOs.UserDTO()
                {
                    Id = keyCheck.TempUserid,
                    Username = keyCheck.TempUsername,
                    Email = keyCheck.TempEmail,
                    Regdate = keyCheck.TempRegdate,
                    UserStats = userStat,
                };

                var res =  await gameController.AddUser(gameUser);

                return ResponseObject.create("Sikeresen megerősítetted a fiókodat, mostmár beléphetsz!", 204);

            }
            catch (Exception ex)
            {
                return ResponseObject.create(ex.Message, 400);
            }
        }

        //Fiók megerősítés ellenőrzés
        public async Task<Object> IsValidKey(string confirmKey)
        {
            try
            {
                var context = new AuthContext();

                var keyCheck = context.Registries.FirstOrDefault(key => key.TempConfirmationKey.Equals(confirmKey));

                if (keyCheck == null)
                {
                    return ResponseObject.create("Hibás kulcs, vagy nem létező fiók!", 400);
                }

                return ResponseObject.create("A megadott kulcs helyes!", keyCheck, 200);

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
                var gameContext = new GameContext();

                await using (var context = new AuthContext())
                {
                    var user = context.RegisteredUsers.FirstOrDefault(user => user.Username == loginDto.UserName);
                    
                    if(user == null)
                    {
                        return ResponseObject.create("Hibás felhasználónév, vagy jelszó!", null!, 400);
                    }

                    if (user!.Hash == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Hash))
                    {
                        return ResponseObject.create("Hibás felhasználónév, vagy jelszó!", null!, 400);
                    }

                    token = _tokenManager.GenerateToken(user);
                    var selectedLoggedInUser = context.LoggedInUsers.FirstOrDefault(u => u.Userid == user.Userid);

                    if (selectedLoggedInUser != null)
                    {
                        var oldToken = selectedLoggedInUser.Token;
                        _tokenManager.blackListToken(oldToken);
                        selectedLoggedInUser.Token = token;
                        context.Update(selectedLoggedInUser);
                    } 
                    else
                    {
                        user.IsLoggedIn = true;
                        await context.AddAsync(new LoggedInUser()
                        {
                            Userid = user.Userid,
                            Token = token
                        });
                    }
                    var gameUser = gameContext.Users.FirstOrDefault(u => u.Email == user.Email);
                    
                    gameUser!.Lastlogin = DateTime.Now;
                    gameContext.Update(gameUser);
                    gameContext.SaveChanges();

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
                    TempConfirmationKey = _tokenManager.GenerateConfirmationToken(new ConfirmationUserDTO { UserId=userId, Email=register.Email, Fullname=register.Fullname, Username = register.Username })
                };


                string message = "A fiókját megerősítheti a következő linken:"+$"http://localhost:3000/confirm/{registry.TempConfirmationKey}";

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

        //Unregister logika
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

                if (!BCrypt.Net.BCrypt.Verify(unregisterDTO.Password, requestedUser!.Hash))
                {
                    return ResponseObject.create("Hibás jelszó!", 400);
                }

                var gameContext = new GameContext();

                var requestedGameUser = gameContext.Users.FirstOrDefault(u => u.Email == requestedUser.Email);

                var requestedStat = gameContext.Userstats.FirstOrDefault(u => u.UserStatId == requestedGameUser!.UserStatsId);

                gameContext!.Remove(requestedStat);
                gameContext!.Remove(requestedGameUser);
                gameContext.SaveChanges();

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