using PlanningCore.Areas.Admin.Models;

namespace PlanningCore.Areas.Admin.Interface
{
    public interface IReport
    {
		Task<List<PrintReportViewModel>> GetAllReportById(int id);

		//Task<PrintReportViewModel> GetAllReports();
		Task<List<PrintReportViewModel>> GetAllReports();

		//Task<List<BhuktaniReportViewModel>> GetAllBhuktaniReport();


		Task<List<RepotSamjhutaViewModel>> Samjhuta(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId);
		Task<List<RepotInCompleteSamjhutaViewModel>> InCompleteSamjhuta(int? fiscalYearId, int? yojanaId, int? wardId);
		Task<List<RepotCompleteSamjhutaViewModel>> CompleteSamjhuta(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId);
        Task<List<RepotBhotikPratiwadanViewModel>> BhotikPratiwadan(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId);
        Task<List<RepotBitiyaPratiwadanViewModel>> BitiyaPratiwadan(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId);
        Task<List<RepotTotalBhukataniViewModel>> TotalBhukatani(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId);
        Task<List<RepotWardWiseViewModel>> WardWise(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId);
        Task<List<RepotWorkWiseViewModel>> WorkWise(int? fiscalYearId, int? yojanaId, int? budgetId, int? wardId);

    }
}
