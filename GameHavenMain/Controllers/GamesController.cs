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
	[Route("api/games")]
	[ApiVersion("1")]
	[ApiController]
	public class GamesController : Controller
	{
		private readonly IGameRepo _repo;

		public GamesController(IGameRepo repo)
		{
			_repo = repo;
		}


		[HttpGet("{gameName}")]
		public async Task<IActionResult> Index(int id)
		{
			var game = await _repo.GameById(id);

			if (game != null)
			{
				return Ok(game);
			}
			else
			{
				return BadRequest("No existing game found!");
			}
		}


		[HttpGet("search/{gameName}")]
		public async Task<IActionResult> SearchGame([FromQuery] SearchParameters searchParams)
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
