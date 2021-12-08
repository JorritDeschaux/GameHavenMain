using GameHavenMain.Data;
using GameHavenMain.Data.DTO;
using GameHavenMain.Data.Interfaces;
using GameHavenMain.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Tests
{
	[TestClass]
	public class Game
	{
        IGameRepo gameRepo;
        ApplicationDbContext context;

        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            context = new ApplicationDbContext(options);

            gameRepo = new GameRepo(context);
        }

        [TestMethod]
        public void Game_Get_ByIdExists()
		{
            GameDTO game = gameRepo.GameById(1).Result;

            Assert.IsNotNull(game);
		}

        [TestMethod]
        public void Game_Get_GamesByName()
        {
            IEnumerable<GameDTO> games = gameRepo.GamesByName("Minecraft").Result;

			foreach (var game in games)
			{
                Assert.IsNotNull(game);
			}
        }

        [TestMethod]
        public void Game_Get_NewGames()
        {
            IEnumerable<GameDTO> games = gameRepo.GamesNew().Result;

            foreach (var game in games)
            {
                Assert.IsNotNull(game);
            }
        }
    }
}
