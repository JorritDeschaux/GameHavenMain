using GameHavenMain.Data;
using GameHavenMain.Data.DTO;
using GameHavenMain.Data.HelperClasses;
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
	[Route("api/auth")]
	[ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    public class AuthController : Controller
	{
		private readonly IUserRepo _repo;

		public AuthController(IUserRepo repo)
		{
            _repo = repo;
		}


		[HttpPost("login")]
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
                return StatusCode(StatusCodes.Status401Unauthorized, "No user exists with the given credentials!");
            }

        }


        [HttpGet("verify")]
        public async Task<IActionResult> VerifyUser()
		{

            var jwt = Request.Headers["Authorization"];

            if (jwt == "null")
                return Ok();

            var token = TokenHelper.Verify(jwt);

            int id = Convert.ToInt32(token.Payload["nameid"].ToString());
            var user = await _repo.GetUserById(id);

            return Ok(user);

		}


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register credentials)
        {
            if (credentials.Password != credentials.ConfirmPassword)
			{
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            UserDTO newUser = new()
            {

                Birthday = credentials.Birthday,
                RegisterDate = DateTime.Now,
                Email = credentials.Email,
                FirstName = credentials.FirstName,
                MiddleName = credentials.MiddleName,
                LastName = credentials.LastName,
                Password = PasswordEncrypter.EncryptPassword(credentials.Password),
                Phone = credentials.Phone,
                Username = credentials.Username,

            };

            var success = await _repo.CreateUser(newUser);

            if (success)
            {
                return Ok();
            }
			else
			{
                return StatusCode(StatusCodes.Status401Unauthorized, "Email already exists in database!");
            }
		}
	}
}
