using Microsoft.AspNetCore.Mvc;
using PlanningCore.Data;
using PlanningCore.Utilities;

namespace PlanningCore.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UserRolesController : Controller
	{
		private readonly IUtility _utility;
		public UserRolesController(IUtility utility)
		{
			_utility = utility;
		}
		public async Task<IActionResult> UserList()
		{
			return View(await _utility.GetUserList());
		}
		public async Task<IActionResult> ChangeRole(string id, string role)
		{
			return Json(await _utility.AssignOrChangeRole(id, role));
		}
		public async Task<IActionResult> UserActivateOrDeactivate(string id)
		{
			return Json(await _utility.UserActivateOrDeactivate(id));
		}
		public async Task<IActionResult> ResetPassword(string id)
		{
			return Json(await _utility.ResetUserPassword(id));
		}
	}
}
