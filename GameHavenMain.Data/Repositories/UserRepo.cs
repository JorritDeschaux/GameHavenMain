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
	public class UserRepo : IUserRepo
	{
		private readonly ApplicationDbContext _context;

		public UserRepo(ApplicationDbContext context)
		{
			_context = context;
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


		public async Task<UserDTO> GetUserById(int id)
		{

			return await _context.User
				.Where(u => u.Id == id)
				.FirstOrDefaultAsync();

		}

		public async Task<bool> CheckEmailExists(string email)
		{
			var user = await _context.User
				.Where(u => u.Email == email)
				.FirstOrDefaultAsync();

			if (user != null)
			{
				return true;
			}
			else
			{
				return false;
			}

		}


		public async Task<bool> DeleteUser(int id)
		{

			var user = await GetUserById(id);
			_context.Remove(user);
			_context.SaveChanges();

			if (user != null)
			{
				return true;
			}
			else
			{
				return false;
			}

		}


		public async Task<UserDTO> UpdateUser(UserDTO updatedUser)
		{

			_context.Update(updatedUser.Id);
			_context.SaveChanges();

			return await GetUserById(updatedUser.Id);

		}


		public async Task<bool> CreateUser(UserDTO newUser)
		{

			var exists = await CheckEmailExists(newUser.Email);

			if (exists)
			{
				return false;
			}

			_context.Add(newUser);
			_context.SaveChanges();

			return true;

		}
	}
}
