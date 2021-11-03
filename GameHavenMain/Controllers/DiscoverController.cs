using GameHavenMain.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameHavenMain.Controllers
{
	public class DiscoverController : Controller
	{
		private readonly IApplicationDbContext _context;

		public DiscoverController(IApplicationDbContext context)
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
