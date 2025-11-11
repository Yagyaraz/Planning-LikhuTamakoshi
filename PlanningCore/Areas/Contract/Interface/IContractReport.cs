using PlanningCore.Areas.Contract.Models;

namespace PlanningCore.Areas.Contract.Interface
{
    public interface IContractReport
    {
        Task<List<Con_PrintReportDetailViewModel>> GetAllReportById(int id);
        //Task<PrintReportViewModel> GetAllReports();
        Task<List<Con_PrintReportDetailViewModel>> GetAllReports();

    }
}
