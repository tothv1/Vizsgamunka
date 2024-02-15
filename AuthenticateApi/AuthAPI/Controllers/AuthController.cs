using AuthAPI.DTOs;
using AuthAPI.Models;
using AuthAPI.Services;
using AuthAPI.Services.ConfirmationKeyGenerators;
using AuthAPI.Services.PasswordStrengthChecker;
using AuthAPI.Services.SendEmailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto.Generators;

namespace AuthAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ConfirmationKeyGenerator _confirmationKeyGenerator = new ConfirmationKeyGenerator();
        private PasswordStrengthChecker _passwordStrengthChecker = new PasswordStrengthChecker();
        private FropsiEmailSender _emailSender = new FropsiEmailSender();

        [HttpPost("register")]
        public ActionResult<ResponseObject> Register(RegisterDTO register)
        {
            try
            {
                var context = new AuthContext();

                if (!_passwordStrengthChecker.CheckPassword(register.Password))
                {
                    return BadRequest("A jelszavad nem elég erős!");
                }

                if(context.RegisteredUsers.FirstOrDefault(user => user.Username == register.Username) != null)
                {
                    return BadRequest("Ez a felhasználónév már foglalt!");
                }
                if(context.RegisteredUsers.FirstOrDefault(user => user.Email == register.Email) != null)
                {
                    return BadRequest("Ez az email cím már foglalt!");
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
                    TempRoleid = 3,
                    TempConfirmationKey = _confirmationKeyGenerator.GenerateConfirmationKey(register.Email, passwordHash)
                };

                context.Add(registry);
                context.SaveChanges();

                _emailSender.sendMailWithFropsiEmailServer(register.Email, "Megerősítő email",
                    "A fiókját megerősítheti a következő linken:\n" +
                    $"http://localhost:5159/Auth/confirmAccount?confirmKey={registry.TempConfirmationKey}");

                return Ok(new ResponseObject
                {
                    responseMessage = "Sikeres regisztráció",
                    responseObject = registry,
                    status = 200,

                });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseObject
                {
                    responseMessage = ex.Message,
                    responseObject = "error",
                    status = 400,
                });
            }
        }
        [HttpGet("confirmAccount")]
        public ActionResult<ResponseObject> ConfirmEmailAccount([FromQuery] string confirmKey)
        {
            try
            {
                var context = new AuthContext();

                var keyCheck = context.Registries.FirstOrDefault(key => key.TempConfirmationKey.Equals(confirmKey));

                if (keyCheck == null)
                {
                    return BadRequest("Hibás kulcs, vagy nem létező fiók!");
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
                    ConfirmationKeyid = null
                };

                context.Add(RegisteredUser);
                context.SaveChanges();

                context.Registries.Remove(keyCheck);
                context.SaveChanges();

                return Ok(RegisteredUser);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("tempusers")]
        public ActionResult<ResponseObject> GetTempResult()
        {
            try
            {
                var context = new AuthContext();

                return Ok(context.Registries.ToList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }

}
