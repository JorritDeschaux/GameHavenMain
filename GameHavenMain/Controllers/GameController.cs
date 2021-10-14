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

namespace GameHavenMain.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class GameController : Controller
	{

		[HttpGet]
		public async Task<IActionResult> GetGamesByName(string gameName)
		{
			string body = $"?fields=*&search={gameName}";
			var games = await GetFromAPI(body);

			return Ok(games);
		}

		[HttpPost]
		public async Task<IActionResult> GetFromAPI(string body)
		 {
			List<GameModel> games = new List<GameModel>();

			var client = new RestClient($"https://api.igdb.com/v4/games/{body}");
			var request = new RestRequest();

			var data = await ApiHelper.AuthorizedAPIRequest(client, request);

			games = JsonConvert.DeserializeObject<List<GameModel>>(data.Content);

			return Ok(games);
		}
	}
}
