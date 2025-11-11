using Microsoft.AspNetCore.Mvc;

namespace PlanningCore.Areas.Contract.Controllers
{
	[Area("Contract")]
	public class SetupController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	
	}
}
