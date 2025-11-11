using Microsoft.AspNetCore.Mvc;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Areas.Contract.Interface;
using PlanningCore.Areas.Contract.Models;
using System.Linq.Expressions;

namespace PlanningCore.Areas.Contract.Controllers
{
    [Area("Contract")]
    public class ContractSamjhautaController : Controller
    {
        private readonly IContractSamjhauta _contract = null;
        public ContractSamjhautaController(IContractSamjhauta contract)
        {
            _contract = contract;
        }
        #region Karkatti
        public async Task<IActionResult> ThekkaKarkattiIndex()
        {
            return View(await _contract.GetAllThekkaKarkatti());
        }
        public async Task<IActionResult> CreateContractKarkatti(int id = 0)
        {
            return View(await _contract.GetContractKarkatttiById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateContractKarkatti(Con_KarKattiViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _contract.CreateContractKarkatti(model))
                {
                    TempData["msg"] = "कर-कट्टी सफलतापूर्वक सुरक्षित भयो";
                    return RedirectToAction("ThekkaKarkattiIndex");
                }
            }
            TempData["msg"] = "Failed";
            return View(model);
        }

        public async Task<IActionResult> DeleteContractKarkatti(int id)
        {
            TempData["Msg"] = await _contract.DeleteContractKarkatti(id) ? "सफलतापूर्वक हटाउनु भयो!!" : "कृपया पुन: प्रयास गर्नुहोस्";
            return RedirectToAction("ThekkaKarkattiIndex");
        }
        #endregion

        #region Contract
        public async Task<IActionResult> InsertContractSamjhauta(int id = 0)
        {
            var data = await _contract.GetContractById(id);
            //data.WardIds = [2,3];
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> InsertContractSamjhauta(ContractSamjhautaViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _contract.InsertContractSamjhauta(model))
                {
                    TempData["msg"] = "success";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Index()
        {
            return View(await _contract.GetAllContract());
        }
        #endregion
        #region Contract Bima
        public async Task<IActionResult> ContractBimaIndex(int id)
        {
            ViewBag.ContractId = id;
            var contract = await _contract.GetContractById(id);
            ViewBag.ContractName = contract.YojanaName;
            var data = await _contract.GetAllInsuranceByContractId(id);
            return View(data);
        }
        public async Task<IActionResult> InsertContractBima(int contractSamjhautaId, int id)
        {
            var data = new InsuranceViewModel();
            var contract = await _contract.GetContractById(contractSamjhautaId);
            ViewBag.ContractName = contract.YojanaName;
            data = await _contract.GetInsuranceById(id) ?? new InsuranceViewModel();
            data.ConSamjhautaId = contractSamjhautaId;
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> InsertContractBima(InsuranceViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            ViewBag.ContractId = model.ConSamjhautaId;
            if (ModelState.IsValid)
            {
                if (await _contract.CreateInsurance(model))
                {
                    TempData["msg"] = "success";
                    return RedirectToAction("ContractBimaIndex", new { id = model.ConSamjhautaId });
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteInsurance(int id, int contractSamjhautaId)
        {
            var data = await _contract.DeleteInsurance(id);
            if (data)
            {
                return RedirectToAction("ContractVariationIndex", new { id = contractSamjhautaId });
            }
            return View();
        }
        #endregion
        #region Contract Variation
        public async Task<IActionResult> ContractVariationIndex(int id)
        {
            ViewBag.ContractId = id;
            var contract = await _contract.GetContractById(id);
            ViewBag.ContractName = contract.YojanaName;
            var data = await _contract.GetAllVariationByContractId(id);
            return View(data);
        }
        public async Task<IActionResult> InsertContractVariation(int contractSamjhautaId, int id)
        {
            var data = new VariationViewModel();
            var contract = await _contract.GetContractById(contractSamjhautaId);
            ViewBag.ContractName = contract.YojanaName;
            data = await _contract.GetVariationById(id) ?? new VariationViewModel();
            data.ConSamjhautaId = contractSamjhautaId;
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> InsertContractVariation(VariationViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            ViewBag.ContractId = model.ConSamjhautaId;
            if (ModelState.IsValid)
            {
                if (await _contract.CreateVariation(model))
                {
                    TempData["msg"] = "success";
                    return RedirectToAction("ContractVariationIndex", new { id = model.ConSamjhautaId });
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteVariation(int id, int contractSamjhautaId)
        {
            var data = await _contract.DeleteVariation(id);
            if (data)
            {
                return RedirectToAction("ContractVariationIndex", new { id = contractSamjhautaId });
            }
            return View();
        }
        #endregion
        #region letters
        public async Task<IActionResult> ContractAgreement(int id = 0)
        {
            return View(await _contract.GetContractById(id));
        }
        #endregion
        #region Bhuktani
        public async Task<IActionResult> ContractBhuktaniIndex(int id)
        {
            var data = (await _contract.GetBhuktaniListByContractSamjhautaId(id));
            data.ContractSamjhautaId = id;
            return View(data);
        }
        public async Task<IActionResult> CreateContractBhuktani(int ContractSamjhautaid, int id)
        {
            Con_BhuktaniViewModel model = new Con_BhuktaniViewModel();

            model = await _contract.GetContractBhuktaniByBhuktaniId(ContractSamjhautaid);
            //model.PlanningBhuktaniKarKattiViewModelList = _bhuktani.GetPlanningKarKatti();
            model.ContractSamjhautaId = ContractSamjhautaid;
            return View(model);
        }
        //      public async Task<IActionResult> CreateBhuktani(int id)
        //      {
        //	return View(await _bhuktani.GetPlanningBhuktaniListByPlanningSamjhautaId(id));
        //}


        [HttpPost]
        public async Task<IActionResult> CreateContractBhuktani(Con_BhuktaniViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                var resultSet = await _contract.InsertUpdateContractBhuktani(model);
                if (resultSet > 0)
                {
                    TempData["Msg"] = "भुक्तानी सफलतापूर्वक सुरछित भयो।";
                    return RedirectToAction("ContractBhuktaniIndex", new { id = resultSet });
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
        public IActionResult CreateTaxDeduction() => PartialView("_TaxDeduction", new Con_TaxDeductionViewModel());

        #endregion
    }
}
