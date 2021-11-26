using GameHavenMain.Data.DTO;
using GameHavenMain.Data.HelperClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameHaven.Tests
{
	[TestClass]
	public class TestEncryption
	{

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

		[TestMethod]
		public void Check_If_Encryption_Works_With_User_Salts()
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
