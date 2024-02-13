using AuthAPI.DTOs;
using AuthAPI.Models;
using AuthAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto.Generators;

namespace AuthAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            ResponseObject responseObject;
            try
            {
                var context = new AuthContext();

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(register.Password);

                var registry = new Registry
                {
                   TempUserid = Guid.NewGuid().ToString(),
                   TempUsername = register.Username,
                   TempFullname = register.Fullname,
                   TempEmail = register.Email,
                   TempHash = passwordHash,
                };

                responseObject = new ResponseObject
                {
                    responseMessage = "Sikeres regisztráció",
                    responseObject = register,
                    status = 200,

                };

                return Ok(responseObject);
            }
            catch (Exception ex)
            {
                responseObject = new ResponseObject
                {
                    responseMessage = ex.Message,
                    responseObject = register,
                    status = 400,
                };

                return BadRequest(responseObject);
            }
        }

    }
}
