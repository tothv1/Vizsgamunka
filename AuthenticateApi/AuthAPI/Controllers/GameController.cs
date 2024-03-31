using AuthAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SyntaxBackEnd.DTOs;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace GameController.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {

        [Authorize(Roles = "Admin, User")]
        [HttpGet("getStats/user")]
        public ActionResult GetUserStatsById(string id)
        {
            try
            {
                using var context = new SyntaxquestContext();

                var requestedUser = context.RegisteredUsers.FirstOrDefault(u => u.Userid == id);

                if (requestedUser == null)
                {
                    return NotFound("A keresett felhasználó nem létezik!");
                }
                var requestedStats = context.UserStats.FirstOrDefault(s => s.Userid == requestedUser!.Userid);

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

        [HttpGet("getTopPlayers")]
        public async Task<ActionResult> GetTopPlayersScoreboard([FromQuery] string statName, [FromQuery] int limit)
        {
            try
            {
                var context = new SyntaxquestContext();

                var requestedTopScoreboard = selectTopPlayers(statName, limit);

                if (requestedTopScoreboard == null)
                {
                    return NotFound("A kért statisztika nem található");
                }

                return Ok(requestedTopScoreboard);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        private List<RegisteredUser>? selectTopPlayers(string type, int limit)
        {
            var context = new SyntaxquestContext();
            var requestedUsers = context.RegisteredUsers.Include(s => s.UserStat).ToList();

            List<RegisteredUser> topPlayers = requestedUsers.OrderByDescending(s => s.UserStat!.GetByKey(type)).ToList();

            if (!requestedUsers[0].UserStat!.IsValidName(type))
            {
                return null;
            }

            return topPlayers.Take(limit).ToList();
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("resetAccount")]
        public async Task<ActionResult> ResetAccount([FromQuery] string userId)
        {
            try
            {

                var context = new SyntaxquestContext();

                var requestedUser = context.RegisteredUsers.FirstOrDefault(u => u.Userid == userId);

                if (requestedUser == null)
                {
                    return NotFound("A kért felhasználó nem található!");
                }

                var requestedStat = context.UserStats.FirstOrDefault(s => s.Userid == requestedUser.Userid);

                requestedStat!.Kills = 0;
                requestedStat.HighestLevel = 0;
                requestedStat.HighestKillCount = 0;
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
        public async Task<ActionResult> UpdateUserStat([FromBody] UserStat userstat)
        {
            try
            {
                var context = new SyntaxquestContext();

                var requestedStat = context.UserStats.FirstOrDefault(u => u.Userid == userstat.Userid);

                if (requestedStat == null)
                {
                    return NotFound("A kért statisztika nem található!");
                }
                userstat.UserStatId = requestedStat.UserStatId;

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
        public async Task<ActionResult> AdminUpdateUserStat([FromBody] UserStat userstat)
        {
            try
            {

                var context = new SyntaxquestContext();

                var requestedStat = context.UserStats.FirstOrDefault(u => u.UserStatId == userstat.UserStatId);

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
