using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHavenMain.Data.DTO
{
	public class ReviewDTO
	{
		[Key]
		public int Id { get; set; }

		public DateTime Upload_Date { get; set; }

		public int Game_Id { get; set; }

		public string Description { get; set; }

		public int Rating { get; set; }
	}
}
