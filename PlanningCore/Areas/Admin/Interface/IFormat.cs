using PlanningCore.Areas.Admin.Models;

namespace PlanningCore.Areas.Admin.Interface
{
    public interface IFormat
    {
        Task<FormatViewModel> GetAllDataForFormat(int? id);
        Task<bool> InsertUpdateYojanaKarya(YojanaKaryakramChecklistViewModel model);
        Task<YojanaKaryakramChecklistViewModel> GetYojanaKaryakramById(int PlanningSamjhautaId);
    }
}
