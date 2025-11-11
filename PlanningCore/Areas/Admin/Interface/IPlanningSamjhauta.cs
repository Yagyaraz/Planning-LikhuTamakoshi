using PlanningCore.Areas.Admin.Models;

namespace PlanningCore.Areas.Admin.Interface
{
	public interface IPlanningSamjhauta
	{
		Task<List<PlanningSamjhautaViewModel>> GetPlanningSamjhautaList();
		Task<List<PlanningSamjhautaViewModel>> GetPlanningSamjhautaAllList(int? wardId, string name);
		Task<int> InsertPlanningSamjhauta(PlanningSamjhautaViewModel model);
		Task<PlanningSamjhautaViewModel> GetPlanningSamjhautaById(int id);		
		Task<bool> UpdateProjectDate(int? id, string date, string detail, string ekai, string pariman, string kaifiyat);
		Task<bool> DeletePlanningSamjhautaById(int id);
		Task<List<PlanningDocumentUploadedViewModel>> GetPlanningDocumentById(int id);
		Task<int> InsertUpdatePlanningDocument(PlanningDocumentUploadedViewModel model);
		Task<bool> DeletePlanningDocumentId(int id);
		Task <PlanningDocumentUploadedViewModel> GetDocsfromPlanningSamjhauta(int? id);
		Task<List<PlanningTaxViewModel>> GetPlanningKarKatti();
		Task<PlanningSamjhautaViewModel> GetNonSamjhauta();
		Task<bool> UpdateBhautikPragati(int? praId, decimal BittiyaPragati, decimal BhautikPragati, string Remarks);

	}

}
