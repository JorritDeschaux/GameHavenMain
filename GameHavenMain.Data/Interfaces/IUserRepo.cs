using GameHavenMain.Data.DTO;
using System.Threading.Tasks;

namespace GameHavenMain.Data.Interfaces
{
	public interface IUserRepo
	{

		public Task<UserDTO> GetLogin(UserDTO loginCredentials);

		public Task<UserDTO> GetUserById(int id);

		public Task<bool> CheckEmailExists(string email);

		public Task<bool> DeleteUser(int id);

		public Task<UserDTO> UpdateUser(UserDTO updatedUser);

		public Task<bool> CreateUser(UserDTO newUser);

	}
}
