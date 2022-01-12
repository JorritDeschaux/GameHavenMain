
using GameHavenMain.Data.DTO;
using GameHavenMain.Data.HelperClasses;
using GameHavenMain.Data.Interfaces;
using GameHavenMain.Models;
using GameHavenMain.Models.Output;
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

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPublicUserInfo(int id)
        {
			UserDTO user = await _userRepo.GetByIdAsync(id);

			if(user == null)
            {
				return NotFound();
            }

			PublicInfo publicInfo = new PublicInfo(user);
			return Ok(publicInfo);
        }

		[HttpGet("updateInfo")]
		public async Task<IActionResult> UpdateUserInfo()
		{
			try
			{
				var jwt = Request.Headers["Authorization"];
				UserDTO user = await _userRepo.GetUserWithTokenAsync(jwt, _tokenHelper);

				if (user == null)
				{
					return Unauthorized("Token is either invalid or expired");
				}

				UserInfo userInfo = new UserInfo(user);

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
				UserDTO user = await _userRepo.GetUserWithTokenAsync(jwt, _tokenHelper);

				if (user == null)
				{
					return Unauthorized("Token is either invalid or expired");
				}

				if(updatedUser.Birthday != new DateTime(1, 1, 1)) { user.Birthday = updatedUser.Birthday; }
				if(updatedUser.FirstName != null) { user.FirstName = updatedUser.FirstName; }
				if(updatedUser.MiddleName != null) { user.MiddleName = updatedUser.MiddleName; }
				if(updatedUser.LastName != null) { user.LastName = updatedUser.LastName; }	
				if(updatedUser.Phone != null) { user.Phone = updatedUser.Phone;}

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
