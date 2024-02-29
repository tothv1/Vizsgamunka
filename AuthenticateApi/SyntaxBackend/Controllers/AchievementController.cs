using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

                if (achievements.IsNullOrEmpty())
                {
                    return NotFound("Jelenleg nincs teljesítmény, vagy valami hiba történt.");
                }

                return Ok(achievements.ToList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("achievements/user")]
        public ActionResult GetUserAchievements(string userId)
        {
            try
            {
                var context = new GameContext();

                var achievements = context.Achievements;

                if (achievements.IsNullOrEmpty())
                {
                    return NotFound("Jelenleg nincs teljesítmény, vagy valami hiba történt.");
                }


                var userAchievements = context.UserAchievementDetails
                    .Include(s => s.UserAchievement)
                    .Include(u => u.Achievement)
                    .Where(s => s.UserAchievement.Userid == userId)
                    .ToList();

                if (userAchievements == null)
                {
                    return NotFound("Nincs ilyen felhasználó!");
                }

                if (userAchievements.IsNullOrEmpty())
                {
                    return NotFound("Ennek a felhasználónak nincs teljesítménye, vagy valami hiba történt.");
                }

                return Ok(userAchievements);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addAchievement/user")]
        public ActionResult AddAchievementToUser(string userId, int achiId)
        {
            try
            {
                using var context = new GameContext();

                var user = context.Users.FirstOrDefault(u => u.Id == userId);

                var achievement = context.Achievements.FirstOrDefault(a => a.Id == achiId);

                if (user == null)
                {
                    return NotFound("A kért felhasználó nem található.");
                }

                if (achievement == null)
                {
                    return NotFound("Az kért teljesítmény nem található.");
                }

                if(context.UserAchievementDetails.FirstOrDefault(achi => achi.Achievement == achievement) != null)
                {
                    return BadRequest("Már megszerezted ezt a teljesítményt.");
                }

                var userAchievement = new UserAchievement
                {
                    AchievementId = 0,
                    Userid = userId,
                };

                context.Add(new UserAchievementDetail
                {
                    AchievementDetailId = 0,
                    AchievementId = achiId,
                    UserAchievement = userAchievement,
                    AchievementDate = DateTime.Now,
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
                    return NotFound("Már létezik ilyen nevű teljesítmény");
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
