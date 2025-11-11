using Microsoft.AspNetCore.Mvc;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;

namespace PlanningCore.Areas.Admin.Controllers
{
    [Area("Admin")]   

    public class TravelExpenseController : Controller
    {
        private readonly ITravelExpense _travel = null;
        public TravelExpenseController(ITravelExpense travel)
        {
            _travel = travel;
        }
        #region Travel
        public async Task<IActionResult> Index()
        {
            return View(await _travel.GetAllTravelExpenses());
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _travel.GetTravelExpenseById(id));
        }
        public async Task<IActionResult> Create(int id = 0)
        {
            return View(await _travel.GetTravelExpenseById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create(TravelExpenseViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _travel.CreateTravelExpense(model))
                {
                    TempData["msg"] = "सफलतापूर्वक सुरक्षित भयो";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        public IActionResult CreateTravelExpenseOther() => PartialView("_ExpenseOtherItems", new TravelRequiredItemsViewModel());
        #endregion

    }
}
