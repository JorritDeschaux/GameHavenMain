using GameHavenMain.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Data.Interfaces
{
	public interface IUserRepo
	{
		public Task<UserDTO> GetLogin(UserDTO loginCredentials);
		public Task<UserDTO> GetUser(int id);

	}
}
