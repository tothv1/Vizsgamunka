using AuthAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SyntaxBackEnd.DTOs;

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
                var context = new SyntaxquestContext();

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
                var context = new SyntaxquestContext();

                var achievements = context.Achievements;

                if (achievements.IsNullOrEmpty())
                {
                    return NotFound("Jelenleg nincs teljesítmény, vagy valami hiba történt.");
                }


                var userAchievements = context.UserAchievements
                    .Include(u => u.Achievement)
                    .Where(s => s.Userid == userId)
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
                using var context = new SyntaxquestContext();

                var user = context.RegisteredUsers.FirstOrDefault(u => u.Userid == userId);

                var achievement = context.Achievements.FirstOrDefault(a => a.AchievementId == achiId);

                if (user == null)
                {
                    return NotFound("A kért felhasználó nem található.");
                }

                if (achievement == null)
                {
                    return NotFound("Az kért teljesítmény nem található.");
                }

                if (context.UserAchievements.FirstOrDefault(achi => achi.Achievement == achievement) != null)
                {
                    return BadRequest("Már megszerezted ezt a teljesítményt.");
                }

                context.Add(new UserAchievement
                {
                    UserAchievementId = 0,
                    AchievementId = achiId,
                    Userid = user.Userid,
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
                using var context = new SyntaxquestContext();


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


        [Authorize(Roles = "Admin")]
        [HttpPut("updateAchievement")]
        public ActionResult UpdateAchievement(Achievement achievement)
        {
            try
            {
                using var context = new SyntaxquestContext();

                var requestAchi = context.Achievements.FirstOrDefault(a => a.AchievementId == achievement.AchievementId);

                if (context.Achievements.FirstOrDefault(a => a.AchievementName == achievement.AchievementName) != null && !requestAchi.AchievementName.Equals(achievement.AchievementName))
                {
                    return BadRequest("Már létezik ilyen nevű teljesítmény");
                }

                requestAchi!.AchievementName = achievement.AchievementName;
                requestAchi.UserAchievements = achievement.UserAchievements;
                requestAchi.AchievementId = achievement.AchievementId;

                context.Update(requestAchi);
                context.SaveChanges();

                return Ok("A teljesítmény sikeresen frissítve!");
            }
            catch (Exception ex)
            {
                return BadRequest(("Sikertelen lekérdezés: {0}", ex.Message));

            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteAchievement")]
        public ActionResult DeleteAchievement(int id)
        {
            try
            {
                using var context = new SyntaxquestContext();

                var requestAchi = context.Achievements.FirstOrDefault(a => a.AchievementId == id);

                if (requestAchi == null)
                {
                    return NotFound("A keresett eredmény nem található!");
                }

                context.Remove(requestAchi);
                context.SaveChanges();

                return Ok("A teljesítmény sikeresen törölve!");
            }
            catch (Exception ex)
            {
                return BadRequest(("Sikertelen lekérdezés: {0}", ex.Message));

            }

        }
    }
}
