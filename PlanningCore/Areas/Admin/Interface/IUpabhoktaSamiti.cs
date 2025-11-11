using Microsoft.AspNetCore.Mvc;
using PlanningCore.Areas.Admin.Models;

namespace PlanningCore.Areas.Admin.Interface
{
    public interface IUpabhoktaSamiti
    {
        //upabhokta
        Task<UpavoktaSamitiDetailViewModel> GetUpavoktaSamitiDetailById(int? id);
        Task<List<UpavoktaSamitiDetailViewModel>> GetAllUpavoktaSamitiDetail();
        Task<bool> CreateUpabhoktaSamiti(UpavoktaSamitiDetailViewModel model);
        Task<bool> DeleteUpavoktaSamitiDetailById(int id);

        //Tolbikash
        Task<List<TolBikashSansthaViewModel>> GetTolBikashSansthaList();
        Task<TolBikashSansthaViewModel> GetTolBikashSansthaById(int? id);
        Task<bool> CreateTolbikashSamiti(TolBikashSansthaViewModel model);
        Task<bool> DeleteTolBikashSansthaById(int id);
        Task<bool> Updatebiupurji(int? id, decimal Amount);

        Task<List<UpbhoktaSamitiMembersModel>> CheckSamitMember(string ctzno, int samitiId);

        Task<UpabhoktaSamitiDetailDocsViewModel> GetDocsListByUpbhokataId(int id);
        Task<bool> InsertUpdateDocsUpload(UpabhoktaSamitiDetailDocsViewModel model);

        //NewToleBikash
        Task<List<NewToleBikashViewModel>> GetAllNewToleBikash();
        Task<NewToleBikashViewModel> GetNewToleBikashById(int id = 0);
        Task<bool> CreateUpabhoktaSamiti(NewToleBikashViewModel model);
        Task<bool> DeleteNewToleBikashById(int id);
    }
}
 