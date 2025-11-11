using PlanningCore.Areas.Admin.Models;
using PlanningCore.Areas.Contract.Models;

namespace PlanningCore.Areas.Contract.Interface
{
	public interface IContractSamjhauta
	{
		Task<List<ContractSamjhautaViewModel>> GetAllContract();
		Task<ContractSamjhautaViewModel> GetContractById(int id);
		Task<bool> InsertContractSamjhauta(ContractSamjhautaViewModel model);

		#region Insurance
		Task<List<InsuranceViewModel>> GetAllInsurance();
		Task<InsuranceViewModel> GetInsuranceById(int id);
		Task<List<InsuranceViewModel>> GetAllInsuranceByContractId(int id);
		Task<bool> CreateInsurance(InsuranceViewModel model);
		Task<bool> DeleteInsurance(int id);
        #endregion

        #region Variation
        Task<List<VariationViewModel>> GetAllVariation();
		Task<List<VariationViewModel>> GetAllVariationByContractId(int id);
		Task<VariationViewModel> GetVariationById(int id);
		Task<bool> CreateVariation(VariationViewModel model);
		Task<bool> DeleteVariation(int id);
		#endregion

		#region Report
		Task<ContractSamjhautaViewModel> GetContractReportDataByYojanaId(int id);
		Task<ContractSamjhautaViewModel>CheckAndGetContractReportData(int yojanaId, int reportTypeId);
        #endregion

        #region Bhuktani
        Task<Con_BhuktaniViewModel> GetBhuktaniListByContractSamjhautaId(int id);
        Task<List<Con_BhuktaniViewModel>> GetConBhuktaniListFormContractSamjhauta(int? id);

        Task<Con_BhuktaniViewModel> GetContractBhuktaniByBhuktaniId(int? id);
        Task<int> InsertUpdateContractBhuktani(Con_BhuktaniViewModel model);
        //Task<Con_BhuktaniViewModel> GetBhuktaniFromContractSamjhauta(int id);
        #endregion
        #region karkatti
        Task<List<Con_KarKattiViewModel>> GetAllThekkaKarkatti();
        Task<Con_KarKattiViewModel> GetContractKarkatttiById(int id);
        Task<bool> CreateContractKarkatti(Con_KarKattiViewModel model);
		Task<bool> DeleteContractKarkatti(int id);
        #endregion
    }
}
