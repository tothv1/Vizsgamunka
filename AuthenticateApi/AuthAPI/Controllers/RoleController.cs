using AuthAPI.Models;
using AuthAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        [HttpGet("roles")]
        public async Task<ActionResult> GetAllRoles()
        {
            try
            {
                var context = new AuthContext();

                var roles = await context.Roles.ToListAsync();

                return Ok(roles);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("users")]
        public async Task<ActionResult> GetAllRolesPerUser()
        {
            try
            {
                var context = new AuthContext();

                var roles = await context.RegisteredUsers.Include(u => u.Role).ToListAsync();

                return Ok(roles);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("assignrole/user")]
        public async Task<ActionResult> AssignRole([FromQuery] string userid, [FromQuery] int role)
        {
            try
            {
                await using var context = new AuthContext();

                var user = await context.RegisteredUsers.FirstOrDefaultAsync(user => user.Userid == userid);

                if (user == null)
                {
                    return Ok(ResponseObject.create("Nincs ilyen felhasználó!", 400));
                }

                var requestedRole = await context.Roles.FirstOrDefaultAsync(r => r.Roleid == role);

                if (requestedRole == null)
                {
                    return Ok(ResponseObject.create("Nincs ilyen szerepkör!", 400));
                }

                if(user.Roleid == requestedRole.Roleid)
                {
                    return Ok(ResponseObject.create("Már rendelkezik a kért szerepkörrel!", 400));
                }

                user.Role = requestedRole;
                context.Update(user);
                await context.SaveChangesAsync();

                return Ok(ResponseObject.create("Sikeres szerepkör adás", 200));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
