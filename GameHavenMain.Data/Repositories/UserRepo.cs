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
		private UserContext _context;

		public UserRepo(UserContext context)
		{
			_context = context;
		}

		public async Task<UserDTO> GetLogin(UserDTO loginCredentials)
		{
			return await _context.User
				.Where(u => u.Email == loginCredentials.Email && u.Password == loginCredentials.Password)
				.FirstOrDefaultAsync();
		}
	}
}
