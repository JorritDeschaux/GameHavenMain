using GameHavenMain.Data;
using GameHavenMain.Data.DTO;
using GameHavenMain.Data.HelperClasses;
using GameHavenMain.Data.Interfaces;
using GameHavenMain.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GameHavenMain.Tests
{
	[TestClass]
	public class Encryption
	{

		IUserRepo userRepo;
		ApplicationDbContext context;

		private UserDTO CreateTestUser()
		{

			UserDTO user = new UserDTO
			{
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
		public void Encryption_Create_PassGetsEncryptedAndExistsInDB()
		{
			var password = "test1234";

			UserDTO user = CreateTestUser();

			user = PasswordEncrypter.EncryptUserPassword(user, password);
			userRepo.CreateAsync(user);

			Assert.IsNotNull(userRepo.GetByIdAsync(user.Id).Result.Password);
			Assert.IsNotNull(userRepo.GetByIdAsync(user.Id).Result.Salt);

		}


		[TestMethod]
		public void Encryption_Create_SaltsAreUniqueToUser()
		{
			var password = "test1234";

			//Both will have same password but different salt
			UserDTO user = CreateTestUser();
			UserDTO user2 = CreateTestUser();

			user = PasswordEncrypter.EncryptUserPassword(user, password);
			user2 = PasswordEncrypter.EncryptUserPassword(user2, password);
			var hashed = PasswordEncrypter.EncryptPasswordWithGivenSalt(password, user.Salt);


			Assert.AreEqual(hashed, user.Password);
			Assert.AreNotEqual(hashed, user2.Password);
		}
	}
}
