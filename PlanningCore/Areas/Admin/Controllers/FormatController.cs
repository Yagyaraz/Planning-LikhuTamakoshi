using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Areas.Admin.Repositories;
using PlanningCore.Data;
using PlanningCore.Utilities;

namespace PlanningCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FormatController : Controller
    {
        private readonly IUtility _utility = null;
        private readonly IFormat _format = null;
        private readonly PlanningContext _context = null;
        private readonly IPlanningSamjhauta _samjhauta = null;

        public FormatController(IUtility utility, PlanningContext context, IFormat format, IPlanningSamjhauta samjhauta)
        {
            _utility = utility;
            _context = context;
            _format = format;
            _samjhauta = samjhauta;
        }
        [HttpGet]
        public async Task<ActionResult> UpbhoktaSamitigathan(int? id)
        {
            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> PrabahdikLagat(int? id)
        {

            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }
        //[HttpGet]
        //public async Task<ActionResult> Yojanakaryalist(int id, int id)
        //{
        //    var model = await _format.GetYojanaKaryakramById(id, id);
        //    return View("Yojanakarya", new { model });
        //}
        [HttpGet]
        public async Task<ActionResult> Yojanakarya(int id)
        {
            bool Issaved = await _context.YojanaKaryakramChecklist.AnyAsync(x => x.PlanningSamjhuataId == id);
            ViewBag.IsSaved = Issaved;
            if (Issaved == true)
            {
                var data = await _format.GetYojanaKaryakramById(id);
                //return View("Yojanakarya", new { data });
                return View(data);
            }
            ViewBag.id = id;
            var model = new YojanaKaryakramChecklistViewModel();
            model.PlanningSamjhuataId = id;
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Yojanakarya(YojanaKaryakramChecklistViewModel model)
        {
            var error = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                var result = await _format.InsertUpdateYojanaKarya(model);
                if (result == true)
                {
                    TempData["Msg"] = "सुरक्षित भयो!!";
                    return RedirectToAction("Yojanakarya", new { id = model.PlanningSamjhuataId });
                }
                else
                {
                    TempData["Msg"] = ("Failed");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Tolbikashmarfat(int id)
        {
            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> Upabhotasamitibaithak(int? id)
        {
            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> Gaupalikaledeyakosuchana(int? id)
        {

            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> Baithakkonirnaya(int? id)
        {

            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> Myadthap(int? id)
        {

            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> YojanaPartibedan(int? id)
        {

            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> Anusuchisix(int? id)
        {

            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> AnusuchiFour(int? id)
        {

            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> AnitimNirnaya(int? id)
        {

            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }
        public async Task<ActionResult> YojanaHastantaran(int? id)
        {

            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }
        public async Task<ActionResult> AnitiAnugaman(int? id)
        {

            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }
        public async Task<ActionResult> NibedanKodacha(int? id)
        {

            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }
        public async Task<ActionResult> BhuktanikaLagiSifarish(int? id)
        {

            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }
        public async Task<ActionResult> BhuktanikaLagiSifarishSecond(int? id)
        {

            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }
        public async Task<ActionResult> AayojanaAnugaman(int? id)
        {

            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }
        public async Task<ActionResult> TippaniAadesh(int? id, int pesgarneId, int swkritgarneId, int sifarishgarneId, int pesgarne, int sifarishgarne, int swkritgarne)
        {
            ViewBag.PesgarneId = pesgarneId;
            ViewBag.SwkritGarneId = swkritgarneId;
            ViewBag.SifarishGarneId = sifarishgarneId;
            ViewBag.Pesgarne = pesgarne;
            ViewBag.SwkritGarne = swkritgarne;
            ViewBag.SifarishGarne = sifarishgarne;
            var data = await _format.GetAllDataForFormat(id);
            return View(data);
        }
    }
}
