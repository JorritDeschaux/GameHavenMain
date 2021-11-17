using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Data.DTO
{
	public class ProfileDTO
	{

		[Key]
		public int Id { get; set; }


		public int User_Id { get; set; }


		public string Description { get; set; }

	}
}
