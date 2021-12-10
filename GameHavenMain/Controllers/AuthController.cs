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


        [HttpGet("userByToken")]
        public async Task<IActionResult> UserByToken()
        {
            var jwt = Request.Headers["Authorization"];
            UserDTO user = await _userRepo.GetUserWithTokenAsync(jwt, _tokenHelper);
            return user != null ? Ok(new UserInfo(user)) : Unauthorized("Token is either invalid or expired");
        }

        [HttpGet("userIdByToken")]
        public async Task<IActionResult> UserIdByToken()
        {
            var jwt = Request.Headers["Authorization"];
            UserDTO user = await _userRepo.GetUserWithTokenAsync(jwt, _tokenHelper);
            return user != null ? Ok(user.Id) : Unauthorized("Token is either invalid or expired");
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
                return BadRequest("No user exists with the given credentials!");
            }

        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register credentials)
        {
            if (credentials.Password != credentials.ConfirmPassword)
			{
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            UserDTO newUser = new(credentials);
            newUser = PasswordEncrypter.EncryptUserPassword(newUser, credentials.Password);
         
            try
            {
                await _userRepo.CreateAsync(newUser);
                return Ok();
            }
			catch(Exception e)
			{
                return BadRequest("Email already exists in database! Error Code:" + e);
            }

		}

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
		{
            string jwt = Request.Headers["Authorization"];
            if(jwt == null) { return Unauthorized(); }

            var result = _tokenHelper.Validate(jwt);
            if(!_tokenHelper.Authorized(result, id)) { return Unauthorized(); }

            try
            {
                await _userRepo.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e);
            
            }

        }
	}
}
