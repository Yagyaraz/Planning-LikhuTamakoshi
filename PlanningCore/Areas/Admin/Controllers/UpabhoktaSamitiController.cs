using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;

namespace PlanningCore.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class UpabhoktaSamitiController : Controller
    {
        private readonly IUpabhoktaSamiti _upabhokta = null;
        private readonly PlanningContext _context = null;
        private readonly IUtility _utility;
        #region upabhokta
        public UpabhoktaSamitiController(IUpabhoktaSamiti upabhokta, PlanningContext context, IUtility utility)
        {
            _upabhokta = upabhokta;
            _context = context;
            _utility = utility;
        }
        public async Task<IActionResult> SamitiIndex()
        {
            return View(await _upabhokta.GetAllUpavoktaSamitiDetail());
        }
        public async Task<IActionResult> CreateUpabhoktaSamiti(int id = 0)
        {
            UpavoktaSamitiDetailViewModel model = new UpavoktaSamitiDetailViewModel();
            if (id > 0)
            {
                model = await _upabhokta.GetUpavoktaSamitiDetailById(id);
            }
            else
            {
                var max = _context.UpabhoktaSamitiDetail.Max(x => x.DartaNo);
                model.DartaNo = Convert.ToString(max == null ? 1 : Convert.ToInt32(max) + 1);

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUpabhoktaSamiti([FromForm] UpavoktaSamitiDetailViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _upabhokta.CreateUpabhoktaSamiti(model))
                {
                    TempData["msg"] = "success";
                    return RedirectToAction("SamitiIndex");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _upabhokta.GetUpavoktaSamitiDetailById(id));
        }
        public async Task<IActionResult> Darta(int id, int? EmpId, int? PostId, string printdate)
        {
            ViewBag.EmpId = EmpId;
            ViewBag.PostId = PostId;
            var model = await _upabhokta.GetUpavoktaSamitiDetailById(id);
            model.PrintDate = printdate;
            ViewBag.isSavedData = await _utility.CheckPrintContentAvilableUpaBhokta(id, "Darta");
            return View(model);
        }
        public async Task<IActionResult> DartaRautamai(int id, int? EmpId, int? PostId, string printdate)
        {
            ViewBag.EmpId = EmpId;
            ViewBag.PostId = PostId;
            var model = await _upabhokta.GetUpavoktaSamitiDetailById(id);
            model.PrintDate = printdate;
            ViewBag.isSavedData = await _utility.CheckPrintContentAvilableUpaBhokta(id, "DartaRautamai");
            return View(model);
        }
        public IActionResult CreateSamitiMemberList() => PartialView("_SamitiMemberList", new UpavoktaSamitiMemberDetailViewModel());
        public IActionResult CreateAnugamanMember() => PartialView("_CreateAnugaman", new AnugamanViewModel());
        public async Task<IActionResult> Delete(int id = 0)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _upabhokta.DeleteUpavoktaSamitiDetailById(id))
                {
                    TempData["del"] = "तपाँइको उपभोक्ता समिति सफलतापूर्वक रद्द भयो!!";
                    return RedirectToAction("SamitiIndex");
                }
            }
            return View();
        }

        #endregion
        #region Tolbikash       
        public async Task<IActionResult> TolBikashIndex()
        {
            return View(await _upabhokta.GetTolBikashSansthaList());
        }
        public async Task<IActionResult> CreateTolbikashSamiti(int id = 0)
        {
            return View(await _upabhokta.GetTolBikashSansthaById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateTolbikashSamiti(TolBikashSansthaViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _upabhokta.CreateTolbikashSamiti(model))
                {
                    TempData["msg"] = "success";
                    return RedirectToAction("TolBikashIndex");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> TolBikashDetails(int id)
        {
            return View(await _upabhokta.GetTolBikashSansthaById(id));
        }
        public IActionResult CreateTolBikashList() => PartialView("_TolbikashMemberList", new TolBikashSansthaMemberViewModel());
        public async Task<IActionResult> Updatebiupurji(int? id, decimal Amount)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _upabhokta.Updatebiupurji(id, Amount))
                {
                    TempData["del"] = "Success";
                    return RedirectToAction("TolBikashIndex");
                }
            }
            return View();
        }
        public async Task<IActionResult> DeleteTolBikas(int id = 0)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _upabhokta.DeleteTolBikashSansthaById(id))
                {
                    TempData["delete"] = "तपाँइको बिद्यालय व्यवस्थापन समिति सफलतापूर्वक रद्द भयो!!";
                    return RedirectToAction("TolBikashIndex");
                }
            }
            return View();
        }
        public async Task<IActionResult> TolBikashDarta(int id, int? EmpId, int? PostId, string printdate)
        {
            ViewBag.EmpId = EmpId;
            ViewBag.PostId = PostId;
            var model = await _upabhokta.GetTolBikashSansthaById(id);
            model.PrintDate = printdate;
            ViewBag.isSavedData = await _utility.CheckPrintContentAvilableUpaBhokta(id, "TolBikashDarta");
            return View(model);
        }
        #endregion
        #region NewTolbikash
        public async Task<IActionResult> NewTolBikashIndex()
        {
            return View(await _upabhokta.GetAllNewToleBikash());
        }
        public async Task<IActionResult> CreateNewTolbikashSamiti(int id = 0)
        {
            NewToleBikashViewModel model = new NewToleBikashViewModel();
            if (id > 0)
            {
                model = await _upabhokta.GetNewToleBikashById(id);
            }
            else
            {
                var max = _context.NewToleBikash.Max(x => x.DartaNo);
                model.DartaNo = Convert.ToString(max == null ? 1 : Convert.ToInt32(max) + 1);

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewTolbikashSamiti([FromForm] NewToleBikashViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _upabhokta.CreateUpabhoktaSamiti(model))
                {
                    TempData["msg"] = "success";
                    return RedirectToAction("NewTolBikashIndex");
                }
            }
            return View(model);
        }
        public IActionResult CreateNewTolbikashSamitiMemberList() => PartialView("_NewTolBikashSamitiMemberList", new NewToleBikashMemberDetailViewModel());
        public IActionResult CreateNewTolBikashAnugamanMember() => PartialView("_NewTolBikashCreateAnugaman", new NewToleBikashAnugamanMemberViewModel());
        public async Task<IActionResult> DeleteNewTolBikas(int id = 0)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _upabhokta.DeleteNewToleBikashById(id))
                {
                    TempData["deletetol"] = "तपाँइको टोल बिकाश समिति सफलतापूर्वक रद्द भयो!!";
                    return RedirectToAction("NewTolBikashIndex");
                }
            }
            return View();
        }
        public async Task<IActionResult> NewTolBikashDarta(int id, int? EmpId, int? PostId, string printdate)
        {
            ViewBag.EmpId = EmpId;
            ViewBag.PostId = PostId;
            var model = await _upabhokta.GetNewToleBikashById(id);
            model.PrintDate = printdate;
            ViewBag.isSavedData = await _utility.CheckPrintContentAvilableUpaBhokta(id, "NewTolBikashDarta");
            return View(model);
        }
        #endregion
        public async Task<IActionResult> SamitiReport()
        {
            return View(await _upabhokta.GetAllUpavoktaSamitiDetail());
        }

        public async Task<IActionResult> CheckSamitMember(string ctzno, int samitiId)
        {
            return Json(await _upabhokta.CheckSamitMember(ctzno, samitiId));
        }

        public async Task<IActionResult> UpbhokataDocs(int id)
        {
            return View(await _upabhokta.GetDocsListByUpbhokataId(id));
        }

        [HttpPost]
        public async Task<IActionResult> UpbhokataDocs(UpabhoktaSamitiDetailDocsViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _upabhokta.InsertUpdateDocsUpload(model))
                {
                    TempData["msg"] = "success";
                    return RedirectToAction("SamitiIndex");
                }
            }
            return View(model);
        }







    }
}
