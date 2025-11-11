using PlanningCore.Areas.Admin.Models;

namespace PlanningCore.Areas.Admin.Interface
{
    public interface ITravelExpense
    {
        Task<TravelExpenseViewModel> GetTravelExpenseById(int? id);
        Task<List<TravelExpenseViewModel>> GetAllTravelExpenses();       
        Task<bool> CreateTravelExpense(TravelExpenseViewModel model);
       // Task<bool> DeleteUpavoktaSamitiDetailById(int id);
    }
}
