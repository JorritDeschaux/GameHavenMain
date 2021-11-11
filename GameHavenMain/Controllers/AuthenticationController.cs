using GameHavenMain.Data;
using GameHavenMain.Data.Interfaces;
using GameHavenMain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameHavenMain.Controllers
{
	public class AuthenticationController : Controller
	{
        private readonly ApplicationDbContext _context;

		public AuthenticationController(ApplicationDbContext context)
		{
            _context = context;
		}


		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] Login credentials)
		{
            
            var user = await _context.User
                    .Where(u => u.Email == credentials.Mail && u.Password == credentials.Password)
                    .FirstOrDefaultAsync();

            if (user != null)
            {
                Claim[] claims = new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.FirstName)
                };

                var token = TokenHelper.CreateToken(claims);

                return Ok(TokenHelper.WriteToken(token));
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register()
		{
            return Ok();
		}
	}
}
