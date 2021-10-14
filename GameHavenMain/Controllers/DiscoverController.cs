using GameHavenMain.Data;
using GameHavenMain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHavenMain.Controllers
{
	public class DiscoverController : Controller
	{
		private readonly ApplicationDbContext _context;

		public DiscoverController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		[Route("api/[controller]")]
		public IActionResult Index()
		{
			return Ok();
		}
	}
}
