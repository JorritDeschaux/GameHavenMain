using GameHavenMain.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Data.Repositories
{
	public class GameRepo : IGameRepo
	{
		private readonly ApplicationDbContext _context;

		public GameRepo(ApplicationDbContext context)
		{
			_context = context;
		}
	}
}
