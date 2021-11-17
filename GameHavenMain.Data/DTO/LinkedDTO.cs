using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Data.DTO
{
	public class LinkedDTO
	{

		[Key]
		public int Id { get; set; }


		public int OtherId_One { get; set; }


		public int OtherId_Two { get; set; }

	}
}
