using AuthAPI.DTOs;
using AuthAPI.Models;
using AuthAPI.Services;
using AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace AuthAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IEmailSenderService emailSenderService;
        private IConfirmationKeyGenerate confirmationKeyGenerate;
        private IPasswordManager passwordManager;

        public UserController(IEmailSenderService emailSenderService, IConfirmationKeyGenerate confirmationKeyGenerate, IPasswordManager passwordManager) { 
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
        [HttpGet("users/user")]
        public async Task<ActionResult> GetUserById([FromQuery] string id)
        {
            try
            {
                var context = new AuthContext();

                var selectedUser = await context.RegisteredUsers.Include(u => u.Role).FirstOrDefaultAsync(user => user.Userid == id);

                return Ok(selectedUser);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("resetPasswordRequest")]
        public async Task<ActionResult> ResetPasswordRequest([FromBody]string email)
        {
            try
            {
                var context = new AuthContext();

                var selectedUser = await context.RegisteredUsers.FirstOrDefaultAsync(user => user.Email == email);

                if(selectedUser == null)
                {
                    return Ok("A jelszó visszaállító email elküldve, ha érvényes a megadott email cím!");
                }

                var passwordResetKey = confirmationKeyGenerate.GenerateConfirmationKey(email, selectedUser.Hash);

                string mess = $"Megerősítő kód: <b>{passwordResetKey}</b>";

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

        [HttpPut("changePassword")]
        public async Task<ActionResult> ChangeUserPassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            try
            {
                var context = new AuthContext();

                var selectedUser = await context.RegisteredUsers.FirstOrDefaultAsync(user => user.Email == changePasswordDTO.Email);

                var oldHash = BCrypt.Net.BCrypt.Verify(changePasswordDTO.OldPassword, selectedUser!.Hash);

                if(!oldHash)
                {
                    return BadRequest("A régi jelszó helytelen!");
                }

                if(!changePasswordDTO.NewPassword.Equals(changePasswordDTO.NewPasswordAgain))
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

        [HttpPut("changeEmail")]
        public async Task<ActionResult> ChangeUserEmail([FromBody] ChangeEmailDTO changeEmailDTO)
        {
            try
            {
                var context = new AuthContext();

                var selectedUser = await context.RegisteredUsers.FirstOrDefaultAsync(user => user.Email == changeEmailDTO.OldEmail);

                if(selectedUser == null)
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
                
                if(context.RegisteredUsers.FirstOrDefault(u => u.Email == changeEmailDTO.NewEmail) != null)
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
        [HttpGet("users/status")]
        public async Task<ActionResult> GetAllActiveOrInactive([FromQuery]bool isActive)
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
    }
}
