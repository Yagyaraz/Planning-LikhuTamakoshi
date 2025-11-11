
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;
using System.Security.Policy;


namespace PlanningCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommonController : Controller
    {
        private readonly ICommon _common = null;
        public CommonController(ICommon common)
        {
            _common = common;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region fiscalyear
        public async Task<IActionResult> FiscalYearIndex()
        {
            return View(await _common.GetAllFiscalYear());
        }

        public async Task<IActionResult> CreateFiscalYear(int id = 0)
        {
            return View(await _common.GetFiscalYear(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFiscalYear(FiscalYearViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateFiscalYear(model))
                {
                    TempData["msg"] = "सफलतापूर्वक सुरक्षित भयो";
                    return RedirectToAction("FiscalYearIndex");
                }
            }
            return View(model);
        }
        #endregion
        #region Pada
        public async Task<IActionResult> PadaIndex()
        {
            return View(await _common.GetAllPada());
        }
        public async Task<IActionResult> CreatePada(int id = 0)
        {
            return View(await _common.GetPadaById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreatePada(PadaViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdatePada(model))
                {
                    TempData["msg"] = "सफलतापूर्वक सुरक्षित भयो";
                    return RedirectToAction("PadaIndex");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeletePada(int id)
        {
            TempData["Msg"] = await _common.DeletePada(id) ? "सफलतापूर्वक हटाउन भयो!!" : "कृपया पुन: प्रयास गर्नुहोस्";
            return RedirectToAction("PadaIndex");
        }
        #endregion
        #region Employee
        public async Task<IActionResult> EmployeeIndex()
        {
            return View(await _common.GetAllEmployee());
        }
        public async Task<IActionResult> CreateEmployee(int id = 0)
        {
            return View(await _common.GetEmployeeById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateEmployee(model))
                {
                    TempData["msg"] = "सफलतापूर्वक सुरक्षित भयो";
                    return RedirectToAction("EmployeeIndex");
                }
            }
            return View(model);
		}
		public async Task<IActionResult> DeleteEmployee(int id)
		{
            var aa = await _common.DeleteEmployee(id);
			return RedirectToAction("EmployeeIndex");
		}
        #endregion
        #region AnugumanSamiti
        public async Task<IActionResult> AnugumanSamitiIndex()
        {
            var data = await _common.GetAnugumanSamiti();
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> CreateAnugamanSamiti(int?id)
        {
            var data = await _common.GetAnugumanSamitiById(id);
			return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAnugamanSamiti(AnugamanSamitiSetupViewModel model)
        {
            var error= ModelState.Where(x=>x.Value.Errors.Count>0).Select(x=> new {x.Key, x.Value.Errors}).ToArray();
            if (ModelState.IsValid)
            {
                if(await _common.InsertUpdateAnugamanSamiti(model))
                {
					TempData["msg"] = "success";
					return RedirectToAction("AnugumanSamitiIndex");
				}
			}
			return View(model);
		}
        public IActionResult CreateAnugumanMemberList() =>PartialView("_AnugumanSamiti", new AnugamanSamitiMemberViewModel());
        public  async Task<IActionResult> DeleteAnugumanSamiti(int?id)
        {
            var data = await _common.DeleteAnugamanSamiti((int)id);
            return RedirectToAction("AnugumanSamitiIndex");
            
        }
        public async Task<IActionResult> GetAnugamanSamitiMember(int? id)
        {
            return View(await _common.GetAllSamitiMember(id));
        }

        #endregion
            #region Chhetra
        public async Task<IActionResult> ChhetraIndex()
        {
            return View(await _common.GetAllChettra());
        }
        public async Task<IActionResult> CreateChhetra(int id = 0)
        {
            TempData["msg"] = "";

            return View(await _common.GetChettraById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateChhetra(ChettraViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateChettra(model))
                {
                    TempData["msg"] = "सफलतापूर्वक सुरक्षित भयो";
                    return RedirectToAction("ChhetraIndex");
                }
            }
            else
            {
                TempData["msg"] = "Failed";
            }

            return View(model);
        }

		public async Task<IActionResult> DeleteChettra(int id)
		{
			TempData["Msg"] = await _common.DeleteChettraById(id) ? "सफलतापूर्वक हटाउनु भयो!!" : "कृपया पुन: प्रयास गर्नुहोस्";
			return RedirectToAction("ChhetraIndex");
		}
		#endregion
		#region Upachhetra
		public async Task<IActionResult> CreateUpaChhetra(int id = 0)
        {
            return View(await _common.GetUpaChetraById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateUpaChhetra(UpaChetraViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateUpaChetra(model))
                {
                    TempData["msg"] = "सफलतापूर्वक सुरक्षित भयो";
                    return RedirectToAction("UpaChhetraIndex");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> UpaChhetraIndex()
        {
            return View(await _common.GetAllUpaChetra());
        }

		public async Task<IActionResult> DeleteUpaChhetra(int id)
		{
			TempData["Msg"] = await _common.DeleteUpaChetraById(id) ? "सफलतापूर्वक हटाउनु भयो!!" : "कृपया पुन: प्रयास गर्नुहोस्";
			return RedirectToAction("UpaChhetraIndex");
		}
		#endregion
		#region UpachhetraDetails
		public async Task<IActionResult> UpaChhetraDetailsIndex()
        {
            return View(await _common.GetAllUpaChetraDetail());
        }
        public async Task<IActionResult> CreateUpaChhetraDetails(int id = 0)
        {
            return View(await _common.GetUpaChetraDetailById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateUpaChhetraDetails(UpaChetraDetailViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateUpaChetraDetail(model))
                {
                    TempData["msg"] = "सफलतापूर्वक सुरक्षित भयो";
                    return RedirectToAction("UpaChhetraDetailsIndex");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteUpaChetraDetailById(int id)
        {
            TempData["Msg"] = await _common.DeleteUpaChetraDetailById(id) ? "सफलतापूर्वक हटाउनु भयो!!" : "कृपया पुन: प्रयास गर्नुहोस्";
            return RedirectToAction("UpaChhetraDetailsIndex");
        }
        #endregion

        #region YojanaSetup
        public async Task<IActionResult> YojanaIndex()
        {
            return View(await _common.GetAllYojanaSetup());
        }
        public async Task<IActionResult> InsertUpdateYojanaSetup(int id = 0)
        {
            TempData["msg"] = "";
            return View(await _common.GetYojanaSetupById(id));
        }
        [HttpPost]
        public async Task<IActionResult> InsertUpdateYojanaSetup(YojanaSetupViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateYojanaSetup(model))
                {
                    TempData["msg"] = "सफलतापूर्वक सुरक्षित भयो";
                    return RedirectToAction("YojanaIndex");
                }
            }
            else
            {
                TempData["msg"] = "Failed";
            }

            return View(model);
        }
        public async Task<IActionResult> DeleteYojanaSetup(int id)
        {
            var aa = await _common.DeleteYojanaSetup(id);
            return RedirectToAction("YojanaIndex");
        }
        public ActionResult UploadExcelRecords()
        {
            YojanaSetupViewModel model = new YojanaSetupViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UploadExcelRecords(IFormFile excel)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                var resultCount = await _common.InsertUpdateExcelYojana(excel);
                if (resultCount > 0)
                {
                    ViewData["dataCount"] = resultCount;
                    return RedirectToAction("YojanaIndex");
                }
                else 
                {
                    TempData["msg"] = "एक्सेल अपलोड हुन सकेन!! पुन प्रयास गर्नुहोस";
				
				}
            }
            return View();
        }
        #endregion
        #region Sarta
        public async Task<IActionResult> SartaIndex()
        {
            return View(await _common.GetAllSarta());
        }
        public async Task<IActionResult> InsertUpdateSarta(int id = 0)
        {
            return View(await _common.GetSartaById(id));
        }
        [HttpPost]
        public async Task<IActionResult> InsertUpdateSarta(SartaSetupViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateSarta(model))
                {
                    TempData["msg"] = "सफलतापूर्वक सुरक्षित भयो";
                    return RedirectToAction("SartaIndex");
                }
            }
            return View(model);
		}
		public async Task<IActionResult> DeleteSartaById(int id = 0)
		{
            var aa = await _common.DeleteSartaById(id);
			return RedirectToAction("SartaIndex");
		}
		#endregion
		#region Karkatti
		public async Task<IActionResult> KarkattiIndex()
        {
            return View(await _common.GetAllKarkatti());
        }
        public async Task<IActionResult> InsertUpdateKarkatti(int id = 0)
        {
            return View(await _common.GetKarkatttiById(id));
        }
        [HttpPost]
        public async Task<IActionResult> InsertUpdateKarkatti(KarKattiViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateKarkatti(model))
                {
                    TempData["msg"] = "कर-कट्टी सफलतापूर्वक स";
                    return RedirectToAction("KarkattiIndex");
                }
            }
            TempData["msg"] = "Failed";
            return View(model);
        }
        #endregion
        #region Docs
        public async Task<IActionResult> DocumentTypeIndex()
        {
            return View(await _common.GetAllDocumentType());
        }
        public async Task<IActionResult> InsertUpdateDocumentType(int id = 0)
        {
            return View(await _common.GetDocumentTypeById(id));
        }
        [HttpPost]
        public async Task<IActionResult> InsertUpdateDocumentType(DocumentTypeViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateDocumentType(model))
                {
                    TempData["msg"] = "कागजात सफलतापूर्वक सुरक्षित भयो";
                    return RedirectToAction("DocumentTypeIndex");
                }
            }
            TempData["msg"] = "Failed";
            return View(model);
        }
        #endregion

        #region ExcelFormat
        public ActionResult Download()
        {
            var fileContents = GenerateExcel();
            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FormattedExcel.xlsx");
        }
        public byte[] GenerateExcel()
        {
            // Set the license context to non-commercial
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Main headers with colspan and rowspan
                worksheet.Cells["A1"].Value = "योजनाको नाम";

                worksheet.Cells["B1"].Value = "वडा नं";

                worksheet.Cells["C1"].Value = "उप-क्षेत्र";

                worksheet.Cells["D1"].Value = "खर्च शीर्षक";

                worksheet.Cells["E1"].Value = "स्रोत";

                worksheet.Cells["F1"].Value = "रकम";

                // Adjust column widths
                worksheet.Cells.AutoFitColumns();

                return package.GetAsByteArray();
            }
        }
		#endregion

		#region Unit
		public async Task<IActionResult> UnitIndex()
		{
			return View(await _common.GetAllUnit());
		}
		public async Task<IActionResult> CreateUnit(int id = 0)
		{
			return View(await _common.GetUnitById(id));
		}
		[HttpPost]
		public async Task<IActionResult> CreateUnit(UnitViewModel model)
		{
			var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
			if (ModelState.IsValid)
			{
				if (await _common.InsertUpdateUnit(model))
				{
					TempData["msg"] = "सफलतापूर्वक सुरक्षित भयो";
					return RedirectToAction("UnitIndex");
				}
			}
			return View(model);
        }
        public async Task<IActionResult> DeleteUnit(int id)
        {
            TempData["Msg"] = await _common.DeleteUnit(id) ? "सफलतापूर्वक हटाउनु भयो!!" : "कृपया पुन: प्रयास गर्नुहोस्";
            return RedirectToAction("UnitIndex");
        }
        #endregion

        #region Bank
        public async Task<IActionResult> BankIndex()
        {
            return View(await _common.GetAllBank());
        }
        public async Task<IActionResult> CreateBank(int id = 0)
        {
            return View(await _common.GetBankById(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateBank(BankViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                if (await _common.InsertUpdateBank(model))
                {
                    TempData["msg"] = "सफलतापूर्वक सुरक्षित भयो";
                    return RedirectToAction("BankIndex");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteBank(int id)
        {
            TempData["Msg"] = await _common.DeleteBank(id) ? "सफलतापूर्वक हटाउनु भयो!!" : "कृपया पुन: प्रयास गर्नुहोस्";
            return RedirectToAction("BankIndex");
        }
        #endregion
    }
}
