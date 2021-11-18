using GameHavenMain.Data.DTO;
using System.Threading.Tasks;

namespace GameHavenMain.Data.Interfaces
{
	public interface IUserRepo
	{

		public Task<UserDTO> GetLogin(UserDTO loginCredentials);

		public Task<UserDTO> GetUserById(int id);

		public Task<bool> CheckEmailExists(string email);

		public void DeleteUser(int id);

		public Task<UserDTO> UpdateUser(int id);

		public Task<bool> CreateUser(UserDTO newUser);

	}
}
