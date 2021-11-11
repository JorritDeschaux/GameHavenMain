using GameHavenMain.Data;
using GameHavenMain.Models;
using IGDB;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class DiscoverController : Controller
	{

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			//Call API to show new games on front page
			var igdb = ApiHelper.CreateClient();

			var games = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: $"f id,name,rating,cover.*; " +
																						$"s rating desc; " +
																						$"w rating != 0 & first_release_date >= 1630792800; " +
																						$"l 50; ");


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
