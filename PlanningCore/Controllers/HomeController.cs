using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanningCore.Dashboard;
using PlanningCore.Models;
using System.Diagnostics;

namespace PlanningCore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
		private readonly ILogger<HomeController> _logger;
		private readonly IDashboard dashboard;

		public HomeController(ILogger<HomeController> logger, IDashboard _dashboard)
		{
			_logger = logger;
			dashboard = _dashboard;
		}

		public async Task<IActionResult> Index()
        {
			return View(await dashboard.GetDashAllDataForDashboard());
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
