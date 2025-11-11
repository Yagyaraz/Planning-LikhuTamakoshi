using PlanningCore.Areas.Admin.Models;

namespace PlanningCore.Areas.Admin.Interface
{
	public interface IBudget
	{
	
			//BudgetSource
			Task<List<BudgetSourceViewModel>> GetAllBudgetSource();
			Task<BudgetSourceViewModel> GetBudgetSourceById(int id);
			Task<bool> InsertUpdateBudgetSource(BudgetSourceViewModel model);
			Task<bool> DeleteBudgetSourceById(int id);

			//BudgetType

			Task<List<BudgetTypeViewModel>> GetAllBudgetType();
			Task<BudgetTypeViewModel> GetBudgetTypeById(int id);
			Task<bool> InsertUpdateBudgetType(BudgetTypeViewModel model);
			Task<bool> DeleteBudgetTypeById(int id);

			//BudgetSubType
			Task<List<BudgetSubTypeViewModel>> GetAllBudgetSubType();
			Task<BudgetSubTypeViewModel> GetBudgetSubTypeById(int id);
			Task<bool> InsertUpdateBudgetSubType(BudgetSubTypeViewModel model);
			Task<bool> DeleteBudgetSubTypeById(int id);
		}
	}

