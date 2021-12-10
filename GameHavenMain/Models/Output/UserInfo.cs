using GameHavenMain.Data.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Models
{
	public class UserInfo
	{
		public UserInfo()
		{

		}

		public UserInfo(UserDTO user)
		{
			Id = user.Id;
			Email = user.Email;
			Username = user.Username;
			FirstName = user.FirstName;
			MiddleName = user.MiddleName;
			LastName = user.LastName;
			Birthday = user.Birthday;
			Phone = user.Phone;
			RegisterDate = user.RegisterDate;
		}

		public int Id { get; set; }

		public string Email { get; set; }

		public string Username { get; set; }

		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public string LastName { get; set; }

		[DataType(DataType.Date)]
		public DateTime Birthday { get; set; }

		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }

		public DateTime RegisterDate { get; set; }

	}
}
