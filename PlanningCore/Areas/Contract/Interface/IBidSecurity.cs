using PlanningCore.Areas.Contract.Models;

namespace PlanningCore.Areas.Contract.Interface
{
	public interface IBidSecurity
	{
		Task<List<BidSecurityViewModel>> GetBidSecurity();
		Task<BidSecurityViewModel> GetBidSecurityById(int id);
		Task<bool> InsertUpdateBidSecurity(BidSecurityViewModel model);
		Task<object> GetYojanaInfo(int id);
		Task<bool> DeleteBidSecurity(int id);
	}
}
