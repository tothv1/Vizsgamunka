using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyntaxBackEnd.DTOs;
using SyntaxBackEnd.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace GameController.Controllers
{
    [Authorize]
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

                var requestedUsers = context.Users
                    .Include(u => u.Role)
                    .Include(s => s.UserStats)
                    .Include(a => a.UserAchievements)
                    .ThenInclude(a => a.UserAchievementDetails)
                    .ThenInclude(a => a.Achievement);


                return Ok(requestedUsers.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest("Sikertelen lekérdezés: " + ex.Message);

            }
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("getUsers/user")]
        public ActionResult GetUserById(string id)
        {
            try
            {
                using var context = new GameContext();

                var requestedUser = context.Users
                     .Include(u => u.Role)
                     .Include(s => s.UserStats)
                     .Include(a => a.UserAchievements)
                     .ThenInclude(a => a.UserAchievementDetails)
                     .ThenInclude(a => a.Achievement)
                     .FirstOrDefault(user => user.Id == id);

                if (requestedUser == null)
                {
                    return NotFound("A kért felhasználó nem található.");
                }
                return Ok(requestedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(("Sikertelen lekérdezés: {0}", ex.Message));

            }
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("getStats/user")]
        public ActionResult GetUserStatsById(string id)
        {
            try
            {
                using var context = new GameContext();

                var requestedUser = context.Users.FirstOrDefault(u => u.Id == id);

                if (requestedUser == null)
                {
                    return NotFound("A keresett felhasználó nem létezik!");
                }
                var requestedStats = context.Userstats.FirstOrDefault(s => s.UserStatId == requestedUser!.UserStatsId);

                if (requestedUser == null)
                {
                    return Ok("A kért felhasználó nem található.");
                }
                return Ok(requestedStats);
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
                    UserAchievements = []
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

        [Authorize(Roles = "Admin, User")]
        [HttpPut("resetAccount")]
        public async Task<ActionResult> ResetAccount([FromQuery] string userId)
        {
            try
            {

                var context = new GameContext();

                var requestedUser = context.Users.FirstOrDefault(u => u.Id == userId);

                if (requestedUser == null)
                {
                    return NotFound("A kért felhasználó nem található!");
                }

                var requestedStat = context.Userstats.FirstOrDefault(s => s.UserStatId == requestedUser.UserStatsId);

                requestedStat!.Kills = 0;
                requestedStat.Deaths = 0;
                requestedStat.Timesplayed = 0;

                await context.SaveChangesAsync();

                return Ok("Sikeresen resetelted a fiókodat!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("updateAccountStats")]
        public async Task<ActionResult> UpdateUserStat([FromBody] Userstat userstat)
        {
            try
            {

                var context = new GameContext();

                var requestedStat = context.Userstats.FirstOrDefault(u => u.UserStatId == userstat.UserStatId);

                if (requestedStat == null)
                {
                    return NotFound("A kért statisztika nem található!");
                }

                requestedStat!.Kills += userstat.Kills;
                requestedStat.HighestLevel = Math.Max(userstat.HighestLevel, requestedStat.HighestLevel);
                requestedStat.HighestKillCount = Math.Max(userstat.Kills, requestedStat.HighestKillCount);

                requestedStat!.Deaths += 1;
                requestedStat!.Timesplayed += 1;

                context.Update(requestedStat);
                await context.SaveChangesAsync();

                return Ok("Frissültek a statisztikáid!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [Authorize(Roles = "Admin")]
[HttpPut("adminUpdateAccountStats")]
public async Task<ActionResult> AdminUpdateUserStat([FromBody] Userstat userstat)
{
    try
    {

        var context = new GameContext();

        var requestedStat = context.Userstats.FirstOrDefault(u => u.UserStatId == userstat.UserStatId);

        if (requestedStat == null)
        {
            return NotFound("A kért statisztika nem található!");
        }

        requestedStat!.Kills = userstat.Kills;
        requestedStat.HighestLevel = userstat.HighestLevel;
        requestedStat.HighestKillCount = userstat.HighestKillCount;

        requestedStat!.Deaths = userstat.Deaths;
        requestedStat!.Timesplayed = userstat.Timesplayed;

        context.Update(requestedStat);
        await context.SaveChangesAsync();

        return Ok("Sikeresen frissítetted a statisztikákat!");
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
        throw;
    }
}




    }
}
