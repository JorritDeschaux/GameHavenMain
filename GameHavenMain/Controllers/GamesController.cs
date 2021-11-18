using GameHavenMain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using IGDB;
using GameHavenMain.Data.DTO;
using GameHavenMain.Data.Interfaces;

namespace GameHavenMain.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiVersion("1")]
	[ApiController]
	public class GamesController : Controller
	{
		private readonly IGameRepo _repo;

		public GamesController(IGameRepo repo)
		{
			_repo = repo;
		}


		[HttpGet]
		public async Task<IActionResult> SearchGame(string gameName)
		{

			if(gameName == null || gameName == string.Empty) { return BadRequest("Search body can't be empty!"); }

			var games = await _repo.GamesByName(gameName);

			if(games != null)
			{
				return Ok(games);
			}
			else
			{
				return BadRequest("No search results found!");
			}

		}

	}
}
