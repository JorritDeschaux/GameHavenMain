﻿using GameHavenMain.Data.DTO;
using GameHavenMain.Data.Interfaces;
using GameHavenMain.Data.HelperClasses;
using IGDB;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GameHavenMain.Data.Repositories
{
	public class GameRepo : IGameRepo
	{

		private readonly ApplicationDbContext _context;
		private IGDBClient igdb;

		public GameRepo(ApplicationDbContext context)
		{
			_context = context;
			igdb = ApiHelper.CreateClient();
		}


		public async Task<GameDTO> GameById(int id)
		{
			var result = await igdb.QueryAsync<GameDTO>(IGDBClient.Endpoints.Games, query: $"fields id,name,summary,rating,cover.*; w id = {id};");
			return result[0];
		}


		public async Task<IEnumerable<GameDTO>> GamesByName(string name) 
		{
			return await igdb.QueryAsync<GameDTO>(IGDBClient.Endpoints.Games, query: $"fields id,name,summary,rating,cover.*; search \"{name}\"; l 50;");
		}


		public async Task<IEnumerable<GameDTO>> GamesNew()
		{
			return await igdb.QueryAsync<GameDTO>(IGDBClient.Endpoints.Games, query: $"f id,name,rating,cover.*; " +
																						$"s rating desc; " +
																						$"w rating != 0 & first_release_date >= 1630792800; " +
																						$"l 50; ");
		}

		public async Task<IEnumerable<GameDTO>> Top100()
		{
			return await igdb.QueryAsync<GameDTO>(IGDBClient.Endpoints.Games, query: $"f id,name,rating,cover.*; " +
																						$"s total_rating desc; " +
																						$"w total_rating != 0" +
																						$"l 100; ");
		}

	}
}
