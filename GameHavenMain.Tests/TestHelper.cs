using GameHavenMain.Data.DTO;
using GameHavenMain.Data.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Tests
{
	public static class TestHelper
	{

        public static UserDTO CreateTestUser()
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
    }
}
