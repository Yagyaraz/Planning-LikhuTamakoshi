using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using PlanningCore.Areas.Admin.Models;

namespace PlanningCore.Areas.Admin.Interface
{
    public interface ICommon
    {

        //FiscalYear
        #region FiscalYear
        Task<List<FiscalYearViewModel>> GetAllFiscalYear();
        Task<FiscalYearViewModel> GetFiscalYear(int id);
        Task<bool> InsertUpdateFiscalYear(FiscalYearViewModel model);
        #endregion

        //Pada
        #region Pada
        Task<List<PadaViewModel>> GetAllPada();
        Task<PadaViewModel> GetPadaById(int id);
        Task<bool> InsertUpdatePada(PadaViewModel model);
        Task<bool> DeletePada(int id);
        #endregion

        //Employee
        #region Employee
        Task<List<EmployeeViewModel>> GetAllEmployee();
        Task<EmployeeViewModel> GetEmployeeById(int id);
        Task<bool> InsertUpdateEmployee(EmployeeViewModel model);
        Task<bool> DeleteEmployee(int id);
        #endregion
        #region AnugumanSamiti
        Task<List<AnugamanSamitiSetupViewModel>> GetAnugumanSamiti();
        Task<AnugamanSamitiSetupViewModel> GetAnugumanSamitiById(int ? id);
        Task<bool> InsertUpdateAnugamanSamiti(AnugamanSamitiSetupViewModel model);
        Task<bool> DeleteAnugamanSamiti(int id);
        Task<AnugamanSamitiSetupViewModel> GetAllSamitiMember(int? id);

        #endregion
        //Chhetra
        #region Chhetra
        //Chettra
        Task<List<ChettraViewModel>> GetAllChettra();
        Task<ChettraViewModel> GetChettraById(int id);
        Task<bool> InsertUpdateChettra(ChettraViewModel model);
        Task<bool> DeleteChettraById(int id);

        //UpaChetra
        Task<List<UpaChetraViewModel>> GetAllUpaChetra();
        Task<UpaChetraViewModel> GetUpaChetraById(int id);
        Task<bool> InsertUpdateUpaChetra(UpaChetraViewModel model);
        Task<bool> DeleteUpaChetraById(int id);

        //UpaChetraDetail
        Task<List<UpaChetraDetailViewModel>> GetAllUpaChetraDetail();
        Task<UpaChetraDetailViewModel> GetUpaChetraDetailById(int id);
        Task<bool> InsertUpdateUpaChetraDetail(UpaChetraDetailViewModel model);
        Task<bool> DeleteUpaChetraDetailById(int id);
        #endregion

        //YojanaSetup
        #region yojanasetup
        Task<List<YojanaSetupViewModel>> GetAllYojanaSetup();
        Task<YojanaSetupViewModel> GetYojanaSetupById(int? id);      
        Task<bool> InsertUpdateYojanaSetup(YojanaSetupViewModel model);
        Task<bool> DeleteYojanaSetup(int id);
        Task<int> InsertUpdateExcelYojana(IFormFile excelFile);
     
		#endregion

		#region Sarta
		Task<List<SartaSetupViewModel>> GetAllSarta();
        Task<SartaSetupViewModel> GetSartaById(int id);
        Task<bool> InsertUpdateSarta(SartaSetupViewModel model);
        Task<bool> DeleteSartaById(int id);
        #endregion
        #region karkatti
        Task<List<KarKattiViewModel>> GetAllKarkatti();
        Task<KarKattiViewModel> GetKarkatttiById(int id);
        Task<bool> InsertUpdateKarkatti(KarKattiViewModel model);
        //Task<bool> DeleteSartaById(int id);
        #endregion

        #region Docs
        Task<List<DocumentTypeViewModel>> GetAllDocumentType();
        Task<DocumentTypeViewModel> GetDocumentTypeById(int id);
        Task<bool> InsertUpdateDocumentType(DocumentTypeViewModel model);
        Task<bool> DeleteDocumentTypeById(int id);
        #endregion

        #region Unit
        Task<List<UnitViewModel>> GetAllUnit();
        Task<UnitViewModel> GetUnitById(int id);
        Task<bool> InsertUpdateUnit(UnitViewModel model);
        Task<bool> DeleteUnit(int id);
        #endregion

        #region Bank
        Task<List<BankViewModel>> GetAllBank();
        Task<BankViewModel> GetBankById(int id);
        Task<bool> InsertUpdateBank(BankViewModel model);
        Task<bool> DeleteBank(int id);
        #endregion
    }
}
