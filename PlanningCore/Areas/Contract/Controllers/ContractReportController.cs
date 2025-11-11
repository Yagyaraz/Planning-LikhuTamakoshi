// using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using PlanningCore.Areas.Contract.Interface;
using PlanningCore.Areas.Contract.Models;
using PlanningCore.Utilities;

namespace PlanningCore.Areas.Contract.Controllers
{
    [Area("Contract")]
    public class ContractReportController : Controller
    {
        public static IUtility _utility = null;
        public static IContractSamjhauta _contractSamjhauta = null;
        public static IContractReport _contractReport = null;
        public ContractReportController(IUtility utility, IContractSamjhauta contractSamjhauta, IContractReport contractReport)
        {
            _utility = utility;
            _contractSamjhauta = contractSamjhauta;
            _contractReport = contractReport;
        }

        public IActionResult Index()
        {
            return View(new Con_ReportViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> SearchResult(Con_ReportViewModel model)
        {
            var getReportCode = "";
            ViewBag.EmpId = model.EmpId;
            ViewBag.PostId = model.PostId;
            var data = new ContractSamjhautaViewModel();
            var checkPrintedOrNot = await _contractSamjhauta.CheckAndGetContractReportData(model.YojanaId, model.ReportId);
            if (checkPrintedOrNot.PrintContent == null)
            {
                data = await _contractSamjhauta.GetContractReportDataByYojanaId(model.YojanaId);
            }
            else
            {
                TempData["Message"] = "Already Printed!!";
                data = checkPrintedOrNot;
            }

            if (model.ReportId > 0)
            {
                getReportCode = await _utility.GetThekkaReportCodeById(model.ReportId);
            }
            switch (getReportCode)
            {
                case "K":
                    return PartialView("_ThekkaKaryadeshReport", data);
                case "FirstRunningBill":
                    return PartialView("_ThekkaFirstRunningBillReport", data);
                case "AntimRunningBill":
                    return PartialView("_AntimRunningBillReport", data);
                case "ContractAgreement":
                    return PartialView("_ContractAgreement", data);             
                case "Mobilization":
                    return PartialView("_Mobilization", data);  
                case "AgreementEnglish":
                    return PartialView("_AgreementEnglish", data);

                default:
                    return View(model);
            }
        }

        public async Task<IActionResult> GetAllContractReports()
        {
            return View(await _contractReport.GetAllReports());
        }

        public async Task<IActionResult> PrintContractReport(int id)
        {
            return View(await _contractReport.GetAllReportById(id));
        }
    }
}
