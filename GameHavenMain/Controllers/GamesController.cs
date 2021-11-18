﻿using GameHavenMain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using IGDB;
using GameHavenMain.Data.HelperClasses;
using GameHavenMain.Data.DTO;

namespace GameHavenMain.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class GamesController : Controller
	{

		[HttpGet]
		public async Task<IActionResult> SearchGame(string gameName)
		{

			if(gameName == null || gameName == string.Empty) { return BadRequest("Search body can't be empty!"); }

			//Creates and authenticates IGDB API using Twitch Developer Authentication with Client-Id and Secret Key.
			var igdb = ApiHelper.CreateClient();

			var games = await igdb.QueryAsync<GameDTO>(IGDBClient.Endpoints.Games, query: $"fields id,name,summary,rating,cover.*; search \"{gameName}\"; l 50;") ;
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
