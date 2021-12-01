using GameHavenMain.Data.DTO;
using System.Threading.Tasks;

namespace GameHavenMain.Data.Interfaces
{
	public interface IUserRepo : IGenericRepo<UserDTO>
	{

		Task<UserDTO> GetLogin(UserDTO loginCredentials);

		Task<string> CheckEmailExists(string email);

	}
}
