using AuthAPI.DTOs;
using AuthAPI.Models;
using AuthAPI.Services;
using AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using SyntaxBackEnd.Models;

namespace AuthAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IEmailSenderService emailSenderService;
        private IConfirmationKeyGenerate confirmationKeyGenerate;
        private IPasswordManager passwordManager;

        public UserController(IEmailSenderService emailSenderService, IConfirmationKeyGenerate confirmationKeyGenerate, IPasswordManager passwordManager)
        {
            this.emailSenderService = emailSenderService;
            this.confirmationKeyGenerate = confirmationKeyGenerate;
            this.passwordManager = passwordManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<ActionResult> GetAllUser()
        {
            try
            {
                var context = new AuthContext();

                var users = await context.RegisteredUsers.Include(u => u.Role).ToListAsync();

                return Ok(users);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

         [Authorize(Roles = "Admin, User")]
         [HttpGet("statid/user")]
         public async Task<ActionResult> GetUserStatById([FromQuery] string id)
         {
             try
             {
                 var gameContext = new GameContext();
        
                 var selectedUser = gameContext.Users.Include(s => s.UserStats).FirstOrDefault(s => s.Id == id)!;
        
                 if (selectedUser == null)
                 {
                     return NotFound("A kért felhasználó nem található");
                 }
        
                 return Ok(selectedUser.UserStatsId);
        
             }
             catch (Exception ex)
             {
                 return BadRequest(ex.Message);
             }
         }
        
        [Authorize(Roles = "Admin, User")]
        [HttpGet("users/user")]
        public async Task<ActionResult> GetUserById([FromQuery] string id)
        {
            try
            {
                var context = new AuthContext();

                var selectedUser = await context.RegisteredUsers.Include(u => u.Role).FirstOrDefaultAsync(user => user.Userid == id);

                if (selectedUser == null)
                {
                    return NotFound("A kért felhasználó nem található");
                }

                return Ok(selectedUser);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users/status")]
        public async Task<ActionResult> GetAllActiveOrInactive([FromQuery] bool isActive)
        {
            try
            {
                var context = new AuthContext();

                var users = await context.RegisteredUsers.Include(u => u.Role).Where(u => u.IsLoggedIn == isActive).ToListAsync();

                return Ok(users);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("resetPasswordRequest")]
        public async Task<ActionResult> ResetPasswordRequest([FromBody] string email)
        {
            try
            {
                var context = new AuthContext();

                var selectedUser = await context.RegisteredUsers.FirstOrDefaultAsync(user => user.Email == email);

                if (selectedUser == null)
                {
                    return Ok("A jelszó visszaállító email elküldve, ha érvényes a megadott email cím!");
                }

                var passwordResetKey = confirmationKeyGenerate.GeneratePasswordResetCode();

                string mess = $"Megerősítő kód: {passwordResetKey}";

                if (!emailSenderService.sendMailWithFropsiEmailServer(email, "Jelszó visszaállító", mess))
                {
                    return Ok("A jelszó visszaállító email elküldve, ha érvényes a megadott email cím!");
                }

                selectedUser.ChangePasswordConfirmationKey = passwordResetKey;
                context.Update(selectedUser);
                context.SaveChanges();

                return Ok("A jelszó visszaállító email elküldve, ha érvényes a megadott email cím!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(Roles = "Admin, User")]
        [HttpPut("resetPassword")]
        public async Task<ActionResult> ResetUserPassword([FromBody] string key)
        {
            try
            {
                var context = new AuthContext();

                var selectedUser = await context.RegisteredUsers.FirstOrDefaultAsync(user => user.ChangePasswordConfirmationKey == key);

                if (selectedUser == null)
                {
                    return BadRequest("A megadott kód helytelen!");
                }

                string resetedPassword = passwordManager.generateNewPassword(16);

                emailSenderService.sendMailWithFropsiEmailServer(selectedUser.Email, "Visszaállított jelszó", $"Az új jelszavad: {resetedPassword}");

                var newHashPassword = BCrypt.Net.BCrypt.HashPassword(resetedPassword);

                selectedUser.ChangePasswordConfirmationKey = null;
                selectedUser.Hash = newHashPassword;

                context.Update(selectedUser);
                context.SaveChanges();

                return Ok("Az új jelszavadat elküldtük emailben!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("changePassword")]
        public async Task<ActionResult> ChangeUserPassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            try
            {
                var context = new AuthContext();

                var selectedUser = await context.RegisteredUsers.FirstOrDefaultAsync(user => user.Email == changePasswordDTO.Email);

                var oldHash = BCrypt.Net.BCrypt.Verify(changePasswordDTO.OldPassword, selectedUser!.Hash);

                if (!oldHash)
                {
                    return BadRequest("A régi jelszó helytelen!");
                }

                if (!changePasswordDTO.NewPassword.Equals(changePasswordDTO.NewPasswordAgain))
                {
                    return BadRequest("A két jelszó nem egyezik!");
                }

                var newHashPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordDTO.NewPassword);

                selectedUser.Hash = newHashPassword;

                context.Update(selectedUser);
                context.SaveChanges();

                return Ok("A jelszavad megváltozott!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("changeEmail")]
        public async Task<ActionResult> ChangeUserEmail([FromBody] ChangeEmailDTO changeEmailDTO)
        {
            try
            {
                var context = new AuthContext();

                var selectedUser = await context.RegisteredUsers.FirstOrDefaultAsync(user => user.Email == changeEmailDTO.OldEmail);

                if (selectedUser == null)
                {
                    return BadRequest("Hibás a megadott email!");
                }

                var hashVerify = BCrypt.Net.BCrypt.Verify(changeEmailDTO.Password, selectedUser!.Hash);

                if (!hashVerify)
                {
                    return BadRequest("A megadott jelszó helytelen!");
                }

                if (changeEmailDTO.NewEmail.Equals(selectedUser.Email))
                {
                    return BadRequest("Nem lehet ugyaz az email");
                }

                if (context.RegisteredUsers.FirstOrDefault(u => u.Email == changeEmailDTO.NewEmail) != null)
                {
                    return BadRequest("Ez az email már foglalt!");
                }

                selectedUser.Email = changeEmailDTO.NewEmail;

                context.Update(selectedUser);
                context.SaveChanges();

                return Ok("Az emailed megváltozott!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("updateUser")]
        public async Task<ActionResult> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            try
            {
                var context = new AuthContext();
                var gameContext = new GameContext();

                var selectedUser = await context.RegisteredUsers.FirstOrDefaultAsync(user => user.Userid == updateUserDTO.userid);
                var selectedGameUser = await gameContext.Users.FirstOrDefaultAsync(user => user.Id == updateUserDTO.userid);

                if (selectedUser == null || selectedGameUser == null)
                {
                    return NotFound("A kért felhasználó nem található!");
                }

                if (context.RegisteredUsers.FirstOrDefault(u => u.Email == updateUserDTO.Email) != null && !selectedUser!.Email.Equals(updateUserDTO.Email))
                {
                    return BadRequest("Ez az email már foglalt!");
                }

                if (context.RegisteredUsers.FirstOrDefault(u => u.Username == updateUserDTO.Username) != null && !selectedUser!.Username.Equals(updateUserDTO.Username))
                {
                    return BadRequest("Ez a felhasználónév már foglalt!");
                }

                selectedUser.Email = updateUserDTO.Email;
                selectedUser.Username = updateUserDTO.Username;
                selectedUser.Roleid = updateUserDTO.Roleid;
                selectedUser.Fullname = updateUserDTO.Fullname;
                selectedUser.Regdate = updateUserDTO.Regdate;
                selectedUser.IsLoggedIn = updateUserDTO.IsLoggedIn;

                selectedGameUser.Regdate = updateUserDTO.Regdate;
                selectedGameUser.Username = updateUserDTO.Username;
                selectedGameUser.Email = updateUserDTO.Email;

                context.Update(selectedUser);
                await context.SaveChangesAsync();

                gameContext.Update(selectedGameUser);
                await gameContext.SaveChangesAsync();

                return Ok("A felhasználó szerkesztése sikeresen mgtörtént!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
