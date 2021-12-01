using GameHavenMain.Data.DTO;
using GameHavenMain.Data.HelperClasses;
using GameHavenMain.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Data.Repositories
{
	public class UserRepo : GenericRepo<UserDTO>, IUserRepo
	{
		public UserRepo(ApplicationDbContext context) : base(context) 
		{

		}

		public async Task<UserDTO> GetLogin(UserDTO loginCredentials)
		{
			try
			{
				var userSalt = await _context.User
								.Where(u => u.Email == loginCredentials.Email)
								.Select(u => u.Salt)
								.FirstOrDefaultAsync();

				var hashedInput = PasswordEncrypter.EncryptPasswordWithGivenSalt(loginCredentials.Password, userSalt);		
				
				return await _context.User
				.Where(u => u.Email == loginCredentials.Email && u.Password == hashedInput)
				.FirstOrDefaultAsync();
			}
			catch
			{
				return null;
			}
		}

		public async Task<string> CheckEmailExists(string email)
		{
			return await _context.User
				.Where(u => u.Email == email)
				.Select(u => u.Email)
				.FirstOrDefaultAsync();
		}

	}
}
