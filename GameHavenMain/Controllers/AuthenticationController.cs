using GameHavenMain.Data;
using GameHavenMain.Data.DTO;
using GameHavenMain.Data.Interfaces;
using GameHavenMain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameHavenMain.Controllers
{
	[Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : Controller
	{
		private readonly IUserRepo _repo;

		public AuthenticationController(IUserRepo repo)
		{
            _repo = repo;
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] Login credentials)
		{

            UserDTO loginInfo = new UserDTO
            {
                Email = credentials.Mail,
                Password = credentials.Password
            };

            var user = await _repo.GetLogin(loginInfo);
                    
            if (user != null)
            {
                Claim[] claims = new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.FirstName)
                };

                var token = TokenHelper.CreateToken(claims);

                Response.Cookies.Append("token", TokenHelper.WriteToken(token), new CookieOptions
                {
                    HttpOnly = true
                });

                return Ok(new
				{
                    message = "Succes!" + TokenHelper.WriteToken(token)

                });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }

        [HttpGet]
        public async Task<IActionResult> User()
		{
            var jwt = Request.Cookies["token"];

            var token = TokenHelper.Verify(jwt);

            //To Add: Get user id from token

            //return Ok(user);

            return Ok();
		}

        [HttpPost]
        public async Task<IActionResult> Register()
		{
            return Ok();
		}
	}
}
