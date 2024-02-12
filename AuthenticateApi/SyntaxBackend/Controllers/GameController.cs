using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyntaxBackEnd.Models;

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
                using var userlist = new SyntaxgameContext();
                var users = userlist.Users.Include(u => u.Permission);

                return Ok(users.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(("Sikertelen lekérdezés: {0}", ex.Message));
                
            }
        }
    }
}
