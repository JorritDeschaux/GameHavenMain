using GameHavenMain.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Data.Interfaces
{
	public interface IGameRepo
	{

		public Task<GameDTO> GameById(int id);

		public Task<IEnumerable<GameDTO>> GamesByName(string name);

		public Task<IEnumerable<GameDTO>> GamesNew();

		public Task<IEnumerable<GameDTO>> Top100();

	}
}
