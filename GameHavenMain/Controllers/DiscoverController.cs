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
	[ApiController]
	public class DiscoverController : Controller
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

			if(games != null)
			{
				return Ok(games);
			}
			else
			{
				return BadRequest("No games found!");
			}
		}
	}
}
