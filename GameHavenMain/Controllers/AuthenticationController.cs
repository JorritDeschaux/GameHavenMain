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
        private readonly IApplicationDbContext _context;

		public AuthenticationController(IApplicationDbContext context)
		{
            _context = context;
		}


		[HttpPost("LogIn")]
		[Authorize]
		public async Task<IActionResult> LogIn([FromBody] Login credentials)
		{
            var user = await _context.Users
                    .Where(u => u.Mail == credentials.Mail && u.Password == credentials.Password)
                    .FirstOrDefaultAsync();

            if (user != null)
            {
                Claim[] claims = new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
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

		public async Task<IActionResult> Register()
		{
            return Ok();
		}
	}
}
