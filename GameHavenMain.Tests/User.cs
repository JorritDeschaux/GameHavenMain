using GameHavenMain.Data;
using GameHavenMain.Data.DTO;
using GameHavenMain.Data.Interfaces;
using GameHavenMain.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GameHavenMain.Tests
{

	[TestClass]
	public class User
	{

        IUserRepo userRepo;
        ApplicationDbContext context;

        private UserDTO CreateTestUser()
        {

            UserDTO user = new UserDTO
            {
                Id = 1,
                RegisterDate = System.DateTime.Now,
                Email = "test@gmail.com",
                Birthday = System.DateTime.Now,
                FirstName = "test",
                LastName = "tester",
                MiddleName = "",
                Phone = "068812391",
                Username = "test"
            };

            return user;
        }

        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            context = new ApplicationDbContext(options);

            userRepo = new UserRepo(context);
        }

        [TestMethod]
        public void User_Add_TrueExistsById()
        {
            UserDTO user = CreateTestUser();
            userRepo.Create(user);

            Assert.IsNotNull(userRepo.GetById(user.Id).Result);
        }

        [TestMethod]
        public void User_Update_TrueInfoUpdated()
        {
            UserDTO user = CreateTestUser();
            userRepo.Create(user);

            Assert.IsTrue(userRepo.GetById(user.Id).Result.Username == user.Username);

            string updatedUsername = "updated";
            user.Username = updatedUsername;
            userRepo.Update(user);

            Assert.IsTrue(userRepo.GetById(user.Id).Result.Username == updatedUsername);
        }


        [TestMethod]
        public void User_Delete_TrueDeletedFromDatabase()
        {

            UserDTO user = CreateTestUser();
            userRepo.Create(user);

            Assert.IsNotNull(userRepo.GetById(user.Id).Result);

            userRepo.Delete(user.Id);

            Assert.IsNull(userRepo.GetById(user.Id).Result);
        }
    }
}
