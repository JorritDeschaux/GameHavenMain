
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Data.DTO
{
	public class GameDTO
	{

		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public string Summary { get; set; }

		public int[] Platforms { get; set; }

		public string First_Release_Date { get; set; }

		public int Hypes { get; set; }

		public int Follows { get; set; }

		public double Rating { get; set; }

		public int Rating_Count { get; set; }

		public CoverDTO Cover { get; set; }

	}
}
