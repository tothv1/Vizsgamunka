using AuthAPI.Models;
using AuthAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("roles")]
        public async Task<ActionResult> GetAllRoles()
        {
            try
            {
                var context = new SyntaxquestContext();

                var roles = await context.Roles.ToListAsync();

                return Ok(roles);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPost("createRole")]
        public async Task<ActionResult> CreateNewRole([FromBody]Models.Role role)
        {
            try
            {
                
                await using var context = new SyntaxquestContext();

                var requestedRole = await context.Roles.FirstOrDefaultAsync(r => r.RoleName == role.RoleName);

                if (requestedRole != null)
                {
                    return NotFound(ResponseObject.create("Már létezik ilyen szerepkör!", 400));
                }

                
                context.Add(new Models.Role { Roleid = 0, RoleName = role.RoleName });
                await context.SaveChangesAsync();

                return Ok(ResponseObject.create("Sikeresen létrehoztad a szerepkört!", 200));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("assignrole/user")]
        public async Task<ActionResult> AssignRole([FromQuery] string userid, [FromQuery] int role)
        {
            try
            {
                await using var context = new SyntaxquestContext();

                var user = await context.RegisteredUsers.FirstOrDefaultAsync(user => user.Userid == userid);

                if (user == null)
                {
                    return NotFound(ResponseObject.create("Nincs ilyen felhasználó!", 400));
                }

                var requestedRole = await context.Roles.FirstOrDefaultAsync(r => r.Roleid == role);

                if (requestedRole == null)
                {
                    return NotFound(ResponseObject.create("Nincs ilyen szerepkör!", 400));
                }

                if(user.Roleid == requestedRole.Roleid)
                {
                    return Ok(ResponseObject.create("Már rendelkezik a kért szerepkörrel!", 400));
                }

                var finalGameUser = context.RegisteredUsers.FirstOrDefault(u => u.Email == user.Email)!;

                finalGameUser.Role = context.Roles.FirstOrDefault(r => r.RoleName == requestedRole.RoleName)!;

                context.Update(finalGameUser);
                await context.SaveChangesAsync();

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

        [Authorize(Roles = "Admin")]
        [HttpPut("updateRole")]
        public async Task<ActionResult> UpdateRole([FromBody] Models.Role role)
        {
            try
            {
                var context = new SyntaxquestContext();
                var requestRole = context.Roles.FirstOrDefault(r => r.Roleid == role.Roleid);

                if (requestRole == null)
                {
                    return NotFound("A kért szerepkör nem létezik!");
                }

                requestRole.RoleName = role.RoleName;
                requestRole.RegisteredUsers = role.RegisteredUsers.ToList();
                requestRole.Roleid = role.Roleid;

                context.Update(requestRole);
                await context.SaveChangesAsync();

                return Ok("Sikeresen szerkesztetted a szerepkört!");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteRole")]
        public async Task<ActionResult> DeleteRole([FromQuery] int roleId)
        {
            try
            {
                var context = new SyntaxquestContext();
                var requestRole = context.Roles.FirstOrDefault(r => r.Roleid == roleId);

                if (requestRole == null)    {
                    return NotFound("A kért szerepkör nem létezik!");
                }
                context.Remove(requestRole);
                await context.SaveChangesAsync();
                return Ok("Sikeresen törölted a kívánt a szerepkört!");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
