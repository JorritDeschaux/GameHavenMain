using GameHavenMain.Data.DTO;
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
			return await _context.User
				.Where(u => u.Email == loginCredentials.Email && u.Password == loginCredentials.Password)
				.FirstOrDefaultAsync();
		}

		public async Task<UserDTO> GetUser(int id)
		{
			return await _context.User
				.Where(u => u.Id == id)
				.FirstOrDefaultAsync();
		}

		public async Task<UserDTO> DeleteUser(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<UserDTO> UpdateUser(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<UserDTO> CreateUser(UserDTO registerInfo)
		{
			throw new NotImplementedException();
		}
	}
}
