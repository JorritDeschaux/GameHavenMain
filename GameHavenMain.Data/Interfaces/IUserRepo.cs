using GameHavenMain.Data.DTO;
using System.Threading.Tasks;

namespace GameHavenMain.Data.Interfaces
{
	public interface IUserRepo
	{
		public Task<UserDTO> GetLogin(UserDTO loginCredentials);
		public Task<UserDTO> GetUser(int id);
		public Task<UserDTO> DeleteUser(int id);
		public Task<UserDTO> UpdateUser(int id);
		public Task<UserDTO> CreateUser(UserDTO registerInfo);
	}
}
