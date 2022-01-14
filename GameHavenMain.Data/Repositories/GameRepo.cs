using GameHavenMain.Data.DTO;
using GameHavenMain.Data.Interfaces;
using GameHavenMain.Data.HelperClasses;
using IGDB;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GameHavenMain.Data.Repositories
{
	public class GameRepo : IGameRepo
	{

		private readonly IGDBClient igdb;

		public GameRepo(ApplicationDbContext context)
		{
			igdb = ApiHelper.CreateClient();
		}


		public async Task<GameDTO> GameById(int id)
		{
			var result = await igdb.QueryAsync<GameDTO>(IGDBClient.Endpoints.Games, query: $"fields id,name,summary,total_rating,rating,cover.*; w id = {id};");
			return result[0];
		}


		public async Task<IEnumerable<GameDTO>> GamesByName(string name) 
		{
			return await igdb.QueryAsync<GameDTO>(IGDBClient.Endpoints.Games, query: $"fields id,name,summary,rating,cover.*; search \"{name}\"; l 50;");
		}


		public async Task<IEnumerable<GameDTO>> GamesNew()
		{
			return await igdb.QueryAsync<GameDTO>(IGDBClient.Endpoints.Games, query: $"f id,name,total_rating,rating,cover.*; " +
																						$"s total_rating desc; " +
																						$"w total_rating >= 75 & first_release_date >= 1630792800 & rating_count >= 3;" +
																						$"l 50; ");
		}

		public async Task<IEnumerable<GameDTO>> Top100()
		{
			return await igdb.QueryAsync<GameDTO>(IGDBClient.Endpoints.Games, query: $"f id,name,total_rating,rating,cover.*; " +
																						$"s total_rating desc; " +
																						$"w total_rating >= 80 & rating_count >= 50;" +
																						$"l 100; ");
		}

	}
}
