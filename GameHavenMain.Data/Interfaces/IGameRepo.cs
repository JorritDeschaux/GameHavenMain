using GameHavenMain.Data.DTO;
using GameHavenMain.Data.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Data.Interfaces
{
	public interface IGameRepo
	{

		public Task<IEnumerable<GameDTO>> GameById(int id);

		public Task<IEnumerable<GameDTO>> GamesByName(string name);

		public Task<IEnumerable<GameDTO>> GamesNew();

	}
}
