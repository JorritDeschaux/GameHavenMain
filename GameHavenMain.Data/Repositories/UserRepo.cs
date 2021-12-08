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

		public async Task<UserDTO> GetLoginAsync(UserDTO loginCredentials)
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

		public async Task<string> CheckEmailExistsAsync(string email)
		{
			return await _context.User
				.Where(u => u.Email == email)
				.Select(u => u.Email)
				.FirstOrDefaultAsync();
		}

		public async Task<UserDTO> GetUserWithTokenAsync(string jwt, TokenHelper _tokenHelper)
		{
			try
			{
				var validatedToken = _tokenHelper.Validate(jwt);

				int id = Convert.ToInt32(validatedToken.Payload["nameid"].ToString());

				if (_tokenHelper.IsExpired(validatedToken))
					return null;

				return await GetByIdAsync(id);
			}
			catch
			{
				return null;
			}

		}

	}
}
