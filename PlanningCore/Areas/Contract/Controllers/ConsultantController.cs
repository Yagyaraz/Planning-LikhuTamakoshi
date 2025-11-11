using Microsoft.AspNetCore.Mvc;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Areas.Contract.Interface;
using PlanningCore.Areas.Contract.Models;
using System.Diagnostics.Contracts;

namespace PlanningCore.Areas.Contract.Controllers
{
	[Area("Contract")]
	public class ConsultantController : Controller
	{
		private readonly IConsultant _consultant = null;
		public ConsultantController(IConsultant consultant)
		{
			_consultant = consultant;
		}
		public async Task<IActionResult> InsertConsultant(int id = 0)
		{
			return View(await _consultant.GetConsultantById(id));
		}
		[HttpPost]
		public async Task<IActionResult> InsertConsultant(ConsultantViewModel model)
		{
			var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
			if (ModelState.IsValid)
			{
				if (await _consultant.InsertConsultant(model))
				{
					TempData["msg"] = "success";
					return RedirectToAction("ConsultantIndex");
				}
			}
			return View(model);
		}
		public IActionResult CreateJV() => PartialView("_CreateJointVenture", new JointVentureViewModel());

		public async Task<IActionResult> ConsultantIndex()
		{
			return View(await _consultant.GetAllConsultant());
		}

		public async Task<IActionResult> DeleteConsultant(int id)
		{
			TempData["Msg"] = await _consultant.DeleteConsultant(id) ? "सफलतापूर्वक हटाउनु भयो!!" : "कृपया पुन: प्रयास गर्नुहोस्";
			return RedirectToAction("InsertConsultant");
		}
	}
}
