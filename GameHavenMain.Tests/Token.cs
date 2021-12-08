using GameHavenMain.Data;
using GameHavenMain.Data.DTO;
using GameHavenMain.Data.HelperClasses;
using GameHavenMain.Data.Interfaces;
using GameHavenMain.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Tests
{

    [TestClass]
	public class Token
	{

        IUserRepo userRepo;
        ApplicationDbContext context;
        private TokenHelper tokenHelper;

        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            context = new ApplicationDbContext(options);

            userRepo = new UserRepo(context);

            tokenHelper = new TokenHelper("SEcret2930139SDKLAK@!_!_#DLKASD93eiweLkJSD");
        }

        [TestMethod]
        public void Token_Create_JWTGetsCreatedWithClaimsAndIsValid()
		{

            UserDTO user = TestHelper.CreateTestUser();

            Claim[] claims = new Claim[]
               {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("firstname", user.FirstName),
                    new Claim("middlename", user.MiddleName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("username", user.Username)
               };

            var token = tokenHelper.CreateToken(claims);

            Assert.IsNotNull(token);

            var validatedToken = tokenHelper.Validate(tokenHelper.WriteToken(token));

            Assert.IsNotNull(validatedToken);
            Assert.AreEqual(user.Id.ToString(), validatedToken.Payload["nameid"]);

        }

        [TestMethod]
        public void Token_Create_ExpiredTokenShouldBeChecked()
        {

            UserDTO user = TestHelper.CreateTestUser();

            Claim[] claims = new Claim[]
               {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("firstname", user.FirstName),
                    new Claim("middlename", user.MiddleName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("username", user.Username)
               };

            //JWT expires in 30 days normally, expiration can't be UtcNow
            var token = tokenHelper.CreateToken(claims, DateTime.UtcNow.AddSeconds(0.01));

            //Token should be expired so IsExpired returns true
            Assert.IsNotNull(token);
            Assert.IsTrue(tokenHelper.IsExpired(token));


            var tokenNormal = tokenHelper.CreateToken(claims);

            //Token shouldn't be expired so IsExpired returns false
            Assert.IsNotNull(tokenNormal);
            Assert.IsFalse(tokenHelper.IsExpired(tokenNormal));

        }

        [TestMethod]
        public void Token_Get_UserUsingToken()
        {
            UserDTO user = TestHelper.CreateTestUser();
            userRepo.CreateAsync(user);

            Claim[] claims = new Claim[]
               {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("firstname", user.FirstName),
                    new Claim("middlename", user.MiddleName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("username", user.Username)
               };

            var token = tokenHelper.CreateToken(claims);

            //This operation also checks for expiration of the token, if it is expired it will return null
            UserDTO userGet = userRepo.GetUserWithTokenAsync(tokenHelper.WriteToken(token), tokenHelper).Result;

            Assert.IsNotNull(userGet);
        }
    }
}
