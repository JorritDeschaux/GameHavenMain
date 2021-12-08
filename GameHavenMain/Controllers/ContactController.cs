using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Controllers
{
	[Route("api/contact")]
	[ApiVersion("1")]
	[ApiVersion("2")]
	[ApiController]
	public class ContactController : ControllerBase
	{

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return Ok();
		}
	}
}
