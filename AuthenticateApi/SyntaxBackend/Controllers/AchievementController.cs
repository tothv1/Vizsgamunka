using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SyntaxBackEnd.DTOs;
using SyntaxBackEnd.Models;

namespace SyntaxBackEnd.Controllers
{
    [Authorize]
    [Route("Game")]
    [ApiController]
    public class AchievementController : ControllerBase
    {

        [Authorize(Roles = "Admin, User")]
        [HttpGet("achievements")]
        public ActionResult GetAllAchievements()
        {
            try
            {
                var context = new GameContext();

                var achievements = context.Achievements;

                if(achievements.IsNullOrEmpty())
                {
                    return BadRequest("Jelenleg nincs teljesítmény, vagy valami hiba történt.");
                }

                return Ok(achievements.ToList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("achievements/{userId}")]
        public ActionResult GetUserAchievements(int userId)
        {
            try
            {
                var context = new GameContext();

                var achievements = context.Achievements;

                if (achievements.IsNullOrEmpty())
                {
                    return BadRequest("Jelenleg nincs teljesítmény, vagy valami hiba történt.");
                }

                var userAchievements = context.Userachievements.ToList();

                if (userAchievements.IsNullOrEmpty())
                {
                    return BadRequest("Ennek a felhasználónak nincs teljesítménye, vagy valami hiba történt.");
                }

                return Ok(userAchievements);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addAchievement/{userid}/{achiId}")]
        public ActionResult AddAchievementToUser(string userid, int achiId)
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

                if(context.Userachievements.FirstOrDefault(achi => achi.Achievement == achievement) != null)
                {
                    return BadRequest("Már megszerezted ezt a teljesítményt.");
                }

                context.Userachievements.Add(new Userachievement
                {
                    UserId = userid,
                    AchievementId = achiId,
                    Achievement = achievement,
                    AchievementDate = DateTime.UtcNow,
                    
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
                    return BadRequest("Már létezik ilyen nevű teljesítmény");
                }

                context.Add(new Achievement
                {
                    AchievementName = achievement.AchievementName
                });
                context.SaveChanges();

                return Ok(achievement.AchievementName + " teljesítmény sikeresen létrehozva.");
            }
            catch (Exception ex)
            {
                return BadRequest(("Sikertelen lekérdezés: {0}", ex.Message));

            }

        }
    }
}
