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
                using var userlist = new GameContext();
                var users = userlist.Users.Include(u => u.Permission).Include(s => s.UserStats).Include(s => s.UserAchievements);

                return Ok(users.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(("Sikertelen lekérdezés: {0}", ex.Message));

            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getUsers/{id}")]
        public ActionResult GetUserById(int id)
        {
            try
            {
                using var userlist = new GameContext();
                var user = userlist.Users.Include(u => u.Permission).Include(s => s.UserStats).Include(s => s.UserAchievements).First(s => s.Id == id);

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

        [Authorize(Roles = "Admin")]
        [HttpPost("addAchievement/{userid}/{achiId}")]
        public ActionResult AddAchievementToUser(int userid, int achiId)
        {
            try
            {
                using var context = new GameContext();

                var user = context.Users.FirstOrDefault(u => u.Id == userid);

                var achievement = context.Achievements.FirstOrDefault(a => a.Id == achiId);

                if (user == null)
                {
                    return Ok("A kért felhasználó nem található.");
                }

                if (achievement == null)
                {
                    return Ok("Az kért teljesítmény nem található.");
                }

                context.Userachievements.Add(new Userachievement
                {
                    UserId = userid,
                    AchievementId = achiId,
                    Achievement = achievement
                });
                context.SaveChanges();

                return Ok("Teljesítmény sikeresen hozzáadva a kért felhasználóhoz.");
            }
            catch (Exception ex)
            {
                return BadRequest(("Sikertelen lekérdezés: {0}", ex.Message));

            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPost("createAchievement")]
        public ActionResult AddAchievementToUser(AchievementDTO achievement)
        {
            try
            {
                using var context = new GameContext();


                if (context.Achievements.Contains(context.Achievements.FirstOrDefault(a => a.AchievementName == achievement.AchievementName)))
                {
                    return BadRequest("Már létezik ilyen nevû teljesítmény");
                }

                context.Add(new Achievement
                {
                    AchievementName = achievement.AchievementName
                });
                context.SaveChanges();

                return Ok(achievement.AchievementName +" teljesítmény sikeresen létrehozva.");
            }
            catch (Exception ex)
            {
                return BadRequest(("Sikertelen lekérdezés: {0}", ex.Message));

            }

        }

    }
}
