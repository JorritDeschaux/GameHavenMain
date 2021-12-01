using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Models
{
	public class UpdateUser
	{

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string MiddleName { get; set; }

		[DataType(DataType.Date)]
		public DateTime Birthday { get; set; }

		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }

	}
}
