using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Models
{
	public class Game
	{

		public int Id { get; set; }

		public string Name { get; set; }

		public string Summary { get; set; }

		public double Rating { get; set; }
	}
}
