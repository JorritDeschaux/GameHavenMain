
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

		public ProfileController(IUserRepo userRepo)
		{
			_userRepo = userRepo;
		}

		[HttpGet("updateInfo")]
		public async Task<IActionResult> UpdateUserInfo()
		{
			try
			{
				var jwt = Request.Headers["Authorization"];
				TokenHelper.Verify(jwt);

				var validatedToken = TokenHelper.Verify(jwt);

				int id = Convert.ToInt32(validatedToken.Payload["nameid"].ToString());

				var user = await _userRepo.GetById(id);

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
				TokenHelper.Verify(jwt);

				var validatedToken = TokenHelper.Verify(jwt);

				int id = Convert.ToInt32(validatedToken.Payload["nameid"].ToString());

				var user = await _userRepo.GetById(id);

				user.Birthday = updatedUser.Birthday;
				user.FirstName = updatedUser.FirstName;
				user.MiddleName = updatedUser.MiddleName;
				user.LastName = updatedUser.LastName;
				user.Phone = updatedUser.Phone;

				await _userRepo.Update(user);

				return Ok();

			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

	}

}
