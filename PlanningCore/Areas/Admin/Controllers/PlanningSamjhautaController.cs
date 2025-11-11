using Microsoft.AspNetCore.Mvc;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;

namespace PlanningCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PlanningSamjhautaController : Controller
    {
        private readonly IPlanningSamjhauta _planningSamjhauta = null;
        private readonly IBhuktani _bhuktani = null;
        private readonly ICommon _common = null;
        private readonly IUtility _utility = null;

        private readonly PlanningContext _Context = null;
        public PlanningSamjhautaController(IPlanningSamjhauta planningSamjhauta, IBhuktani bhuktani, ICommon common, IUtility utility, PlanningContext context)
        {
            _planningSamjhauta = planningSamjhauta;
            _utility = utility;
            _bhuktani = bhuktani;
            _common = common;
            _Context = context;
        }
        #region SamjhautaMainForm
        public async Task<IActionResult> Details(int id)
        {
            PlanningSamjhautaViewModel model = new PlanningSamjhautaViewModel();
            model.PlanningSamjhautaId = id;
            model = await _planningSamjhauta.GetPlanningSamjhautaById(id);
            //var model = await _planningSamjhauta.GetPlanningSamjhautaById(id);
            return View(model);
        }
        public async Task<IActionResult> PlanningSamjhautaIndex(int? wardId, string name)
        {
            ViewBag.wardId = wardId;
            ViewBag.name = name;
            return View(await _planningSamjhauta.GetPlanningSamjhautaAllList(wardId, name));
        }

        public async Task<IActionResult> InsertPlanningSamjhauta(int id = 0)
        {
            PlanningSamjhautaViewModel model = new PlanningSamjhautaViewModel();
            if (id > 0)
            {
                model = await _planningSamjhauta.GetPlanningSamjhautaById(id);
            }
            else
            {
                var max = _Context.PlanningEntry.Max(x => x.SerialNo);
                model.SerialNo = Convert.ToString(max == null ? 0 : Convert.ToInt32(max) + 1);
                var data = Convert.ToInt32(model.SerialNo);
                model.TaxList = await _planningSamjhauta.GetPlanningKarKatti();
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> InsertPlanningSamjhauta(PlanningSamjhautaViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                var resultSet = await _planningSamjhauta.InsertPlanningSamjhauta(model);
                if (resultSet > 0)
                {
                    TempData["Msg"] = "सम्झौता सफलतापूर्वक सुरक्षित भयो!!";
                    return RedirectToAction("Details", new { id = resultSet });
                }
                else
                {
                    TempData["Msg"] = ("Failed");
                    return View(model);
                }
            }
            TempData["Msg"] = ("Failed");
            return View(model);
        }

        public async Task<IActionResult> Delete(int id = 0)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _planningSamjhauta.DeletePlanningSamjhautaById(id))
                {
                    TempData["del"] = "सम्झौता सफलतापुर्वक रद्द भयो!!";
                    return RedirectToAction("PlanningsamjhautaIndex");
                }
            }
            return View();
        }
        public async Task<IActionResult> UpdateProjectDate(int? id, string date, string detail, string ekai, string pariman, string kaifiyat)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _planningSamjhauta.UpdateProjectDate(id, date, detail, ekai, pariman, kaifiyat))
                {
                    TempData["del"] = "Success";
                    return RedirectToAction("PlanningsamjhautaIndex");
                }
            }
            return View();
        }
        #region BhautikPragati
        public async Task<IActionResult> UpdateBhautikPragati(int? praId, decimal BittiyaPragati, decimal BhautikPragati, string Remarks)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _planningSamjhauta.UpdateBhautikPragati(praId, BittiyaPragati, BhautikPragati, Remarks))
                {
                    TempData["del"] = "Success";
                    return RedirectToAction("PlanningsamjhautaIndex");
                }
            }
            return View();
        }
        #endregion

        #endregion SamjhautaMainForm
        #region Bhuktani

  
        public async Task<IActionResult>DeleteBhukatani(int id)
        {
            if (ModelState.IsValid)
            {
                if (await _bhuktani.DeleteBhuktaniById(id))
                {
                    TempData["Msg"] = "भुक्तानि सफलतापुर्वक रद्द भयो!!";
                    return RedirectToAction("BhuktaniIndex");
                }
            }
            return View();
        }
        public async Task<IActionResult> BhuktaniIndex(int id)
        {
            var data = (await _bhuktani.GetPlanningBhuktaniListByPlanningSamjhautaId(id));
            data.PlanningSamjhautaId = id;
            ViewBag.pid = id;
            return View(data);
        }
        public async Task<IActionResult> CreateBhuktani(int PlanningSamjhautaid, int id)
        {
            PlanningBhuktaniViewModel model = new PlanningBhuktaniViewModel();
            model = await _bhuktani.GetBhuktaniFromPlanningSamjhauta(PlanningSamjhautaid);

            if (id > 0)
            {
                model = await _bhuktani.GetPlanningBhuktaniByBhuktaniId(PlanningSamjhautaid, id);
            }
            model.PlanningBhuktaniKarKattiViewModelList = _bhuktani.GetPlanningKarKatti();
            model.PlanningSamjhautaId = PlanningSamjhautaid;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBhuktani(PlanningBhuktaniViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                var resultSet = await _bhuktani.InsertUpdatePlanningBhuktani(model);
                if (resultSet > 0)
                {
                    TempData["Msg"] = "भुक्तानी सफलतापूर्वक सुरछित भयो।";
                    return RedirectToAction("BhuktaniIndex", new { id = resultSet });
                }
                else
                {
                    TempData["Msg"] = ("Failed");
                    return View(model);
                }
                //if (await _bhuktani.InsertUpdatePlanningBhuktani(model))
                //{
                //	TempData["msg"] = "success";
                //	return RedirectToAction("BhuktaniIndex");
                //}
            }
            TempData["Msg"] = ("Failed");
            return View(model);
        }
        #endregion
        #region Documents
        //public async Task<IActionResult> CreatePlanningDocument(int planningSamjhuataId)
        //{
        //	var data = await _planningSamjhauta.GetPlanningDocumentById(planningSamjhuataId);


        //	return View(data);
        //}
        public async Task<IActionResult> DocumentIndex(int id)
        {
            var data = (await _planningSamjhauta.GetDocsfromPlanningSamjhauta(id));
            data.PlanningSamjhuataId = id;
            return View(data);
        }
        public async Task<IActionResult> CreatePlanningDocument(int PlanningSamjhautaid, int id)
        {
            PlanningDocumentUploadedViewModel model = new PlanningDocumentUploadedViewModel();

            model = await _planningSamjhauta.GetDocsfromPlanningSamjhauta(PlanningSamjhautaid);
            model.PlanningSamjhuataId = PlanningSamjhautaid;
            return View(model);
        }
        //      public async Task<IActionResult> CreatePlanningDocument(int planningSamjhuataId, int id)
        //{
        //	var data = await _planningSamjhauta.GetDocsfromPlanningSamjhauta(planningSamjhuataId);


        //	return View(data);
        //}

        [HttpPost]
        public async Task<IActionResult> CreatePlanningDocument([FromForm] PlanningDocumentUploadedViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                PlanningDocumentUploadedViewModel data = new PlanningDocumentUploadedViewModel();
                data.PlanningSamjhuataId = model.PlanningSamjhuataId;
                var resultSet = await _planningSamjhauta.InsertUpdatePlanningDocument(model);
                if (resultSet > 0)
                {
                    TempData["Msg"] = "सम्झौता सफलतापूर्वक सुरक्षित भयो!!";


                    return RedirectToAction("Details", new { id = data.PlanningSamjhuataId });
                }

                else
                {
                    TempData["Msg"] = ("Failed");
                    return View(model);
                }
            }
            TempData["Msg"] = ("Failed");
            return View(model);


        }

        //DeletePlanningDocumentById
        [HttpPost]
        public async Task<IActionResult> DeletePlanningDocumentById(int id)
        {
            var data = await _planningSamjhauta.DeletePlanningDocumentId(id);
            return View(data);
        }
        public IActionResult CreatePlanningDocs() => PartialView("_AddPlanningDocuments", new PlanningDocumentUploadedViewModel());

        #endregion
        #region Letters
        #region Samjhauta
        public async Task<IActionResult> SamjhautaCompleteReport(int id)
        {
            TempData["MunicipalityManjuriDate"] = _Context.MunicipalitySamitiManjuriPatra.Where(x => x.PlanningSamjhautaId == id).Select(x => x.Municipality_Manjuri_Date).FirstOrDefault();
            var model = await _planningSamjhauta.GetPlanningSamjhautaById(id);
            return View(model);
        }
        #endregion
        #region SamjhautaGaripauPatra
        public async Task<ActionResult> SamjhautaGaripauPatra(int id, string printdate)
		{
			ViewBag.isSavedData = await _utility.CheckPrintContentAvilable(id, "SamjhautaGaripauPatra");
			var model = await _planningSamjhauta.GetPlanningSamjhautaById(id);
            model.PrintDate = printdate;
            return View(model);
        }
        #endregion
        #region SamjhautaGaridineySambandhama
        public async Task<ActionResult> SamjhautaGaridineySambandhama(int id)
        {
            var model = await _planningSamjhauta.GetPlanningSamjhautaById(id);
            return View(model);
        }
        #endregion
        #region KaryadeshDineSambandha
        public async Task<ActionResult> KaryadeshDineSambandha(int id, int? EmpId, int? PostId, string printdate)
        {
            ViewBag.isSavedData = await _utility.CheckPrintContentAvilable(id, "KaryadeshDineSambandha");
            var model = await _planningSamjhauta.GetPlanningSamjhautaById(id);
            ViewBag.EmpId = EmpId;
            ViewBag.PostId = PostId;
            model.PrintDate = printdate;
            return View(model);
        }
        #endregion
        #region YojanaAnugamanTahaSamitiPratibedan
        public async Task<ActionResult> YojanaAnugamanTahaSamitiPratibedan(int id, int? EmpId, int? PostId, string printdate)
		{
			ViewBag.isSavedData = await _utility.CheckPrintContentAvilable(id, "YojanaAnugamanTahaSamitiPratibedan");
			var model = await _planningSamjhauta.GetPlanningSamjhautaById(id);
            ViewBag.EmpId = EmpId;
            ViewBag.PostId = PostId;
            model.PrintDate = printdate;
            return View(model);
        }
        #endregion
        #region UpavoktaSamitiGathanPratibedhan
        public async Task<ActionResult> UpavoktaSamitiGathanPratibedhan(int id, int? EmpId, int? PostId, string printdate)
		{
			ViewBag.isSavedData = await _utility.CheckPrintContentAvilable(id, "UpavoktaSamitiGathanPratibedhan");
			var model = await _planningSamjhauta.GetPlanningSamjhautaById(id);
            ViewBag.EmpId = EmpId;
            ViewBag.PostId = PostId;
            model.PrintDate = printdate;
            return View(model);
        }
        #endregion
        #region Karyadesh

        public async Task<ActionResult> Karyadesh(int id, int? EmpId, int? PostId, string printdate)
        {
            ViewBag.isSavedData = await _utility.CheckPrintContentAvilable(id, "Karyadesh");
            var model = await _planningSamjhauta.GetPlanningSamjhautaById(id);
            ViewBag.EmpId = EmpId;
            ViewBag.PostId = PostId;
            model.PrintDate = printdate;
            return View(model);
        }
        #endregion
        #region SamjhautaReport
        public async Task<ActionResult> SamjhautaReport(int id, int? EmpId, int? PostId, string printdate)
        {
            var model = await _planningSamjhauta.GetPlanningSamjhautaById(id);
            ViewBag.EmpId = EmpId;
            ViewBag.PostId = PostId;
			model.PrintDate = printdate;
            return View(model);
        }
        #endregion
        #region BankForm
        public async Task<ActionResult> BankForm(int id, int? EmpId, int? PostId, string printdate)
		{
			ViewBag.isSavedData = await _utility.CheckPrintContentAvilable(id, "BankForm");
			var model = await _planningSamjhauta.GetPlanningSamjhautaById(id);
            ViewBag.EmpId = EmpId;
            ViewBag.PostId = PostId;
            model.PrintDate = printdate;
            return View(model);
        }

        public async Task<ActionResult> BankClose(int id, int? EmpId, int? PostId, string printdate)
		{
			ViewBag.isSavedData = await _utility.CheckPrintContentAvilable(id, "BankClose");
			var model = await _planningSamjhauta.GetPlanningSamjhautaById(id);            
            ViewBag.EmpId = EmpId;
            ViewBag.PostId = PostId;
            model.PrintDate = printdate;
            return View(model);
        }

        #endregion
        #region SifarishGariyeko
        public async Task<ActionResult> SifarishGariyeko(int id, int? EmpId, int? PostId, string printdate)
		{
			ViewBag.isSavedData = await _utility.CheckPrintContentAvilable(id, "SifarishGariyeko");
			var model = await _planningSamjhauta.GetPlanningSamjhautaById(id);
            ViewBag.EmpId = EmpId;
            ViewBag.PostId = PostId;
            model.PrintDate = printdate;
            return View(model);
        }
        #endregion
        #region SamjhautaGarnapathayeko
        public async Task<ActionResult> SamjhautaGarnaPathayeko(int id, int? EmpId, int? PostId, string printdate)
		{
			ViewBag.isSavedData = await _utility.CheckPrintContentAvilable(id, "SamjhautaGarnaPathayeko");
			var model = await _planningSamjhauta.GetPlanningSamjhautaById(id);
            ViewBag.EmpId = EmpId;
            ViewBag.PostId = PostId;
            model.PrintDate = printdate;
            return View(model);
        }
        #endregion
        #region BhuktaniReport
        public async Task<ActionResult> BhuktaniReport(int id, int? EmpId, int? PostId, string printdate,int pid)        
        {
            var model = await _bhuktani.GetPlanningBhuktaniByBhuktaniId(pid,id);
            model.PlanningBhuktaniKarKattiViewModelList = _bhuktani.GetPlanningKarKatti();
            ViewBag.EmpId = EmpId;
            model.PlanningSamjhautaId = pid;
            ViewBag.PostId = PostId;
            model.PrintDate = printdate;
            ViewBag.isSavedData = await _utility.CheckPrintContentAvilable(pid, "BhuktaniReport");
            return View(model);
        }

        #endregion
        #endregion
        #region Pragati Pratibedhan
        public ActionResult Anusuchi1(string FiscalYear, string OdaNagar, string ProjectName, string Type)
        {
            return null;
        }
        #endregion
        #region Nonsamjhauta
        public async Task<IActionResult> NonSamjhautaIndex()
        {
            return View(await _planningSamjhauta.GetNonSamjhauta());
        }
        #endregion

        public async Task<ActionResult> SuchanaPatti(int id)
        {
            ViewBag.isSavedData = await _utility.CheckPrintContentAvilable(id, "SuchanaPatti");
            var model = await _planningSamjhauta.GetPlanningSamjhautaById(id);
            return View(model);
        }
        #region BhuktaniKoLagiSifarish
        public async Task<ActionResult> BhuktaniKoLagiSifarish(int id, string RunningBillBhuktaniAmt, string BhuktaniDineyAmt, string printdate)
        {
            ViewBag.isSavedData = await _utility.CheckPrintContentAvilable(id, "BhuktaniKoLagiSifarish");
            var model = await _planningSamjhauta.GetPlanningSamjhautaById(id);
            ViewBag.RunningBillBhuktaniAmt = RunningBillBhuktaniAmt;
            ViewBag.BhuktaniDineyAmt = BhuktaniDineyAmt;
            model.PrintDate = printdate;
            return View(model);
        }
        #endregion
    }
}





