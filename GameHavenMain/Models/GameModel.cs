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
		[ModelBinder(Name = "id")]
		public int Id { get; set; }

		[ModelBinder(Name="summary")]
		public string Summary { get; set; }
	}
}
