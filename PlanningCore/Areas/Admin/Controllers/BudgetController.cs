using Microsoft.AspNetCore.Mvc;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;

namespace PlanningCore.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BudgetController : Controller
	{
		private readonly IBudget _budget = null;

		public BudgetController(IBudget budget)
		{
			_budget = budget;
		}
		#region budget
		public async Task<IActionResult> BudgetSourceIndex()
		{
			return View(await _budget.GetAllBudgetSource());
		}
		public async Task<IActionResult> CreateBudgetSource(int id = 0)
		{
			return View(await _budget.GetBudgetSourceById(id));
		}
		[HttpPost]
		public async Task<IActionResult> CreateBudgetSource(BudgetSourceViewModel model)
		{
			var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
			if (ModelState.IsValid)
			{
				if (await _budget.InsertUpdateBudgetSource(model))
				{
					TempData["msg"] = "success";
					return RedirectToAction("BudgetSourceIndex");
				}
			}
			return View(model);
        }
        public async Task<IActionResult> DeleteBudgetSource(int id)
        {
            TempData["Msg"] = await _budget.DeleteBudgetSourceById(id) ? "सफलतापूर्वक हटाउनु भयो!!" : "कृपया पुन: प्रयास गर्नुहोस्";
            return RedirectToAction("BudgetSourceIndex");
        }
        public async Task<IActionResult> Details(int id)
		{
			return View(await _budget.GetBudgetSourceById(id));
		}
		#endregion
		#region BudgetType
		public async Task<IActionResult> BudgetTypeIndex()
		{
			return View(await _budget.GetAllBudgetType());
		}
		public async Task<IActionResult> CreateBudgetType(int id = 0)
		{
			return View(await _budget.GetBudgetTypeById(id));
		}
		[HttpPost]
		public async Task<IActionResult> CreateBudgetType(BudgetTypeViewModel model)
		{
			var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
			if (ModelState.IsValid)
			{
				if (await _budget.InsertUpdateBudgetType(model))
				{
					TempData["msg"] = "success";
					return RedirectToAction("BudgetTypeIndex");
				}
			}
			return View(model);
		}

		#endregion

	}
}
