using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Models
{
	public class Register
	{

		public string Email { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public string LastName { get; set; }

		[DataType(DataType.Date)]
		public DateTime Birthday { get; set; }

		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }

	}
}
