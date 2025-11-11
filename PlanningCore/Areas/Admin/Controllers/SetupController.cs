using Microsoft.AspNetCore.Mvc;

namespace PlanningCore.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class SetupController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}	
		public IActionResult Setup()
		{
			return View();
		}	
		public IActionResult YojanaDetailsSuchi()
		{
			return View();
		}	
		public IActionResult TotalSuchi()
		{
			return View();
		}
	}
}
