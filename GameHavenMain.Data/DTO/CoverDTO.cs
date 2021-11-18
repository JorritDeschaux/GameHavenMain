using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Data.DTO
{
	public class CoverDTO
	{

		[Key]
		public int Id { get; set; }

		public int Game { get; set; }

		public string Url { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

	}
}
