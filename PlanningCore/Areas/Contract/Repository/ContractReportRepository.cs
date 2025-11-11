using Microsoft.EntityFrameworkCore;
using PlanningCore.Areas.Contract.Interface;
using PlanningCore.Areas.Contract.Models;
using PlanningCore.Data;
using PlanningCore.Utilities;
using System.Security.Claims;

namespace PlanningCore.Areas.Contract.Repository
{
    public class ContractReportRepository : IContractReport
    {
        private readonly PlanningContext context;
        private readonly ILogger<BankGuaranteeRepository> _logger;
        private readonly string _userId = null;
        private readonly IUtility _utility;
        public ContractReportRepository(PlanningContext _context, IHttpContextAccessor httpContextAccessor, ILogger<BankGuaranteeRepository> logger, IUtility utility)
        {
            context = _context;
            _logger = logger;
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            utility = _utility;
        }
        public async Task<List<Con_PrintReportDetailViewModel>> GetAllReportById(int id)
        {
            return await context.Con_PrintReportDetail.Where(x => x.YojanaId == id)
                 .Select(x => new Con_PrintReportDetailViewModel()
                 {
                     PrintContent = x.PrintContent,
                     ReportName = x.Con_ReportType.ReportName_Nep,
                     YojanaName = x.YojanaSetup.YojanaName,
                 }).ToListAsync() ?? new List<Con_PrintReportDetailViewModel>();
        }

        public async Task<List<Con_PrintReportDetailViewModel>> GetAllReports()
        {
            var data = await context.Con_PrintReportDetail
                 .Select(x => new Con_PrintReportDetailViewModel
                 {
                     Id = x.Id,
                     YojanaId = x.YojanaId,
                     PrintContent = x.PrintContent,
                     ReportName = x.Con_ReportType.ReportName_Nep,
                     YojanaName = x.YojanaSetup.YojanaName,
                 })
                 .ToListAsync();
            var distinctData = data.DistinctBy(x => x.YojanaId).ToList();

            return distinctData;
        }
    }
}
