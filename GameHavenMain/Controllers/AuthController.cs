using GameHavenMain.Data;
using GameHavenMain.Data.DTO;
using GameHavenMain.Data.HelperClasses;
using GameHavenMain.Data.Interfaces;
using GameHavenMain.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class AuthController : ControllerBase
    {
        private IUserRepo _userRepo;
		private TokenHelper _tokenHelper;

		public AuthController(IUserRepo userRepo, TokenHelper tokenHelper)
		{
            _userRepo = userRepo;
            _tokenHelper = tokenHelper;
		}


        [HttpGet("verify")]
        public async Task<IActionResult> Verify()
        {

            var jwt = Request.Headers["Authorization"];

            UserDTO user = _userRepo.GetUserWithTokenAsync(jwt, _tokenHelper).Result;

            if(user == null)
			{
                return Unauthorized("Token is either invalid or expired");
			}                

            UserInfo userInfo = new UserInfo
            {
                Email = user.Email,
                Birthday = user.Birthday,
                RegisterDate = user.RegisterDate,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Phone = user.Phone,
                Username = user.Username
            };

            return Ok(userInfo);

        }


        [HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] Login credentials)
		{

            UserDTO loginInfo = new UserDTO
            {
                Email = credentials.Email,
                Password = credentials.Password
            };

            var user = await _userRepo.GetLoginAsync(loginInfo);
                    
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

                var token = _tokenHelper.CreateToken(claims);

                return Ok(_tokenHelper.WriteToken(token));
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "No user exists with the given credentials!");
            }

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
                Phone = credentials.Phone,
                Username = credentials.Username,
            };

            newUser = PasswordEncrypter.EncryptUserPassword(newUser, credentials.Password);
         
            try
            {
                await _userRepo.CreateAsync(newUser);
                return Ok();
            }
			catch(Exception e)
			{
                return StatusCode(StatusCodes.Status401Unauthorized, "Email already exists in database! Error Code:" + e);
            }

		}

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
		{

            try
            {
                await _userRepo.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Email already exists in database! Error Code: " + e);
            
            }

        }
	}
}
