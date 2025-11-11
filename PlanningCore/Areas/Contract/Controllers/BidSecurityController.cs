using Microsoft.AspNetCore.Mvc;
using PlanningCore.Areas.Contract.Interface;
using PlanningCore.Areas.Contract.Models;

namespace PlanningCore.Areas.Contract.Controllers
{
    [Area("Contract")]
    public class BidSecurityController : Controller
    {
        private readonly IBidSecurity _bid = null;
        public BidSecurityController(IBidSecurity bidSecurity)  
        {
            _bid = bidSecurity;
        }
        public async Task<IActionResult> InsertUpdateBidSecurity(int id = 0)
        {
            return View(await _bid.GetBidSecurityById(id));
        }
        [HttpPost]
        public async Task<IActionResult> InsertUpdateBidSecurity(BidSecurityViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _bid.InsertUpdateBidSecurity(model))
                {
                    TempData["msg"] = "success";
                    return RedirectToAction("BidSecurityIndex");
                }
            }
            return View(model);
        }
       
        public async Task<IActionResult> BidSecurityIndex()
        {
            return View(await _bid.GetBidSecurity());
        }
       
        public async Task<IActionResult> GetYojanaInfo(int id)
        {
            return Json(await _bid.GetYojanaInfo(id));
        }

		public async Task<IActionResult> DeleteBidSecurity(int id)
		{
			TempData["Msg"] = await _bid.DeleteBidSecurity(id) ? "सफलतापूर्वक हटाउनु भयो!!" : "कृपया पुन: प्रयास गर्नुहोस्";
			return RedirectToAction("BidSecurityIndex");
		}
	}
}
