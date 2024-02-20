using AuthAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
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
