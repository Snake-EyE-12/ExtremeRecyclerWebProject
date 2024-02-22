using ExtremeRecycler.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExtremeRecycler.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		PlayerData pd = new PlayerData(5.32f, null);
		Item itm = new Item("https://th.bing.com/th/id/OIP.yoQQCDKIK7zg7jZEBJr_1QAAAA?w=176&h=180&c=7&r=0&o=5&dpr=1.5&pid=1.7");
		
		
		public IActionResult Index()
		{
			BigModel bm = new BigModel(pd, itm);

            return View(bm);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}