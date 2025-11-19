using PlanningCore.Areas.Admin.Models;

namespace PlanningCore.Areas.Admin.Interface
{
    public interface IAnugamanPartibedan
    {
        Task<bool> InsertAnugamanParitbedan(AnugamanPartibedanViewModel model);
        Task<AnugamanPartibedanViewModel> GetAnugamanPartiBedan(int planningSamjhuataId);
    }
}
