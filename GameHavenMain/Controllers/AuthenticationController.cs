using GameHavenMain.Data;
using GameHavenMain.Data.DTO;
using GameHavenMain.Data.Interfaces;
using GameHavenMain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
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
                    new Claim("firstname", user.FirstName),                    
                    new Claim("middlename", user.MiddleName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("username", user.Username)
                };

                var token = TokenHelper.CreateToken(claims);

                return Ok(TokenHelper.WriteToken(token));
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }

        [HttpGet]
        public async Task<IActionResult> User()
		{

            var jwt = Request.Headers["Authorization"];

            if (jwt == "null")
                return Ok();

            var token = TokenHelper.Verify(jwt);

            int id = Convert.ToInt32(token.Payload["nameid"].ToString());
            var user = await _repo.GetUser(id);

            return Ok(user);

		}

        [HttpPost]
        public async Task<IActionResult> Register()
		{
            return Ok();
		}
	}
}
