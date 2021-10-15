using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Models
{
	public class GameModel
	{
		[Key]
		[ModelBinder(Name = "id")]
		public int Id { get; set; }

		[ModelBinder(Name = "name")]
		public string Name { get; set; }

		[ModelBinder(Name="summary")]
		public string Summary { get; set; }

		[ModelBinder(Name = "rating")]
		public double Rating { get; set; }
	}
}
