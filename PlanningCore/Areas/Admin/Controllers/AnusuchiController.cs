using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Planning.Models.PlanningAnusuchiViewModel;
using PlanningCore.Data;
using PlanningCore.Utilities;

namespace PlanningCore.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AnusuchiController : Controller
	{

	
		private readonly IAnusuchi _anusuchi = null;
		private readonly IUtility _utility = null;
		private readonly PlanningContext _context = null;
		public AnusuchiController(IUtility utility, PlanningContext context, IAnusuchi anusuchi )
		{
	
			_context = context;
			_utility = utility;
			_anusuchi = anusuchi;
			
		}
		#region Anusuchi 1
		public async Task<IActionResult> GetAnushuchi3List(int id)
		{
			return View(await _anusuchi.GetAnushuchi3List(id));
		}
		public async Task<IActionResult> InsertUpdateAnusuchi3(int id)
		{
			return View(await _anusuchi.GetAnushuchi3List(id));
		}
		[HttpPost]
		public async Task<IActionResult> InsertUpdateAnusuchi3(PlanningAnusuchi3ViewModel model)
		{
			var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
			if (ModelState.IsValid)
			{
				if (await _anusuchi.InsertUpdateAnusuchi3(model))
				{
					TempData["msg"] = "सफलतापूर्वक सुरक्षित भयो";
					return RedirectToAction("Details");
				}
			}
			return View(model);
		}
		#endregion

	}
}
