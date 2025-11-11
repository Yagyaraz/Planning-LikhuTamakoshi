using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;
using PlanningCore.Areas.Admin.Interface;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;

namespace PlanningCore.Areas.Admin.Repositories
{
    public class ReportRepositories : IReport
    {
        private readonly PlanningContext _context;
        private readonly IUtility _utility = null;
        private readonly string _userId = null;
        public ReportRepositories(PlanningContext context, IHttpContextAccessor httpContextAccessor, IUtility utility)
        {
            _context = context;
            _utility = utility;
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<List<PrintReportViewModel>> GetAllReportById(int id)
        {
            var data = await _context.PrintReport.Where(x => x.PlanningSamjhautaId == id)
                       .Select(x => new PrintReportViewModel()
                       {
                           PrintContent = x.PrintContent,
                           ReportName = x.ReportName,
                           SamjhautaName = x.PlanningSamjhauta.YojanaSetup.YojanaName,
                       }).ToListAsync() ?? new List<PrintReportViewModel>();
            var data1 = await (from pr in _context.PrintReportUpabhokta
                               join samiti in _context.UpabhoktaSamitiDetail on pr.UpabhoktaSamitiDetailId equals samiti.UpabhoktaSamitiDetailId into detail
                               from samiti in detail.DefaultIfEmpty()
                               join samjhauta in _context.PlanningSamjhauta on pr.UpabhoktaSamitiDetailId equals samjhauta.SamitiDetailId into psamjhauta
                               from samjhauta in psamjhauta.DefaultIfEmpty()
                               where samjhauta.PlanningSamjhautaId == id
                               select new PrintReportViewModel()
                               {
                                   Id = pr.Id,
                                   PlanningSamjhautaId = samjhauta.PlanningSamjhautaId,
                                   PrintContent = pr.PrintContent,
                                   ReportName = pr.ReportName
                               }).ToListAsync();
            var docList = await _context.DocumentUploaded.Where(x => x.PlanningSamjhuataId == id)
                               .Select(p => new PrintReportViewModel()
                               {
                                   PlanningSamjhautaId = p.PlanningSamjhuataId,
                                   DocTypeName = _context.DocumentType.Where(z => z.DocumentTypeId == p.DocumentTypeId).Select(z => z.DocumentTypeName).FirstOrDefault(),
                                   DocPath = p.ImagePath,
                               }).ToListAsync();
            var combinedData = data.Concat(data1).ToList() ?? new List<PrintReportViewModel>();
            var combinedData1 = combinedData.Concat(docList).ToList() ?? new List<PrintReportViewModel>();
            return combinedData1;
        }

        public async Task<List<PrintReportViewModel>> GetAllReports()
        {
            // Fetch data from the first query
            var data = await _context.PrintReport
                .Select(x => new PrintReportViewModel
                {
                    Id = x.Id,
                    PlanningSamjhautaId = x.PlanningSamjhautaId,
                    ReportName = x.ReportName,
                    SamjhautaName = x.PlanningSamjhauta.YojanaSetup.YojanaName,
                })
                .ToListAsync();

            // Fetch data from the second query
            var data1 = await (from pr in _context.PrintReportUpabhokta
                               join samiti in _context.UpabhoktaSamitiDetail on pr.UpabhoktaSamitiDetailId equals samiti.UpabhoktaSamitiDetailId into detail
                               from samiti in detail.DefaultIfEmpty()
                               join samjhauta in _context.PlanningSamjhauta on pr.UpabhoktaSamitiDetailId equals samjhauta.SamitiDetailId into psamjhauta
                               from samjhauta in psamjhauta.DefaultIfEmpty()
                               where samjhauta.IsDeleted == false
                               select new PrintReportViewModel
                               {
                                   Id = pr.Id,
                                   PlanningSamjhautaId = samjhauta.PlanningSamjhautaId,
                                   PrintContent = pr.PrintContent,
                                   ReportName = pr.ReportName
                               }).ToListAsync();

            // Combine both datasets and remove duplicates based on PlanningSamjhautaId
            var combinedData = data.Concat(data1)
                .GroupBy(x => x.PlanningSamjhautaId)
                .Select(group => group.First()) // Select the first entry in each group
                .ToList();

            return combinedData;
        }

        #region BhuktaniReport
        public async Task<List<RepotSamjhutaViewModel>> Samjhuta(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId)
        {
            var data = await (from p in _context.PlanningSamjhauta
                              join u in _context.UpabhoktaSamitiDetail on p.SamitiDetailId equals u.UpabhoktaSamitiDetailId
                              join uy in _context.UpabhoktaSamitiDetailYojanas on u.UpabhoktaSamitiDetailId equals uy.UpabhoktaSamitiDetailId
                              join ps in _context.ProjectSourceDetail on p.PlanningSamjhautaId equals ps.PlanningSamjhautaId into projectSource
                              from ps in projectSource.DefaultIfEmpty()
                              join pe in _context.PlanningEntry on p.PlanningSamjhautaId equals pe.PlanningSamjhautaId into planningEntry
                              from pe in planningEntry.DefaultIfEmpty()
                              join ped in _context.ProjectEntryDetail on p.PlanningSamjhautaId equals ped.PlanningSamjhautaId into projectEntry
                              from ped in projectEntry.DefaultIfEmpty()
                              where p.IsDeleted == false 
                              && (fiscalYearId == null || p.FiscalYearId == fiscalYearId)
                              && (yojanaId == null || uy.YojanaId == yojanaId)
                              && (budgetId == null || pe.BudgetSourceId == budgetId)
                              && (wardId == null || uy.YojanaSetup.WardId == wardId)
                              select new RepotSamjhutaViewModel()
                              {
                                  FiscalName = _context.FiscalYear.Where(x => x.Id == p.FiscalYearId).Select(x => x.Name).FirstOrDefault(),
                                  BudgetName = _context.BudgetSource.Where(x => x.BudgetSourceId == pe.BudgetSourceId).Select(x => x.BudgetSourceName).FirstOrDefault(),
                                  YojanaName = string.Join(",<br/> ", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(x => x.YojanaSetup.YojanaName).ToList()),
                                  YojanaAddress = u.Address,
                                  WardName = string.Join(",<br/> ", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(u => _context.Ward.Where(w => w.Id == u.YojanaSetup.WardId).Select(w => w.Name).FirstOrDefault()).ToList()),
                                  SamitiName = u.Name,
                                  StartMiti = ped.Project_Start_Date,
                                  EndMiti = ped.Project_End_Date,
                                  BinyojitRkm = ps.Project_estimated_Amount,
                                  LagatRkm = ps.Total_Amount_Source,
                              }).ToListAsync();
            return data;
        }

        public async Task<List<RepotInCompleteSamjhutaViewModel>> InCompleteSamjhuta(int? fiscalYearId, int? yojanaId, int? wardId)
        {
            var data = await _context.YojanaSetup
                .Where(x => x.IsDeleted == false
                && (wardId == null || x.WardId == wardId)
                && !(from p in _context.PlanningSamjhauta
                    join u in _context.UpabhoktaSamitiDetail on p.SamitiDetailId equals u.UpabhoktaSamitiDetailId
                    join uy in _context.UpabhoktaSamitiDetailYojanas on u.UpabhoktaSamitiDetailId equals uy.UpabhoktaSamitiDetailId
                    where p.IsDeleted == false && uy.YojanaId == x.YojanaSetupId
                    select uy.YojanaId).Any()
                ).Select(x => new RepotInCompleteSamjhutaViewModel()
                {
                    YojanaName = x.YojanaName,
                    BinyojitRkm = x.Amount,
                    WardName = _context.Ward.Where(w => w.Id == x.WardId).Select(w => w.Name).FirstOrDefault(),
                    FiscalName = _context.FiscalYear.Where(f => f.Id == x.FiscalYearId).Select(f => f.Name).FirstOrDefault(),
                }).ToListAsync();
            return data;
        }

        public async Task<List<RepotCompleteSamjhutaViewModel>> CompleteSamjhuta(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId)
        {
            var data = await (from p in _context.PlanningSamjhauta
                              join u in _context.UpabhoktaSamitiDetail on p.SamitiDetailId equals u.UpabhoktaSamitiDetailId
                              join uy in _context.UpabhoktaSamitiDetailYojanas on u.UpabhoktaSamitiDetailId equals uy.UpabhoktaSamitiDetailId
                              join ps in _context.ProjectSourceDetail on p.PlanningSamjhautaId equals ps.PlanningSamjhautaId into projectSource
                              from ps in projectSource.DefaultIfEmpty()
                              join pe in _context.PlanningEntry on p.PlanningSamjhautaId equals pe.PlanningSamjhautaId into planningEntry
                              from pe in planningEntry.DefaultIfEmpty()
                              join ped in _context.ProjectEntryDetail on p.PlanningSamjhautaId equals ped.PlanningSamjhautaId into projectEntry
                              from ped in projectEntry.DefaultIfEmpty()
                              where p.IsDeleted == false && ped.Project_Complete_Date != null
                              && (fiscalYearId == null || p.FiscalYearId == fiscalYearId)
                              && (yojanaId == null || p.YojanaSetup.YojanaSetupId == yojanaId)
                              && (budgetId == null || pe.BudgetSourceId == budgetId)
                              && (wardId == null || uy.YojanaSetup.WardId == wardId)
                              select new RepotCompleteSamjhutaViewModel()
                              {
                                  FiscalName = _context.FiscalYear.Where(x => x.Id == p.FiscalYearId).Select(x => x.Name).FirstOrDefault(),
                                  BudgetName = _context.BudgetSource.Where(x => x.BudgetSourceId == pe.BudgetSourceId).Select(x => x.BudgetSourceName).FirstOrDefault(),
                                  YojanaName = string.Join(",<br/> ", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(x => x.YojanaSetup.YojanaName).ToList()),
                                  YojanaAddress = u.Address,
                                  WardName = string.Join(",<br/> ", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(u => _context.Ward.Where(w => w.Id == u.YojanaSetup.WardId).Select(w => w.Name).FirstOrDefault()).ToList()),
                                  EndMiti = ped.Project_End_Date,
                                  BinyojitRkm = ps.Project_estimated_Amount,
                              }).ToListAsync();
            return data;
        }
        
        public async Task<List<RepotBhotikPratiwadanViewModel>> BhotikPratiwadan(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId)
        {
            var data = await (from p in _context.PlanningSamjhauta
                              join u in _context.UpabhoktaSamitiDetail on p.SamitiDetailId equals u.UpabhoktaSamitiDetailId
                              join uy in _context.UpabhoktaSamitiDetailYojanas on u.UpabhoktaSamitiDetailId equals uy.UpabhoktaSamitiDetailId
                              join ps in _context.ProjectSourceDetail on p.PlanningSamjhautaId equals ps.PlanningSamjhautaId into projectSource
                              from ps in projectSource.DefaultIfEmpty()
                              join pe in _context.PlanningEntry on p.PlanningSamjhautaId equals pe.PlanningSamjhautaId into planningEntry
                              from pe in planningEntry.DefaultIfEmpty()
                              join ped in _context.ProjectEntryDetail on p.PlanningSamjhautaId equals ped.PlanningSamjhautaId into projectEntry
                              from ped in projectEntry.DefaultIfEmpty()
                              where p.IsDeleted == false && p.UpabhoktaSamitiDetail != null
                              && (fiscalYearId == null || p.FiscalYearId == fiscalYearId)
                              && (yojanaId == null || p.YojanaSetup.YojanaSetupId == yojanaId)
                              && (budgetId == null || pe.BudgetSourceId == budgetId)
                              && (wardId == null || uy.YojanaSetup.WardId == wardId)
                              select new RepotBhotikPratiwadanViewModel()
                              {
                                  FiscalName = _context.FiscalYear.Where(x => x.Id == p.FiscalYearId).Select(x => x.Name).FirstOrDefault(),
                                  BudgetName = _context.BudgetSource.Where(x => x.BudgetSourceId == pe.BudgetSourceId).Select(x => x.BudgetSourceName).FirstOrDefault(),
                                  YojanaName = string.Join(",<br/> ", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(x => x.YojanaSetup.YojanaName).ToList()),
                                  YojanaAddress = u.Address,
                                  WardName = string.Join(",<br/> ", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(u => _context.Ward.Where(w => w.Id == u.YojanaSetup.WardId).Select(w => w.Name).FirstOrDefault()).ToList()),
                                  SamitiName = u.Name,
                                  SamjhutaMiti = ped.Project_Start_Date,
                                  EndMiti = ped.Project_End_Date,
                                  BinyojitRkm = ps.Project_estimated_Amount,
                                  LagatRkm = ps.Total_Amount_Source,
                              }).ToListAsync();
            return data;
        }

        public async Task<List<RepotBitiyaPratiwadanViewModel>> BitiyaPratiwadan(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId)
        {
            var data = await (from p in _context.PlanningSamjhauta
                              join u in _context.UpabhoktaSamitiDetail on p.SamitiDetailId equals u.UpabhoktaSamitiDetailId
                              join uy in _context.UpabhoktaSamitiDetailYojanas on u.UpabhoktaSamitiDetailId equals uy.UpabhoktaSamitiDetailId
                              join ps in _context.ProjectSourceDetail on p.PlanningSamjhautaId equals ps.PlanningSamjhautaId into projectSource
                              from ps in projectSource.DefaultIfEmpty()
                              join pe in _context.PlanningEntry on p.PlanningSamjhautaId equals pe.PlanningSamjhautaId into planningEntry
                              from pe in planningEntry.DefaultIfEmpty()
                              join ped in _context.ProjectEntryDetail on p.PlanningSamjhautaId equals ped.PlanningSamjhautaId into projectEntry
                              from ped in projectEntry.DefaultIfEmpty()
                              where p.IsDeleted == false && p.UpabhoktaSamitiDetail != null
                              && (fiscalYearId == null || p.FiscalYearId == fiscalYearId)
                              && (yojanaId == null || p.YojanaSetup.YojanaSetupId == yojanaId)
                              && (budgetId == null || pe.BudgetSourceId == budgetId)
                              && (wardId == null || uy.YojanaSetup.WardId == wardId)
                              select new RepotBitiyaPratiwadanViewModel()
                              {
                                  FiscalName = _context.FiscalYear.Where(x => x.Id == p.FiscalYearId).Select(x => x.Name).FirstOrDefault(),
                                  BudgetName = _context.BudgetSource.Where(x => x.BudgetSourceId == pe.BudgetSourceId).Select(x => x.BudgetSourceName).FirstOrDefault(),
                                  YojanaName = string.Join(",<br/> ", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(x => x.YojanaSetup.YojanaName).ToList()),
                                  YojanaAddress = u.Address,
                                  WardName = string.Join(",<br/> ", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(u => _context.Ward.Where(w => w.Id == u.YojanaSetup.WardId).Select(w => w.Name).FirstOrDefault()).ToList()),
                                  LagatRkm = ps.Total_Amount_Source,
                              }).ToListAsync();
            return data;
        }

        public async Task<List<RepotTotalBhukataniViewModel>> TotalBhukatani(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId)
        {
            var data = await (from p in _context.PlanningSamjhauta
                              join u in _context.UpabhoktaSamitiDetail on p.SamitiDetailId equals u.UpabhoktaSamitiDetailId
                              join uy in _context.UpabhoktaSamitiDetailYojanas on u.UpabhoktaSamitiDetailId equals uy.UpabhoktaSamitiDetailId
                              join ps in _context.ProjectSourceDetail on p.PlanningSamjhautaId equals ps.PlanningSamjhautaId into projectSource
                              from ps in projectSource.DefaultIfEmpty()
                              join pe in _context.PlanningEntry on p.PlanningSamjhautaId equals pe.PlanningSamjhautaId into planningEntry
                              from pe in planningEntry.DefaultIfEmpty()
                              join ped in _context.ProjectEntryDetail on p.PlanningSamjhautaId equals ped.PlanningSamjhautaId into projectEntry
                              from ped in projectEntry.DefaultIfEmpty()
                              where p.IsDeleted == false && p.UpabhoktaSamitiDetail != null
                              && (fiscalYearId == null || p.FiscalYearId == fiscalYearId)
                              && (yojanaId == null || p.YojanaSetup.YojanaSetupId == yojanaId)
                              && (budgetId == null || pe.BudgetSourceId == budgetId)
                              && (wardId == null || uy.YojanaSetup.WardId == wardId)
                              select new RepotTotalBhukataniViewModel()
                              {
                                  FiscalName = _context.FiscalYear.Where(x => x.Id == p.FiscalYearId).Select(x => x.Name).FirstOrDefault(),
                                  BudgetName = _context.BudgetSource.Where(x => x.BudgetSourceId == pe.BudgetSourceId).Select(x => x.BudgetSourceName).FirstOrDefault(),
                                  YojanaName = string.Join(",<br/> ", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(x => x.YojanaSetup.YojanaName).ToList()),
                                  WardName = string.Join(",<br/> ", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(u => _context.Ward.Where(w => w.Id == u.YojanaSetup.WardId).Select(w => w.Name).FirstOrDefault()).ToList()),
                                  LagatRkm = ps.Total_Amount_Source,
                              }).ToListAsync();
            return data;
        }

        public async Task<List<RepotWardWiseViewModel>> WardWise(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId)
        {
            var data = await (from p in _context.PlanningSamjhauta
                              join u in _context.UpabhoktaSamitiDetail on p.SamitiDetailId equals u.UpabhoktaSamitiDetailId
                              join uy in _context.UpabhoktaSamitiDetailYojanas on u.UpabhoktaSamitiDetailId equals uy.UpabhoktaSamitiDetailId
                              join ps in _context.ProjectSourceDetail on p.PlanningSamjhautaId equals ps.PlanningSamjhautaId into projectSource
                              from ps in projectSource.DefaultIfEmpty()
                              join pe in _context.PlanningEntry on p.PlanningSamjhautaId equals pe.PlanningSamjhautaId into planningEntry
                              from pe in planningEntry.DefaultIfEmpty()
                              join ped in _context.ProjectEntryDetail on p.PlanningSamjhautaId equals ped.PlanningSamjhautaId into projectEntry
                              from ped in projectEntry.DefaultIfEmpty()
                              where p.IsDeleted == false && p.UpabhoktaSamitiDetail != null
                              && (fiscalYearId == null || p.FiscalYearId == fiscalYearId)
                              && (yojanaId == null || p.YojanaSetup.YojanaSetupId == yojanaId)
                              && (budgetId == null || pe.BudgetSourceId == budgetId)
                              && (wardId == null || uy.YojanaSetup.WardId == wardId)
                              select new RepotWardWiseViewModel()
                              {
                                  FiscalName = _context.FiscalYear.Where(x => x.Id == p.FiscalYearId).Select(x => x.Name).FirstOrDefault(),
                                  BudgetName = _context.BudgetSource.Where(x => x.BudgetSourceId == pe.BudgetSourceId).Select(x => x.BudgetSourceName).FirstOrDefault(),
                                  YojanaName = string.Join(",<br/> ", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(x => x.YojanaSetup.YojanaName).ToList()),
                                  WardName = string.Join(",<br/> ", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(u => _context.Ward.Where(w => w.Id == u.YojanaSetup.WardId).Select(w => w.Name).FirstOrDefault()).ToList()),
                                  LagatRkm = ps.Total_Amount_Source,
                                  YojanaAddress = u.Address,
                              }).ToListAsync();
            return data;
        }

        public async Task<List<RepotWorkWiseViewModel>> WorkWise(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId)
        {
            var data = await (from p in _context.PlanningSamjhauta
                              join u in _context.UpabhoktaSamitiDetail on p.SamitiDetailId equals u.UpabhoktaSamitiDetailId
                              join uy in _context.UpabhoktaSamitiDetailYojanas on u.UpabhoktaSamitiDetailId equals uy.UpabhoktaSamitiDetailId
                              join ps in _context.ProjectSourceDetail on p.PlanningSamjhautaId equals ps.PlanningSamjhautaId into projectSource
                              from ps in projectSource.DefaultIfEmpty()
                              join pe in _context.PlanningEntry on p.PlanningSamjhautaId equals pe.PlanningSamjhautaId into planningEntry
                              from pe in planningEntry.DefaultIfEmpty()
                              join ped in _context.ProjectEntryDetail on p.PlanningSamjhautaId equals ped.PlanningSamjhautaId into projectEntry
                              from ped in projectEntry.DefaultIfEmpty()
                              join pd in _context.PlanningPravidikDetails on p.PlanningSamjhautaId equals pd.PlanningSamjhautaId into pravidikDetail
                              from pd in pravidikDetail.DefaultIfEmpty()
                              where p.IsDeleted == false && p.UpabhoktaSamitiDetail != null
                              && (fiscalYearId == null || p.FiscalYearId == fiscalYearId)
                              && (yojanaId == null || p.YojanaSetup.YojanaSetupId == yojanaId)
                              && (budgetId == null || pe.BudgetSourceId == budgetId)
                              && (wardId == null || uy.YojanaSetup.WardId == wardId)
                              select new RepotWorkWiseViewModel()
                              {
                                  FiscalName = _context.FiscalYear.Where(x => x.Id == p.FiscalYearId).Select(x => x.Name).FirstOrDefault(),
                                  BudgetName = _context.BudgetSource.Where(x => x.BudgetSourceId == pe.BudgetSourceId).Select(x => x.BudgetSourceName).FirstOrDefault(),
                                  YojanaName = string.Join(",<br/> ", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(x => x.YojanaSetup.YojanaName).ToList()),
                                  WardName = string.Join(",<br/> ", _context.UpabhoktaSamitiDetailYojanas.Where(u => u.UpabhoktaSamitiDetailId == p.SamitiDetailId).Select(u => _context.Ward.Where(w => w.Id == u.YojanaSetup.WardId).Select(w => w.Name).FirstOrDefault()).ToList()),
                                  Unit = pd.Unit.Name,
                                  SamitiName = u.Name,
                                  EndMiti = ped.Project_Complete_Date,
                              }).ToListAsync();
            return data;
        }
		#endregion


	}
}
