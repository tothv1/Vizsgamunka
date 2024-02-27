using AuthAPI.Models;
using AuthAPI.Services;
using GameController.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyntaxBackEnd.Models;

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
                var context = new AuthContext();

                var roles = await context.Roles.ToListAsync();

                return Ok(roles);

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
                var gameContext = new GameContext();
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

                var finalGameUser = gameContext.Users.FirstOrDefault(u => u.Email == user.Email)!;

                finalGameUser.Role = gameContext.Roles.FirstOrDefault(r => r.RoleName == requestedRole.RoleName)!;

                gameContext.Update(finalGameUser);
                await gameContext.SaveChangesAsync();

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
