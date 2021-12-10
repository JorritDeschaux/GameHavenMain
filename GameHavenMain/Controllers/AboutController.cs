using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Controllers
{

	[Route("api/about")]
	[ApiVersion("1")]
	[ApiVersion("2")]
	[ApiController]
	public class AboutController : ControllerBase
	{

		[HttpGet]
		public IActionResult Index()
		{
			return Ok();
		}

	}

}
