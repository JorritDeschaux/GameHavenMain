using GameHavenMain.Data;
using GameHavenMain.Data.DTO;
using GameHavenMain.Data.HelperClasses;
using GameHavenMain.Data.Interfaces;
using GameHavenMain.Models;
using IGDB;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Controllers
{
	[Route("api/discover")]
	[ApiVersion("1")]
	[ApiVersion("2")]
	[ApiController]
	public class DiscoverController : ControllerBase
	{

		private readonly IGameRepo _repo;

		public DiscoverController(IGameRepo repo)
		{
			_repo = repo;
		}


		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var games = await _repo.GamesNew();
			return games != null ? Ok(games) : BadRequest("No games found!");
		}
	}
}
