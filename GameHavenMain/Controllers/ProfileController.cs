
using GameHavenMain.Data.DTO;
using GameHavenMain.Data.HelperClasses;
using GameHavenMain.Data.Interfaces;
using GameHavenMain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Controllers
{

	[Route("api/profile")]
	[ApiVersion("1")]
	[ApiVersion("2")]
	[ApiController]
	public class ProfileController : ControllerBase
	{

		private IUserRepo _userRepo;
		private TokenHelper _tokenHelper;

		public ProfileController(IUserRepo userRepo, TokenHelper tokenHelper)
		{
			_userRepo = userRepo;
			_tokenHelper = tokenHelper;
		}


		[HttpGet("updateInfo")]
		public async Task<IActionResult> UpdateUserInfo()
		{
			try
			{
				var jwt = Request.Headers["Authorization"];
				UserDTO user = _userRepo.GetUserWithTokenAsync(jwt, _tokenHelper).Result;

				if (user == null)
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
			catch (Exception e)
			{
				return BadRequest(e);
			}

		}


		[HttpPost("updateInfo")]
		public async Task<IActionResult> UpdateUserInfo(UpdateUser updatedUser)
		{
			try
			{
				var jwt = Request.Headers["Authorization"];
				UserDTO user = _userRepo.GetUserWithTokenAsync(jwt, _tokenHelper).Result;

				if (user == null)
				{
					return Unauthorized("Token is either invalid or expired");
				}

				user.Birthday = updatedUser.Birthday;
				user.FirstName = updatedUser.FirstName;
				user.MiddleName = updatedUser.MiddleName;
				user.LastName = updatedUser.LastName;
				user.Phone = updatedUser.Phone;

				await _userRepo.UpdateAsync(user);

				return Ok();

			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

	}

}
