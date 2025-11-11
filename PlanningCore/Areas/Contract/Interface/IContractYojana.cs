using PlanningCore.Areas.Admin.Models;
using PlanningCore.Areas.Contract.Models;

namespace PlanningCore.Areas.Contract.Interface
{
    public interface IContractYojana
    {
        #region yojanasetup
        Task<List<Con_YojanaViewModel>> GetAllContractYojana();
        Task<Con_YojanaViewModel> GetContractYojanaById(int? id);
        Task<bool> InsertUpdateContractYojana(Con_YojanaViewModel model);
        Task<bool> DeleteContractYojana(int id);
      //  Task<int> InsertUpdateExcelYojana(IFormFile excelFile);

        #endregion
    }
}
