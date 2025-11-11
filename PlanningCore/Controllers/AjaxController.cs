using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Table.PivotTable;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Areas.Contract.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;

namespace PlanningCore.Controllers
{
    public class AjaxController : Controller
    {

        private readonly IUtility _utility;
        private readonly PlanningContext _context;
        public AjaxController(IUtility utility, PlanningContext context)
        {
            _utility = utility;
            _context = context;
        }

        public async Task<JsonResult> GetDistrictByStateId(int? id)
        {
            return Json(await _utility.GetDistrictByStateId(id));
        }
        public async Task<JsonResult> GetPalikaByDistId(int id)
        {
            return Json(await _utility.GetPalikaByDistrictId(id));
        }
		//public async Task<JsonResult> GetYojanaNameBySamitiId(int id)
		//{
		//          GetInsertedFieldInSamjhautaViewModel model = new GetInsertedFieldInSamjhautaViewModel();

		//          if (model != null)
		//          {
		//              model.IsSuperAdmin =  User.IsInRole("SuperAdmin");
		//              model.IsAdmin =  User.IsInRole("Admin");
		//              model.IsUser =  User.IsInRole("User");
		//	}
		//	return Json(await _utility.GetYojanaNameBySamitiId(id));
		//}
		public async Task<JsonResult> GetYojanaNameBySamitiId(int id)
		{
			var data = await _utility.GetYojanaDetailsByUpbhokataId(id);

			var isSuperAdmin = User.IsInRole("SuperAdmin");
			var isAdmin = User.IsInRole("Admin");
			var isUser = User.IsInRole("User");

			var response = new
			{
                data,
                IsSuperAdmin = isSuperAdmin,
				IsAdmin = isAdmin,
				IsUser = isUser
			};

			return Json(response);
		}

        public async Task<JsonResult> GetYojanaNameByTolBikasSansthaId(int id)
        {
            var data = await _utility.GetYojanaDetailsByTolBikasSansthaId(id);

            var isSuperAdmin = User.IsInRole("SuperAdmin");
            var isAdmin = User.IsInRole("Admin");
            var isUser = User.IsInRole("User");

            var response = new
            {
                data,
                IsSuperAdmin = isSuperAdmin,
                IsAdmin = isAdmin,
                IsUser = isUser
            };

            return Json(response);
        }

        public async Task<JsonResult> GetYojanaNameByNewTolBikasSansthaId(int id)
        {
            var data = await _utility.GetYojanaDetailsByNewTolBikasSansthaId(id);

            var isSuperAdmin = User.IsInRole("SuperAdmin");
            var isAdmin = User.IsInRole("Admin");
            var isUser = User.IsInRole("User");

            var response = new
            {
                data,
                IsSuperAdmin = isSuperAdmin,
                IsAdmin = isAdmin,
                IsUser = isUser
            };

            return Json(response);
        }

        public async Task<JsonResult> GetCon_YojanaDetail(int id)
		{
			var data = await _context.Con_Yojana.Where(x=>x.Id==id).Select(z=> new Con_YojanaViewModel()
            {
                Id = z.Id,
                Amount = z.Amount,
                BudgetSubTypeId = z.BudgetSubTypeId,
                BudgetTypeId = z.BudgetTypeId,  
                FiscalYearId = z.FiscalYearId,  
                EstimatedAmount= z.EstimatedAmount,
                Latitude = z.Latitude,
                Longitude = z.Longitude,
                YojanaAddress= z.YojanaAddress,
                WardIds = _context.Con_Yojana_Ward.Where(y=> y.YojanaId == z.Id).Select(q=> q.WardId).ToList(),
            }).FirstOrDefaultAsync();
			if (data != null)
			{
				return Json(data);

			}
			else
			{
				return Json("Data not found");
			}
		}
		public async Task<JsonResult> GetEmpNameByPostId(int? id)
        {
            return Json(await _utility.GetEmpNameByPostId(id));
        }
        public async Task<JsonResult> GetEmpNameBySmitiPostId(int? id)
        {
            return Json(await _utility.GetEmpNameBySmitiPostId(id));
        }
        //public JsonResult GetProjectNameForBhuktani(int id)
        //{
        //    var data = (from ps in _context.PlanningSamjhauta
        //                join yj in _context.YojanaSetup on ps.YojanaId equals yj.YojanaSetupId into yoj
        //                from yj in yoj.DefaultIfEmpty()
        //                join ped in _context.ProjectEntryDetail on ps.PlanningSamjhautaId equals ped.PlanningSamjhautaId into proed
        //                from ped in proed.DefaultIfEmpty()
        //                //join us in _context.UpabhoktaSamitiDetail on ps.YojanaId equals us.YojanaId into samiti
        //                //from us in samiti.DefaultIfEmpty()
        //                join mp in _context.MunicipalitySamitiManjuriPatra on ps.PlanningSamjhautaId equals mp.PlanningSamjhautaId into manju
        //                from mp in manju.DefaultIfEmpty()
        //                join psd in _context.ProjectSourceDetail on ps.PlanningSamjhautaId equals psd.PlanningSamjhautaId into psds
        //                from psd in psds.DefaultIfEmpty()
        //                select new GetInsertedDataFromSamjhauta
        //                {
        //                    PlanningSamjhautaId = ps.PlanningSamjhautaId,
        //                    YojanaAddress = ped.Project_Place,
        //                    FiscalYearId = ps.FiscalYearId,
        //                    YojanaSetupId = yj.YojanaSetupId,
        //                    //UpabhoktaSamitiDetailId = us.UpabhoktaSamitiDetailId,
        //                    SamjhautaDate = mp.Municipality_Manjuri_Date,
        //                    EstimatedAmount = psd.Project_estimated_Amount,
        //                    Municipality = psd.Municipality
        //                }).FirstOrDefault();
        //    return Json(data);
        //}

		public async Task<JsonResult> GetKarkatti(int? id)
		{
			return Json(await _utility.GetKarkatti(id));

		}

        [HttpPost]
        public async Task<JsonResult> PrintReport(int PlanningSamjhautaId, string ReportName, string PrintContent)
        {
            return Json(await _utility.PrintReport(PlanningSamjhautaId, ReportName, PrintContent));
        }
        [HttpPost]
        public async Task<JsonResult> PrintReportUpabhokta(int UpabhoktaSamitiDetailId, string ReportName, string PrintContent)
        {
            return Json(await _utility.PrintReportUpabhota(UpabhoktaSamitiDetailId, ReportName, PrintContent));
        }

        public async Task<JsonResult> GetPrintReportContent(int PlanningSamjhautaId, string ReportName)
        {
            return Json(await _utility.GetPrintReportContent(PlanningSamjhautaId, ReportName));
        }

        public async Task<JsonResult> GetPrintReportContentUpabhokta(int UpabhoktaSamitiDetailId, string ReportName)
        {
            return Json(await _utility.GetPrintReportContentUpabhokta(UpabhoktaSamitiDetailId, ReportName));
        } 
        //public async Task<JsonResult> GetPrintReportContentNewTolBikash(int Id, string ReportName)
        //{
        //    return Json(await _utility.GetPrintReportContentUpabhokta(Id, ReportName));
        //}

              
		public async Task<JsonResult> GetEmpNameBySamitiPostId(int? id , int? samitiId)
		{
			return Json(await _utility.GetEmpNameBySamitiPostId(id , samitiId));
		}

        public async Task<JsonResult> GetEmpNameBySchoolPostId(int? id, int? samitiId)
        {
            return Json(await _utility.GetEmpNameBySchoolPostId(id, samitiId));
        }

        public async Task<JsonResult> GetEmpNameByTolBikashPostId(int? id, int? samitiId)
        {
            return Json(await _utility.GetEmpNameByTolBikashPostId(id, samitiId));
        }

        public async Task<JsonResult> Getdata(int? samitiId)
		{
			return Json(await _utility.Getdata( samitiId));
		}
		public async Task<JsonResult> GetPadaDetail(int? id)
		{
			return Json(await _utility.GetPadaDetail(id));
		}
		public async Task<JsonResult> GetPieChart()
        {
            return Json(await _utility.GetSamjhautaPieChart());
        }
        public async Task<JsonResult> GetThekkaPieChart()
        {
            return Json(await _utility.GetThekkaPieChart());
        }

        [HttpPost]
        public async Task<JsonResult> ContractPrintReportDetail(Con_PrintReportDetailViewModel model)
        {
            return Json(await _utility.ContractPrintReportDetail(model));
        }

        public async Task<JsonResult> GetUpachhetraByCheetraId(int? id)
        {
            return Json(await _utility.GetUpachhetraByCheetraId(id));
        }

        public async Task<JsonResult> GetUpachhetraDetailsByUpachhetraId(int? id)
        {
            return Json(await _utility.GetUpaChhetraDetailsList(id));
        }
        public async Task<JsonResult> CheckCitizenship(string CitizenshipNumber)
        {
            return Json(await _utility.CheckCitizenship(CitizenshipNumber));
        }
        public async Task<JsonResult> GetSchoolPost()
        {
            return Json(await _utility.GetSchoolPostList());
        }
        public async Task<JsonResult> GetSamitiPost()
        {
            return Json(await _utility.GetSamitiPadaList());
        }
        //public async Task<JsonResult> GetNewTolbikashPost()
        //{
        //    return Json(await _utility.GetNewTolBikashPostList());
        //}
    }

}


