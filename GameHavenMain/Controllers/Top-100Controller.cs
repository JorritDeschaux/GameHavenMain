using GameHavenMain.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Controllers
{
	[Route("api/top-100")]
	[ApiVersion("1")]
	[ApiVersion("2")]
	[ApiController]
	public class Top_100Controller : ControllerBase
	{

		private readonly IGameRepo _repo;

		public Top_100Controller(IGameRepo repo)
		{
			_repo = repo;
		}

		[HttpGet]
		public async Task<IActionResult> GetTop100()
		{
			var games = await _repo.Top100();
			return games != null ? Ok(games) : BadRequest();
		}

	}
}
