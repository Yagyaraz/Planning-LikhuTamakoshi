using PlanningCore.Models;

namespace PlanningCore.Dashboard
{
	public interface IDashboard
	{
		Task<DashboardViewModel> GetDashAllDataForDashboard();
		//Task<DashboardViewModel> GetAllSamjhautaList();
	}
}
