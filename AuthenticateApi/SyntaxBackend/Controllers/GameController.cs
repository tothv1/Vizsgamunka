using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyntaxBackEnd.DTOs;
using SyntaxBackEnd.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace GameController.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class GameController : ControllerBase
    {

        [Authorize(Roles = "Admin")]
        [HttpGet("getUsers")]
        public ActionResult getUsers()
        {
            try
            {
                using var context = new GameContext();
                var users = context.Users.Include(u => u.Permission).Include(s => s.UserStats).Include(s => s.AchievementsConnects);

                return Ok(users.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest("Sikertelen lekérdezés: " +ex.Message);

            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getUsers/{id}")]
        public ActionResult GetUserById(int id)
        {
            try
            {
                using var userlist = new GameContext();
                var user = userlist.Users.Include(u => u.Permission).Include(s => s.UserStats).Include(s=> s.AchievementsConnects).First(s => s.Id == id);

                if (user == null)
                {
                    return Ok("A kért felhasználó nem található.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(("Sikertelen lekérdezés: {0}", ex.Message));

            }
        }

        [HttpPost("addUser")]
        public ActionResult AddUser(UserDTO userDTO)
        {
            try
            {

                var context = new GameContext();
                var user = new User
                {
                    Username = userDTO.Username,
                    Email = userDTO.Email,
                    Regdate = DateTime.UtcNow,
                    Permission = context.Permissions.First(s => s.PermissionName == "User"),
                    UserStats = userDTO.UserStats,
                    AchievementsConnects = []

                };

                context.Users.Add(user);

                context.SaveChanges();

                return Ok("Felhasználó létrehozva.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

    }
}
