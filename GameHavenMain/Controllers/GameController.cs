using GameHavenMain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameHavenMain.Controllers
{
	[Route("api/[controller]/[action]")]
	public class GameController : Controller
	{

		HttpClientHandler _clientHandler = new HttpClientHandler();

		[HttpGet]
		public async Task<IActionResult> GameById(string gameName)
		{
			var game = await PingGame(gameName);
			return game;
		}

		[HttpGet]
		public async Task<IActionResult> PingGame(string gameName)
		{
			using (var httpClient = new HttpClient(_clientHandler))
			{
				var response = httpClient.GetAsync("https://api.igdb.com/v4/games/" + gameName).Result;

				string apiResponse = await response.Content.ReadAsStringAsync();
				Game game = new Game();
				game = JsonConvert.DeserializeObject<Game>(apiResponse);

				return Ok(game);
			}	
		}

	}
}
