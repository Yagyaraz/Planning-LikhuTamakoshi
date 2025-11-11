using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanningCore.Data
{

    public class Con_ContractType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public bool Status { get; set; }
    }
    public class Con_Consultant
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string MobileNumber { get; set; }
        public string EMail { get; set; }
        public string MainPersonName { get; set; }
        public string MainPersonPhoneNumber { get; set; }
        public string MainPersonAddress { get; set; }
        public string PanNumber { get; set; }
        public string Registration_Number { get; set; }
        public string Registration_Date { get; set; }
        public string VatEndDate { get; set; }
        public string EjajatNumber { get; set; }
        public string EjajatType { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int PalikaId { get; set; }
        public bool IsJV { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }


        [ForeignKey("StateId")]
        public virtual State State { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }

        [ForeignKey("PalikaId")]
        public virtual Palika Palika { get; set; }

    }
    public class Con_JV
    {
        [Key]
        public int Id { get; set; }
        public int ConsultantId { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string MobileNumber { get; set; }
        public string EMail { get; set; }
        public string MainPersonName { get; set; }
        public string MainPersonPhoneNumber { get; set; }
        public string MainPersonAddress { get; set; }
        public string PanNumber { get; set; }
        public string Registration_Number { get; set; }
        public string Registration_Date { get; set; }
        public string VatEndDate { get; set; }
        public string EjajatNumber { get; set; }
        public string EjajatType { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int PalikaId { get; set; }
        [ForeignKey("StateId")]
        public virtual State State { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }

        [ForeignKey("PalikaId")]
        public virtual Palika Palika { get; set; }
        [ForeignKey("ConsultantId")]
        public virtual Con_Consultant Con_Consultant { get; set; }
    }

    public class PaymentType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Con_BidSecurity
    {
        [Key]
        public int Id { get; set; }
        public int YojanaId { get; set; }
        public int ConsultantId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string BGAmount { get; set; }
        public string BSAmount { get; set; }
        public int BankId { get; set; }
        public string BankAccNumber { get; set; }
        public string BGNumber { get; set; }
        public string FromDate { get; set; }
        public DateTime FromDateEng { get; set; }
        public string TODate { get; set; }
        public DateTime ToDateEng { get; set; }
        public string JariDate { get; set; }
        public DateTime JariDateEng { get; set; }
        public string DepositDate { get; set; }
        public DateTime DepositDateEng { get; set; }
		public bool Status { get; set; }
        public int? PaymentTypeId { get; set; }

        public bool FukuwaStatus { get; set; }  
        public string DecisionDate { get; set; }
        public DateTime DecisionDateEng { get;set; } 
		public string DecisionOfficer { get; set; }
        public int? FukuwaTypeId { get; set; }
        public string FukuwaAmount { get; set; }
        public string FukuwaDate { get; set; }
        public DateTime FukuwaDateEng { get; set; }
        public string Docs { get; set; }

        [ForeignKey("ConsultantId")]
        public virtual Con_Consultant Con_Consultant { get; set; } 
        [ForeignKey("YojanaId")]
        public virtual Con_Yojana YojanaSetup { get; set; }
        [ForeignKey(nameof(PaymentTypeId))]
        public PaymentType PaymentType { get; set; }
    }

    public class Con_BankGuarantee
    {
        [Key]
        public int Id { get; set; }
        public int YojanaId { get; set; }
        public int BankId { get; set; }
        public int ConsultantId { get; set; }
        public string BankGuaranteeName { get; set; }
        public int? BGType { get; set; }
        public string BGNumber { get; set; }
        public string Currency { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal Amount { get; set; }
        public string JariMiti { get; set; }
        public DateTime JariMitiEng { get;set; }
        public string FromDate { get; set; }
        public DateTime FromDateEng { get;set; }
        public string ToDate { get; set; }
        public DateTime ToDateEng { get;set; }
        public bool FukuwaStatus { get; set; }
		public int? FukuwaTypeId { get; set; }
		public string FukwaDate { get; set; }
		public DateTime? FukwaDateEng { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal FukwaAmount { get; set; }
		public string Remarks { get; set; }
		public Nullable<int> EmailDays { get; set; }
		public string PaymentType { get; set; }
		public string JammaDate { get; set; }
		public Nullable<System.DateTime> JammaDateEng { get; set; }
		public string BankAccount { get; set; }
		public string PaskiDate { get; set; }
		public Nullable<System.DateTime> PaskiDateEng { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal PaskiAmount { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal PaskiRemaining { get; set; }
		public string NirnayDate { get; set; }
		public Nullable<System.DateTime> NirnayDateEng { get; set; }
		public string NirnayAdikari { get; set; }
		public int? MyadThapTypeId { get; set; }
		public string MyadThapStartDate { get; set; }
		public Nullable<System.DateTime> MyadThapStartDateEng { get; set; }
		public string MyadThapEndDate { get; set; }
		public Nullable<System.DateTime> MyadThapEndDateEng { get; set; }
        public string FileUpload { get; set; }

		[ForeignKey("ConsultantId")]
        public virtual Con_Consultant Con_Consultant { get; set; }
        [ForeignKey("YojanaId")]
        public virtual Con_Yojana YojanaSetup { get; set; }
        [ForeignKey("BankId")]
        public virtual Class_A_Bank_List Class_A_Bank_List { get; set; }

    }
    public class Con_Samjhauta
    {
        [Key] 
        public int Id { get;set; }
        public int FiscalYearId { get; set; }
        public int ThekkaTypeId { get; set; }
        public int ContractTypeId { get; set; }
        public int YojanaId { get; set;}
        public int ConsultantId { get; set; }       
        public string IFBNumber { get; set; }       
        public string ThekkaNumber { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal EstimatedAmount { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal ContractAmtEclVat { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal PsAmount { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal VAT { get; set; }
        public string BolPatraDate { get; set; }
        public DateOnly BolPatraDateEnglish { get; set; }
        public string ContractDate { get; set; }
        public DateOnly ContractDateEnglish { get; set; }
        public string ProjectStartDate { get; set; }
        public DateOnly ProjectStartDateEng { get; set; }
        public string ProjectEndDate { get; set; }
        public bool IsDeleted { get; set; }
       
        public DateOnly ProjectEndDateEng { get; set; }
        [ForeignKey("ConsultantId")]
        public virtual Con_Consultant Con_Consultant { get; set; }
        [ForeignKey("YojanaId")]
        public virtual Con_Yojana YojanaSetup { get; set; }
        [ForeignKey("BankId")]
        public virtual Class_A_Bank_List Class_A_Bank_List { get; set; }
        [ForeignKey("ContractTypeId")]
        public virtual Con_ContractType Con_ContractType { get; set; } 
       

    }
    public class Con_SamjhautaDetails
    {
        public int Id { get; set; }
        public int Con_SamjhautaId { get; set; }
		public string EmployeerId { get; set; }
		public string ContractorId { get; set; }
		public string YojanaAddress { get; set; }
		public int StateId { get; set; }
		public int DistrictId { get; set; }
		public int PalikaId { get; set; }
		public string Ward { get; set; }
		public string Rahobar { get; set; }
		public string SamjhautaPerson { get; set; }
		public string SamjhautaPersonPost { get; set; }
        public int SifarishId { get; set; }
        public int SifarishPadId { get; set; }
        public int SwikritId { get; set;}
        public int SwikritPadId { get; set; }
		[ForeignKey("Con_SamjhautaId")]
		public virtual Con_Samjhauta Con_Samjhauta { get; set; }
		[ForeignKey("StateId")]
		public virtual State State { get; set; }
		[ForeignKey("DistrictId")]
		public virtual District District { get; set; }
		[ForeignKey("PalikaId")]
		public virtual Palika Palika { get; set; }	
        [ForeignKey("SifarishId")]
		public virtual Employee Sifarish { get; set; }
        [ForeignKey("SwikritId")]
		public virtual Employee Swikrit { get; set; }
	}
    public class Con_Insurance
    {
        [Key]
        public int Id { get; set; }
        public int ConSamjhautaId { get; set; }
        public string Name { get; set; }
        public string InsuranceType { get; set; }
        public string InsuranceAmount { get; set; }
        public string PolicyNumber { get; set; }
        public string ValidFrom { get; set; }
        public DateTime? ValidFromEng { get; set; }
        public string ValidTo { get; set; }
        public DateTime? ValidToEng { get; set; }
        public string Amount { get; set; }
        public string CompanyName { get; set; }
        public string IssueDate { get; set; }
        public DateTime? IssueDateEng { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }
		[ForeignKey("ConSamjhautaId")]
		public virtual Con_Samjhauta Con_Samjhauta { get; set; }

	}
    public class Con_Variation
    {
        [Key]
        public int Id { get; set; }
        public int ConSamjhautaId { get; set;}
        public string VariationNumber { get; set;}
        public string ContractAmt { get; set;}
        public string Amount { get; set;}
        public string VoDate { get; set;}
        public DateTime? VoDateEng { get; set;}
        public string Remarks { get; set;}
        public string SamjhautaEndDate { get; set;}
        public DateTime? SamjhautaEndDateEng { get; set;}
        public bool Status { get; set;} 
        public int VariationTypeId { get; set; }
		[ForeignKey("ConSamjhautaId")]
		public virtual Con_Samjhauta Con_Samjhauta { get; set; }
	}

    public class Con_Yojana
    {
        [Key]
        public int Id { get; set; }
        public Nullable<int> BudgetSubTypeId { get; set; }
        public string YojanaName { get; set; }
        public Nullable<int> WardId { get; set; }
        public Nullable<int> BudgetTypeId { get; set; }
        public Nullable<int> FiscalYearId { get; set; }
        public Nullable<int> ShrotId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal EstimatedAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal RemainingBudget { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal SarkarBudget { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UpabhoktaBudget { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OtherBudget { get; set; }
        public string YojanaAddress { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        [ForeignKey("BudgetTypeId")]
        public virtual BudgetType BudgetType { get; set; }
        [ForeignKey("BudgetSubTypeId")]
        public virtual BudgetSubType BudgetSubType { get; set; }
     

    }
    public class Con_Yojana_Ward
    {
		[Key]
		public int Id { get; set; }
		public int WardId { get; set; }
		public int YojanaId { get; set; }
		[ForeignKey("WardId")]
		public virtual Ward Ward { get; set; }
		[ForeignKey("YojanaId")]
		public virtual Con_Yojana Con_Yojana { get; set; }
	}
    public class Con_BankGuaranteeType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Con_ThekkaType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
	public class Con_MyadThapType
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
	}
    public class Con_VariationType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public class Con_FukuwaType
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
	}
	public class Con_ReportType
	{
        [Key]
		public int Id { get; set; }
		public string ReportName_Nep { get; set; }
		public string ReportName_Eng { get; set; }
		public string ReportCode { get; set; }
	}
    public class  Con_PrintReportDetail
    {
        [Key]
        public int Id { get; set; }
        public int YojanaId { get; set; }
        public int? ContractSamjhautaId { get; set; }
        public int? ReportTypeId { get; set; }
        public string ReportCode { get; set; }
        public string PrintContent { get; set; }
        public string PrintDate { get; set; }
        public string PrintDateEng { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        [ForeignKey("YojanaId")]
        public virtual Con_Yojana YojanaSetup { get; set; }
        [ForeignKey("ContractSamjhautaId")]
        public virtual Con_Samjhauta Con_Samjhuata { get; set; }
        [ForeignKey("ReportTypeId")]
        public virtual Con_ReportType Con_ReportType { get; set; }
    }

    public class Con_Bhuktani
    {
        [Key]
        public int Id { get; set; }
        public int ContractSamjhautaId { get; set; }
        public int BhuktaniTypeId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ContractAmt { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BhuktaniAmount { get; set; }
        public string BhuktaniDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrabidhikDetailsAmount { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal RunningBill { get; set; }  
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PsAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal VAT { get; set; }
        public string PrabidhikDetailsMiti { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PaskiAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal KaryDiyekoAmount { get; set; }
        public string YojanaMulykanDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DharutiAmount { get; set; }
      
        public string GoshwaraVoucherNo { get; set; }
        public string BillNumber { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }

        [ForeignKey("ContractSamjhautaId")]
        public  Con_Samjhauta Con_Samjhauta { get; set; }
        [ForeignKey("BhuktaniTypeId")]
        public  BhuktaniType BhuktaniType { get; set; }
    }
    public class Con_KarKatti
    {
        [Key]
        public int Con_katkattiId { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Value { get; set; }
        public Nullable<bool> Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
    public class Con_TaxDeduction
    {
        [Key]
        public int Id { get; set; }
        public int ContractBhuktaniId { get; set; }
        public int ConKarkattiId { get; set; }
        public string Amount { get; set; }
		[ForeignKey("ContractBhuktaniId")]
		public Con_Bhuktani Con_Bhuktani { get; set; }
		[ForeignKey("ConKarkattiId")]
		public Con_KarKatti Con_KarKatti { get; set; }
	}
}

