using GameHavenMain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
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
			  "vl33o1v2tovnccfy3t2woim9l3yy9l",
			  "hcah9xi55ozqexp7jswmynsmjrpt78"
			);

			var games = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: $"fields id,name,summary; search \"Portal\";");
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
