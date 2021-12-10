using System;
using System.ComponentModel.DataAnnotations;

namespace GameHavenMain.Data.DTO
{
	public class UserDTO
	{
		public UserDTO()
		{

		}

		public UserDTO(dynamic credentials)
		{
			Birthday = credentials.Birthday;
			RegisterDate = DateTime.Now;
			Email = credentials.Email;
			FirstName = credentials.FirstName;
			MiddleName = credentials.MiddleName;
			LastName = credentials.LastName;
			Phone = credentials.Phone;
			Username = credentials.Username;
		}

		[Key]
		public int Id { get; set; }

		public string Email { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public string Salt { get; set; }

		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public string LastName { get; set; }

		public DateTime Birthday { get; set; }

		public string Phone { get; set; }

		public DateTime RegisterDate { get; set; }

	}
}
