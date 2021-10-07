using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Controllers
{
	public class DiscoverController : Controller
	{
		public IActionResult Index()
		{
			return Ok();
		}
	}
}
