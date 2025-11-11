using Microsoft.AspNetCore.Mvc;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;

namespace PlanningCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private readonly IReport _report = null;
        public ReportController(IReport report)
        {
            _report = report;
        }

        public async Task<IActionResult> Samjhuta(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId)
        {
            ViewBag.fiscalId = fiscalYearId;
            ViewBag.yojanaId = yojanaId;
            ViewBag.budgetId = budgetId;
            ViewBag.wardId = wardId;
            return View(await _report.Samjhuta(fiscalYearId, yojanaId, budgetId, wardId));
        }

        public async Task<IActionResult> InCompleteSamjhuta(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId)
        {
            ViewBag.fiscalId = fiscalYearId;
            ViewBag.yojanaId = yojanaId;
            ViewBag.budgetId = budgetId;
            ViewBag.wardId = wardId;
            return View(await _report.InCompleteSamjhuta(fiscalYearId, yojanaId, wardId));
        }

        public async Task<IActionResult> CompleteSamjhuta(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId)
        {
            ViewBag.fiscalId = fiscalYearId;
            ViewBag.yojanaId = yojanaId;
            ViewBag.budgetId = budgetId;
            ViewBag.wardId = wardId;
            return View(await _report.CompleteSamjhuta(fiscalYearId, yojanaId, budgetId, wardId));
        }

        public async Task<IActionResult> BhotikPratiwadan(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId)
        {
            ViewBag.fiscalId = fiscalYearId;
            ViewBag.yojanaId = yojanaId;
            ViewBag.budgetId = budgetId;
            ViewBag.wardId = wardId;
            return View(await _report.BhotikPratiwadan(fiscalYearId, yojanaId, budgetId, wardId));
        }

        public async Task<IActionResult> BitiyaPratiwadan(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId)
        {
            ViewBag.fiscalId = fiscalYearId;
            ViewBag.yojanaId = yojanaId;
            ViewBag.budgetId = budgetId;
            ViewBag.wardId = wardId;
            return View(await _report.BitiyaPratiwadan(fiscalYearId, yojanaId, budgetId, wardId));
        }

        #region wardwiseReport
        public async Task<IActionResult> PrintReport(int? PlanningSamjhautaId)
        {
            var data = await _report.GetAllReportById(PlanningSamjhautaId ?? 0);
            return View(data);
        }
        public async Task<IActionResult> GetAllReports()
        {
            return View(await _report.GetAllReports());
        }
        #endregion

        #region MontlyReport


        #endregion
    }
}
