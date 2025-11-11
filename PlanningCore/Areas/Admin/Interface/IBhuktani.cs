using Microsoft.EntityFrameworkCore;
using PlanningCore.Areas.Admin.Models;

namespace PlanningCore.Areas.Admin.Interface
{
	public interface IBhuktani
	{
		Task<PlanningBhuktaniViewModel> GetPlanningBhuktaniListByPlanningSamjhautaId(int id);
		Task<List<PlanningBhuktaniViewModel>> GetBhuktaniListFormPlanningSamjhauta(int? id);
		
		Task<PlanningBhuktaniViewModel> GetPlanningBhuktaniByBhuktaniId(int? pid,int?id);
		Task<int> InsertUpdatePlanningBhuktani(PlanningBhuktaniViewModel model);
		Task<PlanningBhuktaniViewModel> GetBhuktaniFromPlanningSamjhauta(int id);
		Task<bool>DeleteBhuktaniById(int id);
		List<PlanningBhuktaniViewModel> GetPlanningKarKatti();
		
	}
}

