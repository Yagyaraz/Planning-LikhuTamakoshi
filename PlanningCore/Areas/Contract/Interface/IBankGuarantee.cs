using PlanningCore.Areas.Contract.Models;

namespace PlanningCore.Areas.Contract.Interface
{
	public interface IBankGuarantee
	{
		Task<List<BankGuaranteeViewModel>> GetBankGuarantee();
		Task<BankGuaranteeViewModel> GetBankGuaranteeById(int id);
		Task<bool> InsertUpdateBankGuarantee(BankGuaranteeViewModel model);
	}
}
