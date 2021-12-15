using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GameHavenMain.Data.HelperClasses;
using GameHavenMain.Data.DTO;
using GameHavenMain.Data.Interfaces;

namespace GameHavenMain.Controllers
{
	[Route("api/games")]
	[ApiVersion("1")]
	[ApiVersion("2")]
	[ApiController]
	public class GamesController : ControllerBase
	{
		private readonly IGameRepo _repo;

		public GamesController(IGameRepo repo)
		{
			_repo = repo;
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> SearchById(int id)
		{
			var game = await _repo.GameById(id);
			return game != null ? Ok(game) : BadRequest("No game found!");
		}


		[HttpGet("search/{gameName}")]
		public async Task<IActionResult> SearchGame(string gameName)
		{
			if(gameName == null || gameName == string.Empty) { return BadRequest("Search body can't be empty!"); }

			var games = await _repo.GamesByName(gameName);
			return games != null ? Ok(games) : BadRequest("No search results found!");
		}

	}
}
