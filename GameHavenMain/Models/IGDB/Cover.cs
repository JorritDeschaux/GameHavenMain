using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Models.IGDB
{
	public class Cover
	{

		public int Id { get; set; }

		public int Game { get; set; }

		public string Url { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

	}
}
