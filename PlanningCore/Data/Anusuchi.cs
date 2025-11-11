using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlanningCore.Data
{
    //Anusuchi
    public class Anusuchi1
    {
        [Key]
        public int Id { get; set; }
        public int PlanningSamjhautaId { get; set; }
        public string UpabhoktaSamitiName { get; set; }
        public string Adakshya { get; set; }
        public string Upadakshya { get; set; }
        public string Kosadakshya { get; set; }
        public string Sachib { get; set; }
        public string EstablishDate_Nep { get; set; }
        public DateTime EstablishDate_Eng { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }

    }
    public class Anusuchi10
    {
        [Key]
        public int Anusuchi10Id { get; set; }
        public int? PlanningSamjhautaId { get; set; }
        public string AdakshyaName { get; set; }
        public string AdakshyaGender { get; set; }
        public string AdakshyaMobileNo { get; set; }
        public bool? Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }
    public class Anusuchi10DiscussionConclusion
    {
        [Key]
        public int Anusuchi10DiscussionConclusionId { get; set; }
        public int? Anusuchi10Id { get; set; }
        public string Conclusion { get; set; }
        [ForeignKey("Anusuchi10Id")]
        public virtual Anusuchi10 Anusuchi10 { get; set; }
    }
    public class Anusuchi10DiscussionSubject
    {
        [Key]
        public int Anusuchi10DiscussionSubjectId { get; set; }
        public int? Anusuchi10Id { get; set; }
        public string Subject { get; set; }
        [ForeignKey("Anusuchi10Id")]
        public virtual Anusuchi10 Anusuchi10 { get; set; }

    }
    public class Anusuchi10Meeting
    {
        [Key]
        public int Anusuchi10MeetingId { get; set; }
        public int? Anusuchi10Id { get; set; }
        public string BaithakNo { get; set; }
        public string BaithakDate { get; set; }
        [ForeignKey("Anusuchi10Id")]
        public virtual Anusuchi10 Anusuchi10 { get; set; }
    }
    public class Anusuchi10Members
    {
        [Key]
        public int Anusuchi10MemberId { get; set; }
        public int? Anusuchi10Id { get; set; }
        public string MemberName { get; set; }
        public string MemberGender { get; set; }
        public string MemberPad { get; set; }
        public string MemberPhone { get; set; }
        [ForeignKey("Anusuchi10Id")]
        public virtual Anusuchi10 Anusuchi10 { get; set; }
    }
    public class Anusuchi11
    {
        [Key]
        public int Anusuchi11Id { get; set; }
        public int PlanningSamjhautaId { get; set; }
        public string Name { get; set; }
        public string Postion { get; set; }
        public string Date_Nep { get; set; }
        public DateTime? Date_Eng { get; set; }
        public string Suggestion { get; set; }
        public bool? Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }

    public class Anusuchi3ExpensesType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class Anusuchi3
    {
        [Key]
        public int Anusuchi3Id { get; set; }
        public int PlanningSamjhautaId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectPlace { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? EstimatedAmount { get; set; }
        public string ProjectStartDate_Nep { get; set; }
        public DateTime? ProjectStartDate_Eng { get; set; }
        public string ProjectEndDate_Nep { get; set; }
        public DateTime? ProjectEndDate_Eng { get; set; }
        public string UpabhoktaSamitiName { get; set; }
        public string AdakshyaName { get; set; }
        public string TotalMember { get; set; }
        public string TotalMaleNo { get; set; }
        public string TotalFemaleNo { get; set; }
        public string TotalBenificialNo { get; set; }
        public string RohabarName { get; set; }
        public string RohabarPostion { get; set; }
        public string RohabarDate { get; set; }
        public bool? Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }
    public class Anusuchi3Bhuktani
    {
        [Key]
        public int Anusuchi3BhuktaniId { get; set; }
        public int? Anusuchi3Id { get; set; }
        public string BhuktaniDetail { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BhuktaniAmount { get; set; }
        [ForeignKey("Anusuchi3Id")]
        public virtual Anusuchi3 Anusuchi3 { get; set; }
    }
    public class Anusuchi3Income
    {
        [Key]
        public int Anusuchi3IncomeId { get; set; }
        public int? Anusuchi3Id { get; set; }
        public string IncomeSource { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AmountQuantity { get; set; }
        public string IncomeRemarks { get; set; }
        [ForeignKey("Anusuchi3Id")]
        public virtual Anusuchi3 Anusuchi3 { get; set; }
    }
    public class Anusuchi3Maujat
    {
        [Key]
        public int Anusuchi3MaujatId { get; set; }
        public int? Anusuchi3Id { get; set; }
        public string MaujatDetail { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MaujatAmount { get; set; }
        public string MaujatRemarks { get; set; }
        [ForeignKey("Anusuchi3Id")]
        public virtual Anusuchi3 Anusuchi3 { get; set; }
    }
    public class Anusuchi3Expenses
    {
        [Key]
        public int AnuSuchi3ExpensesId { get; set; }
        public Nullable<int> Anusuchi3ExpenseTypeId { get; set; }
        public Nullable<int> Anusuchi3Id { get; set; }
        public string ExpensesDetails { get; set; }
        public string ExpensesRate { get; set; }
        public string ExpensesQuantity { get; set; }
        public string ExpensesTotal { get; set; }
        public Nullable<int> Type { get; set; }
        [ForeignKey("Anusuchi3Id")]
        public virtual Anusuchi3 Anusuchi3 { get; set; }
        [ForeignKey("Anusuchi3ExpenseTypeId")]
        public virtual Anusuchi3ExpensesType Anusuchi3ExpensesType { get; set; }
    }
    public class Anusuchi3ProjectWorkDetail
    {
        [Key]
        public int Anusuchi3ProjectWorkDetailId { get; set; }
        public int? Anusuchi3Id { get; set; }
        public string WorkDetail { get; set; }
        public string WorkPlan { get; set; }
        public string WorkProgress { get; set; }
        [ForeignKey("Anusuchi3Id")]
        public virtual Anusuchi3 Anusuchi3 { get; set; }
    }
    public class Anusuchi3WorkDivision
    {
        [Key]
        public int Anusuchi3WorkDivisionId { get; set; }
        public int? Anusuchi3Id { get; set; }
        public string MemberName { get; set; }
        [ForeignKey("Anusuchi3Id")]
        public virtual Anusuchi3 Anusuchi3 { get; set; }
    }
    public class Anusuchi4
    {
        [Key]
        public int Anusuchi4Id { get; set; }
        public int? PlanningSamjhautaId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectPlace { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ProjectEstimatedAmount { get; set; }
        public int? FiscalYearId { get; set; }
        public string ProjectApprovalDate { get; set; }
        public string ProjectToFinishDate { get; set; }
        public string ProjectEndedDate { get; set; }
        public string SamitiExpensesApprovalDate { get; set; }
        public bool? Status { get; set; }
        public int? SamitiDetailId { get; set; }
        public string Adakshya { get; set; }
        public string SadasyaTotal { get; set; }
        public string FemaleSadasya { get; set; }
        public string MaleSadasya { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? FirstKista { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SecondKista { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ThirdKista { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Janashram { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BastugatSahayata { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? LagatSahabhagita { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? NagarpalikaAnudhan { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? UpabhoktaBitya { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? UpabhoktaJanasharamdan { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SthaniyaSanstha { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DartaNikaya { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AnnyaSansthaSahayog { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PradeshSarkar { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SanghiyaSarkar { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ChandaDaan { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalIncome { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }
    public class Anusuchi4Expense
    {
        [Key]
        public int Anusuchi4ExpenseId { get; set; }
        public int? Anusuchi4Id { get; set; }
        public string Jyalla { get; set; }
        public string NirmanSamagriKharid { get; set; }
        public string Dhuwani { get; set; }
        public string Bhada { get; set; }
        public string BewasthapanKharhca { get; set; }
        [ForeignKey("Anusuchi4Id")]
        public virtual Anusuchi4 Anusuchi4 { get; set; }
    }
    public class Anusuchi4ExpenseType
    {
        [Key]
        public int Anusuchi4ExpenseTypeId { get; set; }
        public string ExpensesName { get; set; }
        public int? Anusuchi4Id { get; set; }
        [ForeignKey("Anusuchi4Id")]
        public virtual Anusuchi4 Anusuchi4 { get; set; }
    }
    public class Anusuchi4Income
    {
        [Key]
        public int Anusuchi4IncomeId { get; set; }
        public int? Anusuchi4Id { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? FirstKista { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SecondKista { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ThirdKista { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Janashram { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BastugatSahayata { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? LagatSahabhagita { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? NagarpalikaAnudhan { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? UpabhoktaBitya { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? UpabhoktaJanasharamdan { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SthaniyaSanstha { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DartaNikaya { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AnnyaSansthaSahayog { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PradeshSarkar { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SanghiyaSarkar { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ChandaDaan { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalIncome { get; set; }
        [ForeignKey("Anusuchi4Id")]
        public virtual Anusuchi4 Anusuchi4 { get; set; }
    }
    public class Anusuchi5
    {
        [Key]
        public int AnuSuchi5Id { get; set; }
        public int? PlanningSamjhautaId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectRunOrg { get; set; }
        public string UpabhoktaSamitiAdakshya { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ProjectEstimateAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MunicipalityAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? JanaSahabhagitaAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? OtherOrgSourceAmount { get; set; }
        public string ProjectContractDate { get; set; }
        public string ProjectEndDate { get; set; }
        public string TotalProjectBeneficiaries { get; set; }
        public bool? Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }
    public class Anusuchi6
    {
        [Key]
        public int Anusuchi6Id { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        public string ProjectName { get; set; }
        public string WardNo { get; set; }
        public string StreetName { get; set; }
        public string UpabhoktaSamitiName { get; set; }
        public string Adakshya { get; set; }
        public string Sachib { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AnudanRakam { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ChandaRakam { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? JanaSahabhagitaRakam { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalExpensesTillNow { get; set; }
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
    public class Anusuchi6Janasahabhagita
    {
        [Key]
        public int Anusuchi6JanasahabhagitaId { get; set; }
        public Nullable<int> Anusuchi6Id { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? JanasahabhagitaAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SharamAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? JinsiAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TechnicalReviewAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? UpabhiktaDecisionAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? KistaRakamDemand { get; set; }
        public string Field_Supervise_Decision { get; set; }
        public string Main_Expenses { get; set; }
        [ForeignKey("Anusuchi6Id")]
        public virtual Anusuchi6 Anusuchi6 { get; set; }
    }
    public class Anusuchi6Karyalaya
    {
        [Key]
        public int Anusuchi6KaryalayaId { get; set; }
        public Nullable<int> Anusuchi6Id { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Karyalaya_Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Nirman_Samagri_Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Dashya_Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Adashya_Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Others_Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Travel_Expenses_Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Technical_Supervise_Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Masalanda_Amount { get; set; }
        [ForeignKey("Anusuchi6Id")]
        public virtual Anusuchi6 Anusuchi6 { get; set; }
    }
    public class Anusuchi6Solution
    {
        [Key]
        public int Anusuchi6SolutionId { get; set; }
        public int? Anusuchi6Id { get; set; }
        public string Solutions { get; set; }
        [ForeignKey("Anusuchi6Id")]
        public virtual Anusuchi6 Anusuchi6 { get; set; }
    }
    public class Anusuchi7
    {
        [Key]
        public int Anusuchi7Id { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        public Nullable<System.DateTime> Samiti_Estd_Date { get; set; }
        public string Samiti_Address { get; set; }
        public string Established_Type { get; set; }
        public string Adakshya { get; set; }
        public string Total_Present_No { get; set; }
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
    public class Anusuchi7AnugamanSamiti
    {
        [Key]
        public int Anusuchi7AnugamanSamitiId { get; set; }
        public int? Anusuchi7Id { get; set; }
        public string Position { get; set; }
        public string MemberName { get; set; }
        public string Gender { get; set; }
        public string FatherHusbandName { get; set; }
        public string GrandFatherName { get; set; }
        public string MobileNo { get; set; }
        [ForeignKey("Anusuchi7Id")]
        public virtual Anusuchi7 Anusuchi7 { get; set; }
    }
    public class Anusuchi7UpabhoktaSamiti
    {
        [Key]
        public int Anusuchi7UpabhoktaSamitiId { get; set; }
        public Nullable<int> Anusuchi7Id { get; set; }
        public string Position { get; set; }
        public string MemberName { get; set; }
        public string Gender { get; set; }
        public string FatherHusbandName { get; set; }
        public string GrandFatherName { get; set; }
        public string MobileNo { get; set; }
        [ForeignKey("Anusuchi7Id")]
        public virtual Anusuchi7 Anusuchi7 { get; set; }
    }
    public class Anusuchi8
    {
        [Key]
        public int Anusuchi8Id { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        public Nullable<int> WardNo { get; set; }
        public string YojanaName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BiniyojitAmount { get; set; }
        public string Miti { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Status { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }
    public class Anusuchi8WardMember
    {
        [Key]
        public int Anusuchi8WardMembersId { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        public Nullable<int> Anusuchi8Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> PadaId { get; set; }
        public string PadaName { get; set; }
        [ForeignKey("Anusuchi8Id")]
        public virtual Anusuchi8 Anusuchi8 { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }
    public class Anusuchi8Work
    {
        [Key]
        public int Anusuchi8SampannaId { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        public Nullable<int> Anusuchi8Id { get; set; }
        public string WorkName { get; set; }
        [ForeignKey("AnuSuchi8Id")]
        public virtual Anusuchi8 Anusuchi8 { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }
    public class Anusuchi9
    {
        [Key]
        public int Anusuchi9Id { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        public string Adakshya { get; set; }
        public string Adakshya_Address { get; set; }
        public string Sachib { get; set; }
        public string Sachib_Address { get; set; }
        public string Koshadakshya { get; set; }
        public string Koshadakshya_Address { get; set; }
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
    public class Anusuchi9DiscussionConclusion
    {
        [Key]
        public int AnuSuchi9DiscussionConclusionId { get; set; }
        public Nullable<int> Anusuchi9Id { get; set; }
        public string Conclusions { get; set; }
        [ForeignKey("Anusuchi9Id")]
        public virtual Anusuchi9 Anusuchi9 { get; set; }

    }
    public class AnusuchiDiscussionSubject
    {
        [Key]
        public int AnuSuchi9DiscussionSubjectsId { get; set; }
        public Nullable<int> Anusuchi9_Id { get; set; }
        public string Subjects { get; set; }
        [ForeignKey("Anusuchi9Id")]
        public virtual Anusuchi9 Anusuchi9 { get; set; }
    }
    public class Anusuchi9Meeting
    {
        [Key]
        public int AnuSuchi9MeetingId { get; set; }
        public Nullable<int> Anusuchi9Id { get; set; }
        public string Baithak_No { get; set; }
        public Nullable<System.DateTime> Baithak_Date { get; set; }
        public Nullable<int> BaithakSankhya { get; set; }
        public string BaithakPlace { get; set; }
        public string BaithakPurpose { get; set; }
        public string BaithakTime { get; set; }
        public string NagarPalikaPratinidi { get; set; }
        public string BisesAhtithi { get; set; }
        [ForeignKey("Anusuchi9Id")]
        public virtual Anusuchi9 Anusuchi9 { get; set; }
    }
    public class Anusuchi9Member
    {
        [Key]
        public int AnuSuchi9MemberId { get; set; }
        public Nullable<int> Anusuchi9Id { get; set; }
        public string Member { get; set; }
        public string MemberAddress { get; set; }
        [ForeignKey("Anusuchi9Id")]
        public virtual Anusuchi9 Anusuchi9 { get; set; }
    }
    public class AnusuchiFile
    {
        [Key]
        public int AnusuchiFileId { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        public Nullable<int> AnuSuchi3Id { get; set; }
        public Nullable<int> AnuSuchi4Id { get; set; }
        public Nullable<int> AnuSuchi5Id { get; set; }
        public Nullable<int> AnuSuchi6Id { get; set; }
        public Nullable<int> AnuSuchi7Id { get; set; }
        public Nullable<int> AnuSuchi8Id { get; set; }
        public Nullable<int> AnuSuchi9Id { get; set; }
        public Nullable<int> AnuSuchi10Id { get; set; }
        public string FilePath { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
    }
    public class DocumentUploaded
    {
        [Key]
        public int Id { get; set; }
        public int PlanningSamjhuataId { get; set; }
        public int DocumentTypeId { get; set; }
        public string ImagePath { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }
        [ForeignKey("DocumentTypeId")]
        public virtual DocumentType DocumentType { get; set; }
    }
}
