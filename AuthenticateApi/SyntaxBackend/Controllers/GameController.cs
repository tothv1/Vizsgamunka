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
    public class GameController : ControllerBase
    {

        [Authorize(Roles = "Admin")]
        [HttpGet("getUsers")]
        public ActionResult getUsers()
        {
            try
            {
                using var context = new GameContext();
                var users = context.Users.Include(u => u.Role).Include(s => s.UserStats).Include(s => s.AchievementsConnects);

                return Ok(users.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest("Sikertelen lekérdezés: " +ex.Message);

            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getUsers/{id}")]
        public ActionResult GetUserById(string id)
        {
            try
            {
                using var userlist = new GameContext();
                var user = userlist.Users.Include(u => u.Role).Include(s => s.UserStats).Include(s=> s.AchievementsConnects).First(s => s.Id == id);

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
        public async Task<ActionResult> AddUser(UserDTO userDTO)
        {
            try
            {

                var context = new GameContext();
                var user = new User
                {
                    Id = userDTO.Id,
                    Username = userDTO.Username,
                    Email = userDTO.Email,
                    Regdate = DateTime.Now,
                    Role = context.Roles.First(s => s.RoleName == "User")!,
                    UserStats = userDTO.UserStats,
                    AchievementsConnects = []
                };
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return Ok("Felhasználó létrehozva.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [Authorize(Roles ="Admin, User")]
        [HttpPut("resetAccount")]
        public async Task<ActionResult> ResetAccount([FromQuery]int userStatId)
        {
            try
            {

                var context = new GameContext();
               
                var requestedUser = context.Userstats.FirstOrDefault(u=> u.UserStatId == userStatId);

                await context.SaveChangesAsync();

                return Ok("Sikeresen resetelted a fiókodat!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }


    }
}
