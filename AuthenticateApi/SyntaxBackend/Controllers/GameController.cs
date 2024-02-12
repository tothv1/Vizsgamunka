using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}
