using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlanningCore.Areas.Admin.Models;
using PlanningCore.Areas.Contract.Models;
using PlanningCore.Models;
using static PlanningCore.Models.DashboardViewModel;

namespace PlanningCore.Utilities
{
    public interface IUtility
    {
        Task<SelectList> GetStateSelectListItems();
        Task<SelectList> GetDistrictSelectListItems();
        Task<SelectList> GetDistrictByStateId(int? stateId);
        Task<SelectList> GetPalikaSelectListItems();
        Task<SelectList> GetPalikaByDistrictId(int? distId);
        Task<List<SelectListItem>> GetSelectListWard();
        Task<int> GetCurrentFiscalYear();
        Task<string> GetCurrentFiscalYearName();

        Task<int?> GetCurrentPalikaId();
        Task<int> GetCurrentDepartmentId();
        Task<SelectList> GetWardList();
        Task<SelectList> GetMyadThapTypeList();
        Task<SelectList> GetFukuwaTypeList();
        Task<SelectList> GetPaymentTypeList();
        Task<SelectList> GetPadaList();
        Task<SelectList> GetSchoolPostList();
        Task<SelectList> GetSamitiPadaList();
        Task<SelectList> GetYojanaAccToSamjhautaList();
        //Task<SelectList> GetNewTolBikashPostList();
        Task<SelectList> GetAnugamanSamitiPostList();
        Task<SelectList> GetDepartmentListItems(int? wardId);
        Task<SelectList> GetDepartmentList();
        Task<SelectList> GetSubDepartmentListItems(int? depId);
        Task<SelectList> GetSubDepartmentList();
        Task<FileUploadModel> UploadImgReturnPathAndName(string folderName, IFormFile img);
        Task<string> UploadImgAsync(string folderName, IFormFile img);
        string ConvertEnglishToNepali(object text);
        Task<SelectList> GetSelectListRoles();
        Task<List<UserViewModel>> GetUserList();
        Task<bool> ResetUserPassword(string id);
        Task<bool> AssignOrChangeRole(string id, string role);
        Task<int> UserActivateOrDeactivate(string id);
        Task<SelectList> GetRolesSelectListItems();
        Task<SelectList> GetChhetraList();
        Task<SelectList> GetUpaChhetraList();
        Task<SelectList> GetUpaChhetraDetailsList(int? id = 0);
        Task<SelectList> GetyojanaList();
        Task<SelectList> GeUnitList();
        Task<SelectList> GetThekkaTypeList();
        Task<SelectList> GetThekkaYojanaList();
        Task<SelectList> GetVariationTypeList();
        Task<SelectList> GetBankGuranteeTypeList();
        Task<SelectList> GetUpaBhoktaSamitiList();
        Task<SelectList> GetNewTolBikashList();
        Task<SelectList> GetTolBikashList();
        Task<SelectList> GetFiscalYearList();
        Task<SelectList> GetBudgetSourceList();
        Task<SelectList> GetPlanningTypeList();
        Task<SelectList> GetWorkType();
        Task<SelectList> GetContractType();
        Task<SelectList> GetContractReportType();
        Task<SelectList> GetSartaList();
        Task<SelectList> GetUpabhoktaMemberName();
        Task<SelectList> GetUpabhoktaMemberPost();
        Task<SelectList> GetPalikaType(int id);
        //Task <SelectList> GetPalikaTypeName(int id);
        //Task<SelectList> GetEmpByPadaId(int? padId); //paxi garamla
        Task<SelectList> GetEmpList();
        #region MainSetting & Ward
        Task<List<MainSettingViewModel>> GetAllMainSettings();
        Task<MainSettingViewModel> GetMainSettingById(int id);
        Task<bool> InsertUpdateMainSetting(MainSettingViewModel model);
        Task<MainSettingViewModel> GetHeaderData();
        #endregion
        #region Ward
        Task<List<WardViewModel>> GetAllWards();
        Task<WardViewModel> GetWardById(int id);
        Task<bool> InsertUpdateWard(WardViewModel model);
        Task<bool> DeleteWard(int id);
        #endregion
        string ConvertNumberToNepaliText(string paramNumber);
        //EmpTest
        Task<SelectList> GetEmpNameByPostId(int? PostId);

        Task<SelectList> GetEmpNameBySmitiPostId(int? PostId);
        Task<SelectList> GetBhuktaniType();
        Task<SelectList> GetSamjhautaList();

        bool CheckNumber(string Number);
        string ConvertNepaliToEnglish(string NepaliNumericValue);
        Task<FileUploadModel> UploadImgReturnPathAndName(string folderName, IFormFile img, string name);
        Task<SelectList> GetDocumentType();
        Task<string> GetEmpNameById(int? id);
        Task<string> GetPostById(int? id);
        Task RemoveFileFormServer(string path);
        Task<bool> PrintReport(int PlanningSamjhautaId, string ReportName, string PrintContent);
        Task<bool> PrintReportUpabhota(int UpabhoktaSamitiDetailId, string ReportName, string PrintContent);
        Task<string> GetPrintReportContent(int PlanningSamjhautaId, string ReportName);
        Task<string> GetPrintReportContentUpabhokta(int UpabhoktaSamitiDetailId, string ReportName);
        Task<bool> ContractPrintReportDetail(Con_PrintReportDetailViewModel model);
        Task<SelectList> GetEmpNameBySamitiPostId(int? PostId, int? samitiId);
        Task<SelectList> GetEmpNameBySchoolPostId(int? PostId, int? samitiId);
        Task<SelectList> GetEmpNameByTolBikashPostId(int? PostId, int? samitiId);
        Task<UpavoktaSamitiMemberDetailViewModel> Getdata(int? samitiId);
        Task<SelectList> GetBankList();
        Task<SelectList> GetConsultantList();
        Task<SelectList> GetEmployeer();
        Task<SelectList> GetSupplier();
        Task<List<PieChartViewModel>> GetSamjhautaPieChart();
        Task<List<PieChartViewModel>> GetThekkaPieChart();
        Task<string> GetThekkaReportCodeById(int reportId);
        //Added Later

        Task<SelectList> GetYojanaNotContractList(int id);
        Task<SelectList> GetUpabhhoktaSamitiList(int id);
        Task<SelectList> GetNonContractSamiti(int id);
        Task<GetInsertedFieldInSamjhautaViewModel> GetYojanaNameBySamitiId(int id);
        Task<List<DisplayUpbhokataDetailinPlaningSamjhuta>> GetYojanaDetailsByTolBikasSansthaId(int id);
        Task<List<DisplayUpbhokataDetailinPlaningSamjhuta>> GetYojanaDetailsByNewTolBikasSansthaId(int id);
        Task<KarKattiViewModel> GetKarkatti(int? id);
        Task<PadaViewModel> GetPadaDetail(int? id);
        Task<int?> GetWardNoForLogin_Role_User();
        Task<SelectList> GetContractKarkatti();
        Task<SelectList> GetUpachhetraByCheetraId(int? Id);
        Task<bool> CheckCitizenship(string CitizenshipNumber);
        //Task<GetDataFromSamitiPostViewModel> GetDataFromSamitiPost(int id);
        Task<SelectList> GetUpabhoktaSamitiDocsType();
        Task<List<DisplayUpbhokataDetailinPlaningSamjhuta>> GetYojanaDetailsByUpbhokataId(int id);
        Task<bool> CheckPrintContentAvilable(int PlanningSamjhautaId, string ReportName);
        Task<bool> CheckPrintContentAvilableUpaBhokta(int UpabhoktaSamitiDetailId, string ReportName);
    }
}

