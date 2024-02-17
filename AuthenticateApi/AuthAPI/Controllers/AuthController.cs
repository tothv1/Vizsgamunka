using AuthAPI.DTOs;
using AuthAPI.Models;
using AuthAPI.Services;
using AuthAPI.Services.AuthServices;
using AuthAPI.Services.ConfirmationKeyGenerators;
using AuthAPI.Services.IServices;
using AuthAPI.Services.PasswordStrengthChecker;
using AuthAPI.Services.SendEmailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto.Generators;

namespace AuthAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth authService;

        public AuthController(IAuth authService) 
        { 
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO register)
        {
            try
            {
                var response = await authService.Register(register);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] LoginDTO loginDto)
        {
            try
            {
                var response = await authService.Login(loginDto);

                var jsonResponse = JsonConvert.SerializeObject(response);

                return Ok(jsonResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<ActionResult> LogoutUser([FromBody] string token)
        {
            try
            {
                var response = await authService.Logout(token);

                var jsonResponse = JsonConvert.SerializeObject(response);

                return Ok(jsonResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("confirmAccount")]
        public async Task<ActionResult> ConfirmEmailAccount([FromQuery] string confirmKey)
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
                var jsonResponse = JsonConvert.SerializeObject(RegisteredUser);
                return Ok(jsonResponse);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("tempusers")]
        public async Task<ActionResult> GetTempResult()
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
