using Microsoft.AspNetCore.Mvc;
using PlanningCore.Areas.Contract.Interface;
using PlanningCore.Areas.Contract.Models;

namespace PlanningCore.Areas.Contract.Controllers
{
    [Area("Contract")]
    public class BankGuaranteeController : Controller
    {
        private readonly IBankGuarantee _bankGuarantee = null;
        public BankGuaranteeController(IBankGuarantee bankguarantee)
        {
            _bankGuarantee = bankguarantee;
        }
        public async Task<IActionResult> InsertUpdateBankGuarantee(int id = 0)
        {
            return View(await _bankGuarantee.GetBankGuaranteeById(id));
        }
        [HttpPost]
        public async Task<IActionResult> InsertUpdateBankGuarantee(BankGuaranteeViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _bankGuarantee.InsertUpdateBankGuarantee(model))
                {
                    TempData["msg"] = "success";
                    return RedirectToAction("BankGuaranteeIndex");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> BankGuaranteeIndex()
        {
            return View(await _bankGuarantee.GetBankGuarantee());
        }

        public IActionResult PartialBankGuarantee()
        {
            return PartialView("_PartialBankGuarantee", new BankGuaranteeViewModel());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _bankGuarantee.GetBankGuaranteeById(id));
        }

        public async Task<IActionResult> BhuktaniIndex(int id)
        {
            return View();
        }


    }
}
