using GameHavenMain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using IGDB;

namespace GameHavenMain.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class GamesController : Controller
	{

		[HttpGet]
		public async Task<IActionResult> GetGamesByName(string gameName)
		{
			//Creates and authenticates IGDB API using Twitch Developer Authentication with Client-Id and Secret Key.
			var igdb = new IGDBClient(
			  "ptz8ma4spnaia96n78yerskcg7pyyr",
			  "g1gqnneu4bdwvr46fqx2x7nc84a129"
			);

			var games = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: $"fields id,name; search \"Portal\";");
			return Ok(games);
		}

		[HttpPost]
		public async Task<IActionResult> GetFromAPI(RestRequest request)
		 {
			var client = new RestClient($"https://api.igdb.com/v4/{Endpoints.Games}");
			var data = await ApiHelper.AuthorizedAPIRequest(client, request);
			IEnumerable<Game> games = JsonConvert.DeserializeObject<List<Game>>(data.Content);
			return Ok(games);
		}
    }
}
