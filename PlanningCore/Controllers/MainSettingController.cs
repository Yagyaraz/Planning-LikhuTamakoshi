using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Models;
using PlanningCore.Utilities;

namespace PlanningCore.Controllers
{
	
	public class MainSettingController : Controller
    {
        private readonly IUtility _utility;
        public MainSettingController(IUtility utility)
        {
            _utility = utility;
        }
		
		public async Task<IActionResult> MainSetting()
        {
            return View(await _utility.GetAllMainSettings());
        }
        public async Task<IActionResult> CreateMainSetting(int id = 0)
        {
            return View(await _utility.GetMainSettingById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateMainSetting([FromForm] MainSettingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _utility.InsertUpdateMainSetting(model))
                {
                    TempData["Msg"] = "सफलतापूर्वक सुरक्षित भयो!!";
                    return RedirectToAction("MainSetting");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> WardIndex()
        {
            return View(await _utility.GetAllWards());
        }
        public async Task<IActionResult> CreateWard(int id = 0)
        {
			return View(await _utility.GetWardById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateWard(WardViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _utility.InsertUpdateWard(model))
                {
                    return RedirectToAction("WardIndex");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteWard(int id)
        {
            TempData["Msg"] = await _utility.DeleteWard(id) ? "सफलतापूर्वक हटाउन भयो!!" : "कृपया पुन: प्रयास गर्नुहोस्";
            return RedirectToAction("WardIndex");
        }


    }
}
