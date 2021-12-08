using GameHavenMain.Data.DTO;
using GameHavenMain.Data.HelperClasses;
using System.Threading.Tasks;

namespace GameHavenMain.Data.Interfaces
{
	public interface IUserRepo : IGenericRepo<UserDTO>
	{

		Task<UserDTO> GetLoginAsync(UserDTO loginCredentials);

		Task<string> CheckEmailExistsAsync(string email);

		Task<UserDTO> GetUserWithTokenAsync(string jwt, TokenHelper _tokenHelper);

	}
}
