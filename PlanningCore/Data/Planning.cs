using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace PlanningCore.Data
{
    public class BudgetSource
    {
        [Key]
        public int BudgetSourceId { get; set; }
        public string BudgetSourceName { get; set; } 

        public Nullable<bool> IsDeleted { get; set; }
        public int? FiscalYearId { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public Nullable<decimal> Amount { get; set; }

        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }

        [ForeignKey(nameof(FiscalYearId))]
        public FiscalYear FiscalYear { get; set; }
    }
    public class BudgetType
    {
        [Key]
        public int BudgetTypeId { get; set; }
        public string BudgetTypeName { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }
    public class PlanningType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
       
    }
    public class BudgetSubType
    {
        [Key]
        public int Id { get; set; }
        public Nullable<int> BudgetTypeId { get; set; }
        public string BudgetSubTypeName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public Nullable<decimal> Amount { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> FiscalYearId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        [ForeignKey("BudgetTypeId")]
        public virtual BudgetType BudgetType { get; set; }
    }
    public class WorkArea
    {
        [Key]
        public int WorkAreaId { get; set; }
        public string WorkAreaName { get; set; }
        public Nullable<bool> Status { get; set; }
    }
    public class WorkType
    {
        [Key]
        public int WorkTypeId { get; set; }
        public string WorkTypeName { get; set; }
        public Nullable<bool> Status { get; set; }

    }
    public class DocumentType
    {
        [Key]
        public int DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
    public class PlanningDocuments
    {
        [Key]
        public int DocumentId { get; set; }
        public Nullable<int> DocumentTypeId { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        public string ImagePath { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        [ForeignKey("DocumentTypeId")]
        public virtual DocumentType DocumentType { get; set; }
    }
    public class KarKatti
    {
        [Key]
        public int KarKattiId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Contigency { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MarmatSambhar { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal SamajikSurekchya { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BahalKar { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AgrimShulka { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Parishramik { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Royality { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Dhuwani { get; set; }
        public Nullable<bool> Status { get; set; }
        public int? FiscalYearId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        [ForeignKey(nameof(FiscalYearId))]
        public FiscalYear FiscalYear { get; set; }
    }
    public class Chettra
    {
        [Key]
        public int ChettraId { get; set; }
        public string ChettraName { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
    public class UpaChetra
    {
        [Key]
        public int UpaChettraId { get; set; }
        public int? ChettraId { get; set; }
        public string UpaChettra { get; set; }
        public string KharchaSirshark { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        [ForeignKey("ChettraId")]
        public virtual Chettra Chettra { get; set; }
    }
    public class UpaChetraDetail
    {
        [Key]
        public int UpaChetraDetailId { get; set; }
        public int UpaChetraId { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        [ForeignKey("UpaChetraId")]
        public virtual UpaChetra UpaChetra { get; set; }
    }

    // upabhoktaSamiti
    public class UpabhoktaSamitiDetail
    {
        [Key]
        public int UpabhoktaSamitiDetailId { get; set; }
        public Nullable<System.DateTime> Samiti_Estd_Date { get; set; }
        public string NepaliSamitiEstdDate { get; set; }
        public string DartaNo { get; set; }
        public string Name { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal Beneficiaries_Attendance { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal Beneficiaries_Absent { get; set; }
        public string AnugamanMember { get; set; }
        public string NibedanMiti { get; set; }      
        public string Female_Present { get; set; }
        public string Male_Present { get; set; }       
        public int? BankId { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public int? WardNo { get; set; }
        public int? FiscalYearId { get; set; }

        [ForeignKey("BankId")]
        public Class_A_Bank_List class_A_Bank_List { get; set; }
    }

    public class UpabhoktaSamitiDetailYojanas
    {
        [Key]
        public int Id { get; set; }
        public int UpabhoktaSamitiDetailId { get; set; }
        public int YojanaId { get; set; }

        [ForeignKey(nameof(UpabhoktaSamitiDetailId))]
        public UpabhoktaSamitiDetail UpabhoktaSamitiDetail { get; set; }
        [ForeignKey(nameof(YojanaId))]
        public YojanaSetup YojanaSetup { get; set; }
    }

    public class UpabhoktaSamitiDetailDocType
    {
        [Key]   
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpabhoktaSamitiDetailDocs
    {
        [Key]   
        public int Id { get; set; }
        public int UpabhoktaSamitiDetailId { get; set; }
        public int UpabhoktaSamitiDetailDocTypeId { get; set; }
        public string DocPath { get; set; }

        [ForeignKey(nameof(UpabhoktaSamitiDetailId))]
        public UpabhoktaSamitiDetail UpabhoktaSamitiDetail { get; set; }
        [ForeignKey(nameof(UpabhoktaSamitiDetailDocTypeId))]
        public UpabhoktaSamitiDetailDocType UpabhoktaSamitiDetailDocType { get; set; }
    }


    public class UpabhoktaSamitiMemberDetail
    {
        [Key]
        public int UpabhoktaSamitiMemberDetailId { get; set; }
        public Nullable<int> UpabhoktaSamitiDetailId { get; set; }
        //public int? PadaId { get; set; }
        public string MemberName { get; set; }
		public int? SamitiPostId { get; set; }
		public string Address { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string PhoneNo { get; set; }
        public string DOB { get; set; }
        public string Age { get; set; }
        public string ImagePath { get; set; }
        public string CitizenshipNumber { get; set; }
        public Nullable<bool> Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }

        [ForeignKey("UpabhoktaSamitiDetailId")]
        public virtual UpabhoktaSamitiDetail UpabhoktaSamitiDetail { get; set; }
        //[ForeignKey("PadaId")]
        //public virtual Pada Pada { get; set; } 
        [ForeignKey("SamitiPostId")]
        public virtual SamitiPost SamitiPost { get; set; }
    }
    //tolBikashSamiti
    public class TolBikashSanstha
    {
        [Key]
        public int TolBikashSansthaId { get; set; }
        public int YojanaId { get; set; }
        public string TolBikashSansthaName { get; set; }
        public string DartaNo { get; set; }
        public string TolSamitiEstdDate { get; set; }
        public string Beneficiaries_Attendance { get; set; }
        public string Beneficiaries_Absent { get; set; }
        public string UpastithiDate { get; set; }
        public string Female_Present { get; set; }
        public string AnugamanMember { get; set; }
        public string NibedanMiti { get; set; }
        public bool Status { get; set; }
        public string Address { get; set; }
        public int? WardNo { get; set; }
		public string BankName { get; set; }
		public string AccountNumber { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal BiupurjiAmount { get; set; }

		public int? FiscalYearId { get; set; }
        [ForeignKey("YojanaId")]
        public YojanaSetup yojanaSetup { get; set; }
    }
    public class TolBikashSansthaMember
    {
        [Key]
        public int TolBikashSansthaMemberId { get; set; }
        public Nullable<int> TolBikashSansthaId { get; set; }
        //public int? PadaId { get; set; }
        public int? SchoolPostId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string NagariktaNumber { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string ImagePath { get; set; }
        [ForeignKey("TolBikashSansthaId")]
        public virtual TolBikashSanstha TolBikashSanstha { get; set; }
		[ForeignKey("SamitiPostId")]
		public virtual SamitiPost SamitiPost { get; set; }
	}
    public class SartaSetup
    {
        [Key]
        public int SartaSetupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
    public class YojanaSetup
    {
        [Key]
        public int YojanaSetupId { get; set; }
        public Nullable<int> BudgetSubTypeId { get; set; }
        public string YojanaName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal EstimatedAmount { get; set; }
        public Nullable<int> WardId { get; set; }
        public Nullable<int> BudgetTypeId { get; set; }
        public Nullable<int> FiscalYearId { get; set; }
        public Nullable<int> UpaChhetraId { get; set; }
		public Nullable<int> ShrotId { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal RemainingBudget { get; set; }
       
        [Column(TypeName = "decimal(18, 2)")]
        public decimal SarkarBudget { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UpabhoktaBudget { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OtherBudget { get; set; }
        public string KharchaShirsak { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }

        [ForeignKey("BudgetSubTypeId")]
        public virtual BudgetSubType BudgetSubType { get; set; }
        [ForeignKey("BudgetTypeId")]
        public virtual BudgetType BudgetType { get; set; }
        [ForeignKey("UpaChhetraId")]
        public virtual UpaChetra UpaChetra { get; set; }
    }
    public class BeneficiariesGroup
    {
        [Key]
        public int BeneficiariesGroupId { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total_House { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total_Male { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total_Female { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Community { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Other { get; set; }
        public Nullable<bool> Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }

    }
    public class DarkhasthaForm
    {
        [Key]
        public int DarkhastaFormId { get; set; }
        public string Bank_Name { get; set; }
        public Nullable<int> Ward_No { get; set; }
        public string Samiti_Name { get; set; }
        public string Adakshya_Name { get; set; }
        public string Koshadakshya_Name { get; set; }
        public string Sachib_Name { get; set; }
        public string Chalani_Number { get; set; }
        public string FiscalYearRecordId { get; set; }
        public string Other_Bank_Name { get; set; }
        public int YojanaSetupId { get; set; }
        public Nullable<bool> Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        [ForeignKey("YojanaSetupId")]
        public virtual YojanaSetup YojanaSetup { get; set; }
    }
    public class PlanningSamjhauta
    {
        [Key]
        public int PlanningSamjhautaId { get; set; }
        public int FiscalYearId { get; set; }
        //public string Samjhauta_Org_Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Contegency_Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MarmatSambhar_Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total_Amount { get; set; }
        public Nullable<bool> Samjhauta_Acceptance { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Contegency_Percentage { get; set; }
        public Nullable<int> SamitiDetailId { get; set; }
        public Nullable<int> SartaSetupId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Peski_Katti { get; set; }
        public Nullable<int> TolBikashSansthaId { get; set; }
        public Nullable<int> BidhyalayaBewasthapanSansthaId { get; set; }
        public bool Status { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }


        [ForeignKey("YojanaId")]
        public virtual YojanaSetup YojanaSetup { get; set; }
        [ForeignKey("SamitiDetailId")]
        public virtual UpabhoktaSamitiDetail UpabhoktaSamitiDetail { get; set; }
        [ForeignKey("TolBikashSansthaId")]
        public virtual TolBikashSanstha TolBikashSanstha { get; set; }
    }
    public class PlanningEntry
    {
        [Key]
        public int PlanningEntryId { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        public int FiscalYearId { get; set; }       
        public string Work_Details { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount_Estimate { get; set; }
        public Nullable<int> WorkTypeId { get; set; }
       
        public Nullable<int> WorkAreaId { get; set; }
        public string SerialNo { get; set; }
        public Nullable<int> BudgetSourceId { get; set; }
        public Nullable<int> PlanningTypeId { get; set; }
        public string Planning_Type { get; set; }     
        public string BudgetSirshakNo { get; set; }
        public string BudgetSirshak { get; set; }
        public string KharchaSirshakNo { get; set; }
        public string KharchaSirshak { get; set; }
        public int? TayarGarnePadId { get; set; }
        public int? TayarGarneId { get; set; }
        public int? SifarisGarnePadId { get; set; }
        public int? SifarisGarneId { get; set; }
        public int? RujuGarnePadId { get; set; }
        public int? RujuGarneId { get; set; }
        public int? SwikritGarnePadId { get; set; }
        public int? SwikritGarneId { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Kaifiyat { get; set; }
        public int? PravidhikEmployeePadId { get; set; }
        public int? PravidhikEmployeeId { get; set; }
		public int? WardSifarishPadId { get; set; }
		public int? WardSifarishId { get; set; }
		public int? WardSwikritPadId { get; set; }
		public int? WardSwikritId { get; set; }
		public string PlanningSanketNo { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }

    }
    public class ProjectEntryDetail
    {
        [Key]
        public int ProjectEntryDetail_Id { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        //public string Project_Name { get; set; }
        public string Project_Place { get; set; }
        public string Project_Objective { get; set; }
        public string Project_Acceptance_By { get; set; }
        public string Project_Start_Date { get; set; }
        public string Project_End_Date { get; set; }
        public string Project_Complete_Date { get; set; }
        //[Column(TypeName = "decimal(18, 2)")]
        //public decimal Project_estimated_Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total_Use_Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total_Amount_Source { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ProjectAllocatedAmount { get; set; }
        public string Project_Extend_End_Date { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> Project_Working_Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }

        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }
    public class ProjectOtherSource
    {
        [Key]
        public int ProjectOtherSourceId { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        public string Source_Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Quantity { get; set; }
        public string Material_Details { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Unit { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }
    public class ProjectSourceDetail
    {
        [Key]
        public int ProjectSourceDetailId { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Project_estimated_Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Nepal_Government { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Municipality { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal State { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal NGO_INGO { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Community_Org { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Foreign_Org { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Public_Community { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Loan_Grant { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Other_Source { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total_Amount_Source { get; set; }
        public Nullable<bool> Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }
    public class PlanningEntryAnudan
    {
        [Key]
        public int PlanningEntryAnudanId { get; set; }
        public Nullable<int> PlanningEntryId { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Nagarpalika_Amt { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UpaBhoktaSamiti_Amt { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Other_Amt { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal JanaSahaBhagita_Amt { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        [ForeignKey("PlanningEntryId")]
        public virtual PlanningEntry PlanningEntry { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }
    public class PlanningEntryKistaDetail
    {
        [Key]
        public int PlanningEntryKistaDetailId { get; set; }
        public Nullable<int> PlanningEntryId { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Kista_Kram { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Kista_Rakam { get; set; }
        public Nullable<System.DateTime> Payment_Date { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Peski_Bhuktani { get; set; }
        public string Sarta { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        [ForeignKey("PlanningEntryId")]
        public virtual PlanningEntry PlanningEntry { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }
    public class PlanningPravidikDetails
    {
        [Key]
        public int PlanningPravidikDetailId { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        public Nullable<int> FiscalYearId { get; set; }
        public Nullable<int> ChettraId { get; set; }
        public Nullable<int> UpaChetraId { get; set; }
        public Nullable<int> UpaChetraDetailId { get; set; }
        public string Detail { get; set; }
        public string EsDetail { get; set; }
        public int? UnitId { get; set; } // eki
        public string Pariman { get; set; }
        public string Kaifiyat { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
        [ForeignKey("ChettraId")]
        public virtual Chettra Chettra { get; set; }
        [ForeignKey("UpaChetraId")]
        public virtual UpaChetra UpaChetra { get; set; }
        [ForeignKey("UpaChetraDetailId")]
        public virtual UpaChetraDetail UpaChetraDetail { get; set; }
        [ForeignKey(nameof(UnitId))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Unit Unit { get; set; }
    }

    public class BhuktaniType
    {
        [Key]
        public int BhuktaniTypeId { get; set; }
        public string BhuktaniTypeName { get; set; }
        public Nullable<bool> Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }

    public class PlanningBhuktani
    {
        [Key]
        public int PlanningBhuktaniId { get; set; }
        public int FiscalYearId { get; set; }
        public string Nirman_Upabhokta { get; set; }
        public string UpaBhoktaSamitiAccNumber { get; set; }
        public string Aayojana_Karyakram { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Kul_La_Ie { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal NaPa_Binayajit { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Others { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Jana_Sahabagita { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Peski { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Technical_Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Kantigenci { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Remaining_Bhuktani_Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Agrim_Shulka { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Bahal_Kar { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MarmatShmar { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Katti_Rakam { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Aanya_Raaya { get; set; }
        public string AdakshyaName { get; set; }
        public string SamjhautaDate { get; set; }
        public string FarfarakDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Farchot_Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Running_Bhuktani { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Samajik_Surekchya { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Parishramik { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Dhuwani { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Royality { get; set; }   
        [Column(TypeName = "decimal(18, 2)")]
        public decimal KaryasampannaAnushar { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        public Nullable<int> BhuktaniTypeId { get; set; }
        public Nullable<bool> IsBhuktaniApproval { get; set; }
        public Nullable<bool> Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
        [ForeignKey("BhuktaniTypeId")]
        public virtual BhuktaniType BhuktaniType { get; set; }

    }
    public class ReportRemark
    {
        [Key]
        public int ReportRemarkId { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        public string Planning_Serial_No { get; set; }
        public string Remarks { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }
    public class Class_A_Bank_List
    {
        [Key]
        public int Class_A_Bank_List_Id { get; set; }
        public string BankName_Nep { get; set; }
        public string BankName_Eng { get; set; }
        public string Address { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }
    public class MunicipalitySamitiManjuriPatra
    {
        [Key]
        public int MunicipalitySamitiManjuriPatraId { get; set; }
        public int? PlanningSamjhautaId { get; set; }    
        public string Municipality_Manjuri_Date { get; set; }        
        public Nullable<bool> Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }
    public class PaymentRecord
    {
        [Key]
        public int Payment_Records_Id { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        public string Kista_Kram { get; set; }
        public Nullable<System.DateTime> Payment_Date { get; set; }
        public string Kista_Rakam { get; set; }
        public string Nirmarn_Samagri { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }
    public class OrganizationRepresentative
    {
        [Key]
        public int Organization_Representative_Id { get; set; }
        public int PlanningSamjhautaId { get; set; }
        public int RepresentativePostId { get; set; }
        public int RepresentativeNameId { get; set; }
        
        public string RepresentativeAddress { get; set; }
        public Nullable<bool> Status { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
        [ForeignKey("RepresentativePostId")]
        public virtual UpabhoktaSamitiMemberDetail MemberDetail { get; set; }
        [ForeignKey("RepresentativeNameId")]
        public virtual UpabhoktaSamitiMemberDetail UpabhoktaSamitiMemberDetail { get; set; }
		
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }
    public class AayojanaMaintainance
    {
        [Key]
        public int AayojanaMaintainanceId { get; set; }
        public int? PlanningSamjhautaId { get; set; }
        public string ResponsibleOrg { get; set; }
        public string Janashram { get; set; }
        public string SewaSulka { get; set; }
        public string DasturChanda { get; set; }
        public string LagatAnudhan { get; set; }
        public string InterestSaving { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }
    public class AmanatDetail
    {
        [Key]
        public int AmanatDetailId { get; set; }
        public string AmanatName { get; set; }
        public string Darja { get; set; }
        public string AmantSahiDate { get; set; }
        public int PlanningSamjhautaId { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }

    public class AnugamanMember
    {
        [Key]
        public int AnugamanMemberId { get; set; }
        public int? UpabhoktaSamitiDetailId { get; set; }
        public string Name { get; set; }
        public int? PostId { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string DOB { get; set; }
        public string CitizenshipNo { get; set; }
        public bool? Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
       
        [ForeignKey("UpabhoktaSamitiDetailId")]
        public  UpabhoktaSamitiDetail UpabhoktaSamitiDetail { get; set; }
    }

    public class SamitiPost
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class AnugamanSamitiPost
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SchoolPost
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PragatiBibaran
    {
        [Key]
        public int Id { get; set; }
        public int PlanningSamjhautaId { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal BittiyaPragati { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal BhautikPragati { get; set; }
        public string Remarks { get; set; }

		[ForeignKey("PlanningSamjhautaId")]
		public PlanningSamjhauta PlanningSamjhauta { get; set; }
	}
    public class YojanaKaryakramChecklist()
    {
        [Key]
        public int Id { get; set; }
        public int PlanningSamjhuataId { get; set; }
        public  bool Bhelabata { get; set; }
        public  bool MahilaPratinidhi { get; set; }
        public  bool FemalePercentage { get; set; }
        public  bool Rahobhar { get; set; }
        public  bool KanunBamojim { get; set; }
        public  bool AtleastTwoFemale { get; set; }
        public  bool GathanNirnaya { get; set; }
        public  bool Citizenship { get; set; }
        public bool LagatAnuman { get; set; }
        public  bool PhotoOfworkingArea { get; set; }
        public  bool FarfarakBaki { get; set; }
        public  bool EkpariwarKobadiSadshya { get; set; }
        public  bool WardSifarish { get; set; }
        public bool AnugamanSamiti { get; set; }

    }
     
 
}
