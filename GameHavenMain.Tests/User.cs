using GameHavenMain.Data;
using GameHavenMain.Data.DTO;
using GameHavenMain.Data.HelperClasses;
using GameHavenMain.Data.Interfaces;
using GameHavenMain.Data.Repositories;
using GameHavenMain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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
                Username = "test",
            };

            user = PasswordEncrypter.EncryptUserPassword(user, "test");

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
        public void User_Get_Exists()
        {
            UserDTO user = CreateTestUser();
            userRepo.CreateAsync(user);

            UserDTO userGet = userRepo.GetByIdAsync(user.Id).Result;

            Assert.IsNotNull(userGet);
        }

        [TestMethod]
        public void User_Get_AllUsers()
        {
            UserDTO user1 = CreateTestUser();
            userRepo.CreateAsync(user1);
            UserDTO user2 = CreateTestUser();
            user2.Id = 2;
            user2.Email = "test2@test.nl";
            userRepo.CreateAsync(user2);

            IEnumerable<UserDTO> users = userRepo.GetAllAsync().Result;

			foreach (var user in users)
			{
                Assert.IsNotNull(user);
			}
        }

        [TestMethod]
        public void User_Get_UserLogin()
        {
            UserDTO user = CreateTestUser();
            userRepo.CreateAsync(user);

            Login loginCredentials = new Login
            {
                Email = user.Email,
                Password = "test"
            };

            UserDTO loggedInUser = userRepo.GetLoginAsync(
                new UserDTO { Email = loginCredentials.Email, Password = loginCredentials.Password })
                .Result;

            Assert.IsNotNull(loggedInUser);
            Assert.AreEqual(loginCredentials.Email, loggedInUser.Email);
        }

        [TestMethod]
        public void User_Get_UserMailAlreadyExistsCheck()
        {
            UserDTO user1 = CreateTestUser();
            string email = userRepo.CheckEmailExistsAsync(user1.Email).Result;
            if(email is null)
                userRepo.CreateAsync(user1);

            UserDTO user2 = CreateTestUser();
            user2.Id = 2;
            email = userRepo.CheckEmailExistsAsync(user2.Email).Result;
            if (email is null)
                userRepo.CreateAsync(user2);

            Assert.IsNotNull(userRepo.GetByIdAsync(1).Result);
            Assert.IsNull(userRepo.GetByIdAsync(2).Result);
        }

        [TestMethod]
        public void User_Add_TrueExistsById()
        {
            UserDTO user = CreateTestUser();
            userRepo.CreateAsync(user);

            Assert.IsNotNull(userRepo.GetByIdAsync(user.Id).Result);
        }


        [TestMethod]
        public void User_Update_TrueInfoUpdated()
        {
            UserDTO user = CreateTestUser();
            userRepo.CreateAsync(user);

            Assert.IsTrue(userRepo.GetByIdAsync(user.Id).Result.Username == user.Username);

            string updatedUsername = "updated";
            user.Username = updatedUsername;
            userRepo.UpdateAsync(user);

            Assert.IsTrue(userRepo.GetByIdAsync(user.Id).Result.Username == updatedUsername);
        }


        [TestMethod]
        public void User_Delete_TrueDeletedFromDatabase()
        {

            UserDTO user = CreateTestUser();
            userRepo.CreateAsync(user);

            Assert.IsNotNull(userRepo.GetByIdAsync(user.Id).Result);

            userRepo.DeleteAsync(user.Id);

            Assert.IsNull(userRepo.GetByIdAsync(user.Id).Result);
        }
    }
}
