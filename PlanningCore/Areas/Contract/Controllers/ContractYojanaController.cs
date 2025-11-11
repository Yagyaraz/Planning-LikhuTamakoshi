using Microsoft.AspNetCore.Mvc;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Areas.Contract.Interface;
using PlanningCore.Areas.Contract.Models;
using System.Diagnostics.Contracts;

namespace PlanningCore.Areas.Contract.Controllers
{
    [Area("Contract")]
    public class ContractYojanaController : Controller
    {
        private readonly IContractYojana _contractYojana = null;
        public ContractYojanaController(IContractYojana contractYojana)
        {
            _contractYojana = contractYojana;
        }
        #region
        public async Task<IActionResult> ContractYojanaIndex()
        {
            return View(await _contractYojana.GetAllContractYojana());
        }
        public async Task<IActionResult> InsertUpdateContractYojana(int id = 0)
        {
            TempData["msg"] = "";

            return View(await _contractYojana.GetContractYojanaById(id));
        }
        [HttpPost]
        public async Task<IActionResult> InsertUpdateContractYojana(Con_YojanaViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _contractYojana.InsertUpdateContractYojana(model))
                {
                    TempData["msg"] = "success";
                    return RedirectToAction("ContractYojanaIndex");
                }
            }
            else
            {
                TempData["msg"] = "Failed";
            }

            return View(model);
        }

		public async Task<IActionResult> DeleteContractYojana(int id)
		{
			TempData["Msg"] = await _contractYojana.DeleteContractYojana(id) ? "सफलतापूर्वक हटाउनु भयो!!" : "कृपया पुन: प्रयास गर्नुहोस्";
			return RedirectToAction("ContractYojanaIndex");
		}
		#endregion
	}
}
