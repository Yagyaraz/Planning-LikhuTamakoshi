
using PlanningCore.Areas.Planning.Models.PlanningAnusuchiViewModel;

namespace PlanningCore.Areas.Admin.Interface
{
    public interface IAnusuchi
	{
	
			//PlanningAnusuchi1
			//Task<List<PlanningAnusuchi1ViewModel>> GetAnushuchi1List(int? id);
			//Task<bool> InsertUpdateAnusuchi1(List<PlanningAnusuchi1ViewModel> model);
			//Task<List<PlanningAnusuchi1ViewModel>> GetAnusuchiPlanningSamjhautaListId(int? id);


			//PlanningAnusuchi3
			Task<PlanningAnusuchi3ViewModel> GetAnushuchi3List(int? id);
			Task<bool> InsertUpdateAnusuchi3(PlanningAnusuchi3ViewModel model);

			//PlanningAnusuchi4
			//Task<Anusuchi4ViewModel> GetAnushuchi4List(int? id);
			//Task<bool> InsertUpdateAnusuchi4(Anusuchi4ViewModel model);

			////Anusuchi5
			//Task<List<Anusuchi5ViewModel>> GetAnushuchi5List(int? id);
			//Task<bool> InsertUpdateAnusuchi5(Anusuchi5ViewModel model);

			////Anusuchi6
			//Task<List<Anusuchi6ViewModel>> GetAnushuchi6List(int? id);
			//Task<bool> InsertUpdateAnusuchi6(Anusuchi6ViewModel model);

			////Anusuchi7 
			//Task<List<Anusuchi7ViewModel>> GetAnushuchi7List(int? id);
			//Task<bool> InsertUpdateAnusuchi7(Anusuchi7ViewModel model);
			////Anusuchi10
			//Task<List<Anusuchi10ViewModel>> GetAnusuchi10List(int? id);
			//Task<bool> InsertUpdateAnusuchi10(Anusuchi10ViewModel model);

		
		}
	}

