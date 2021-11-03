using GameHavenMain.Data;
using GameHavenMain.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
			//Call API to show new games on front page
			return Ok();
		}
	}
}
