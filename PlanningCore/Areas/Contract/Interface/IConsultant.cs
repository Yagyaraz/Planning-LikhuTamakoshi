using PlanningCore.Areas.Contract.Models;

namespace PlanningCore.Areas.Contract.Interface
{
	public interface IConsultant
	{
		Task<List<ConsultantViewModel>> GetAllConsultant();
		Task<ConsultantViewModel> GetConsultantById(int id);
		Task<bool> InsertConsultant(ConsultantViewModel model);
		Task<bool> DeleteConsultant(int id);
	}
}
