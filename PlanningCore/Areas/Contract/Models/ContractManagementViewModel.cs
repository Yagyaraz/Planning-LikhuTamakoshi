using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace PlanningCore.Areas.Contract.Models
{
	public class ContractTypeViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string NameEng { get; set; }
		public bool Status { get; set; }
	}
	public class ConsultantViewModel
	{
		public int Id { get; set; }
		[Required]
		[Display(Name = "निर्माण व्यवसायी")]
		public string Name { get; set; }
		[Display(Name = "निर्माण व्यवसायी(ENG)")]
		public string NameEng { get; set; }
		[Display(Name = "मोबाइल नम्बर")]
		[RegularExpression("^\\d{10}$", ErrorMessage = "Phone Number Must be 10 Numbers")]
		public string MobileNumber { get; set; }
		[Required(ErrorMessage = "Field can't be empty")]
		[DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
		[Display(Name = "ई-मेल")]
		public string EMail { get; set; }
		[Required]
		[Display(Name = "मुख्य व्यक्तिको नाम")]
		public string MainPersonName { get; set; }
		[Display(Name = "मुख्य व्यक्तिको सम्पर्क नम्बर")]
		public string MainPersonPhoneNumber { get; set; }
		[Display(Name = "ठेगाना")]
		public string MainPersonAddress { get; set; }
		[Display(Name = "कम्पनी दर्ता नम्बर")]
		public string Registration_Number { get; set; }
		[Display(Name = "कम्पनी दर्ता मिति")]
		public string Registration_Date { get; set; }

		[Display(Name = "VAT समाप्त हुने मिति")]
		public string VatEndDate { get; set; }
		[Display(Name = "इजाजत नम्बर")]
		public string EjajatNumber { get; set; }
		[Display(Name = "इजाजत वर्ग")]
		public string EjajatType { get; set; }

		[Display(Name = "स्थायी लेखा नम्बर")]
		public string PanNumber { get; set; }

		[Display(Name = "प्रदेश")]
		public int StateId { get; set; }
		[Display(Name = "जिल्ला")]
		public int DistrictId { get; set; }
		[Display(Name = "पालिका")]
		public int PalikaId { get; set; }
		public bool Status { get; set; }
		public bool IsAllowToDelete { get; set; }
		[Display(Name = "JV")]
		public bool IsJV { get; set; }
		public List<ConsultantViewModel> ConsultantList { get; set; } = new List<ConsultantViewModel>();
		//public List<JointVentureViewModel> JointVentureList { get; set; } = new List<JointVentureViewModel>();
		public List<JointVentureViewModel> JointVentureList { get; set; } = new List<JointVentureViewModel>();

	}
	public class JointVentureViewModel
	{
		public int Id { get; set; }
		public int? ConsultantId { get; set; }
		[Required]
		[Display(Name = "निर्माण व्यवसायी")]
		public string Name { get; set; }
		[Display(Name = "निर्माण व्यवसायी(ENG)")]
		public string NameEng { get; set; }
		[Display(Name = "मोबाइल नम्बर")]
		[RegularExpression("^\\d{10}$", ErrorMessage = "Phone Number Must be 10 Numbers")]
		public string MobileNumber { get; set; }
		[Required(ErrorMessage = "Field can't be empty")]
		[DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
		[Display(Name = "ई-मेल")]
		public string EMail { get; set; }
		[Required]
		[Display(Name = "मुख्य व्यक्तिको नाम")]
		public string MainPersonName { get; set; }
		[Display(Name = "मुख्य व्यक्तिको सम्पर्क नम्बर")]
		public string MainPersonPhoneNumber { get; set; }
		[Display(Name = "ठेगाना")]
		public string MainPersonAddress { get; set; }
		[Display(Name = "कम्पनी दर्ता नम्बर")]
		public string Registration_Number { get; set; }
		[Display(Name = "कम्पनी दर्ता मिति")]
		public string Registration_Date { get; set; }
		[Display(Name = "कार्यालयको फोन नम्बर")]
		public string Office_Phone { get; set; }
		[Display(Name = "VAT समाप्त हुने मिति")]
		public string VatEndDate { get; set; }
		[Display(Name = "इजाजत नम्बर")]
		public string EjajatNumber { get; set; }
		[Display(Name = "इजाजत वर्ग")]
		public string EjajatType { get; set; }
		[Display(Name = "स्थायी लेखा नम्बर")]
		public string PanNumber { get; set; }

        [Display(Name = "प्रदेश")]
        public int StateId { get; set; }
        [Display(Name = "जिल्ला")]
        public int DistrictId { get; set; }
        [Display(Name = "पालिका")]
        public int PalikaId { get; set; }
    }
	public class BidSecurityViewModel
	{
		public int Id { get; set; }
		[Display(Name = "योजनाको नाम")]
		public int YojanaId { get; set; }
		[Display(Name = "बैंकको नाम")]
		public int BankId { get; set; }
		[Display(Name = "निर्माण व्यवसायी")]
		public int ConsultantId { get; set; }
		[Display(Name = "नाम")]
		public string Name { get; set; }
		[Display(Name = "ठेगाना")]
		public string Address { get; set; }
		[Display(Name = "रकम")]
		public string BGAmount { get; set; }
		[Display(Name = "BS रकम")]
		public string BSAmount { get; set; }
		[Display(Name = "बैंक खाता नम्बर")]
		public string BankAccNumber { get; set; }
		[Display(Name = "BG नम्बर")]
		public string BGNumber { get; set; }
		[Display(Name = "मिति देखि")]
		public string FromDate { get; set; }
		public DateTime FromDateEng { get; set; }= DateTime.Now;
		[Display(Name = "मिति सम्म")]
		public string TODate { get; set; }

		public DateTime ToDateEng { get; set; }
		[Display(Name = "जारी मिति")]
		public string JariDate { get; set; }
		public DateTime JariDateEng { get; set; } = DateTime.Now;
		[Display(Name = "फुकुवा")]
		public bool FukuwaStatus { get; set; }
		[Display(Name = "निर्णय मिति")]
		public string DecisionDate { get; set; }
		public DateTime DecisionDateEng { get; set; } = DateTime.Now;
		[Display(Name = "निर्णय आधिकारी")]
		public string DecisionOfficer { get; set; }
		[Display(Name = "फुकुवाको माध्यम ")]
		public int? FukuwaTypeId { get; set; }
		[Display(Name = "फुकुवा रकम")]
		public string FukuwaAmount { get; set; }
		[Display(Name = "फुकुवा मिति")]
		public string FukuwaDate { get; set; }
		public DateTime FukuwaDateEng { get; set; } = DateTime.Now;
		[Display(Name = "जम्मा गरेको माध्यम")]
        public int? PaymentTypeId { get; set; }
		[Display(Name = "फाइल अपलोड")]
        public IFormFile File { get; set; }

		[Display(Name = "फाइल अपलोड")]
        public string Docs { get; set; }
        public bool Status { get; set; }
		public string BankName { get; set; }
		public string PaymentTypeName { get; set; }
	}

	public class BankGuaranteeViewModel
	{
		public int Id { get; set; }
		[Display(Name = "योजनाको नाम")]
		public int YojanaId { get; set; }
		[Display(Name = "बैंकको नाम")]

		public int BankId { get; set; }
		[Display(Name = "निर्माण व्यवसायी")]
		public int ConsultantId { get; set; }
		[Display(Name = "बैंक ग्यारेन्टीको नाम")]
		public string BankGuaranteeName { get; set; }
		[Display(Name = "बैंक ग्यारेन्टीको प्रकार")]
		public int? BGType { get; set; }
		[Display(Name = "बैंक ग्यारेन्टी नम्बर")]
		public string BGNumber { get; set; }
		[Display(Name = "मुद्रा")]
		public string Currency { get; set; }
		[Display(Name = "बैंक ग्यारेन्टी रकम")]
		public decimal Amount { get; set; }
		[Display(Name = "जारी गरेको मिति")]
		public string JariMiti { get; set; }
		
		public DateTime JariMitiEng { get; set; }
		[Display(Name = "ग्यारेन्टी देखी")]
		public string FromDate { get; set; }
		[Display(Name = "आर्थिक बर्ष")]
		public DateTime FromDateEng { get; set; }
		[Display(Name = "ग्यारेन्टी सम्म")]
		public string ToDate { get; set; }
		[Display(Name = "आर्थिक बर्ष")]
		public DateTime ToDateEng { get; set; }
		[Display(Name = "योजनाको नाम")]
		public string YojanaName { get; set; }
		[Display(Name = "फुकुवा")]
		public bool FukuwaStatus { get; set; }
		[Display(Name = "फुकुवाको माध्यम")]
		public int? FukuwaTypeId { get; set; }
		[Display(Name = "फुकुवा मिति")]
		public string FukwaDate { get; set; }
		public DateTime? FukwaDateEng { get; set; }
		[Display(Name = "फुकुवा रकम")]
		public decimal FukwaAmount { get; set; }
		[Display(Name = "कैफियत")]
		public string Remarks { get; set; }
		public Nullable<int> EmailDays { get; set; }
		public string PaymentType { get; set; }
		public string JammaDate { get; set; }
		public Nullable<System.DateTime> JammaDateEng { get; set; }
		[Display(Name = "बैंक ग्यारेन्टी नम्बर")]
		public string BankAccount { get; set; }
		[Display(Name = "पेस्की भुक्तानी मिति")]
		public string PaskiDate { get; set; }
		public Nullable<System.DateTime> PaskiDateEng { get; set; }
		[Display(Name = "पेश्की फर्छ्यौट रकम")]
		public decimal PaskiAmount { get; set; }
		[Display(Name = "पेस्की बाकी रकम")]
		public decimal PaskiRemaining { get; set; }
		[Display(Name = "निर्णय मिति")]
		public string NirnayDate { get; set; }
		public Nullable<System.DateTime> NirnayDateEng { get; set; }
		[Display(Name = "निर्णय गर्ने अधिकारी")]
		public string NirnayAdikari { get; set; }
		[Display(Name = "बैंक जमानत न.")]
		public int? MyadThapTypeId { get; set; }
		[Display(Name = "म्याद थप गरेको सुरु मिति")]
		public string MyadThapStartDate { get; set; }
		public Nullable<System.DateTime> MyadThapStartDateEng { get; set; }
		[Display(Name = "म्याद थप गरेको अन्तिम मिति")]
		public string MyadThapEndDate { get; set; }
		public Nullable<System.DateTime> MyadThapEndDateEng { get; set; }
		public List<BankGuaranteeViewModel> BankGuaranteeList { get; set; } = new List<BankGuaranteeViewModel>();
	}
	public class ContractSamjhautaViewModel
	{

		public int Id { get; set; }

		[Display(Name = "आर्थिक बर्ष")]
		public int FiscalYearId { get; set; }
		[Display(Name = "ठेक्काको प्रकार")]
		public int ThekkaTypeId { get; set; }
		[Display(Name = "ठेक्काको  उप-प्रकार")]
		public int ContractTypeId { get; set; }
		[Display(Name = "योजनाको नाम")]
		public int YojanaId { get; set; }
		[Display(Name = "निर्माण व्यवसायी")]
		public int ConsultantId { get; set; }
		[Display(Name = "योजनाको ठेगाना")]
		public string YojanaAddress { get; set; }
		[Display(Name = "ठेक्का नम्बर")]
		public string ThekkaNumber { get; set; }
		[Display(Name = "सम्झौता रकम")]
		public decimal EstimatedAmount { get; set; }
		[Display(Name = "सम्झौता रकम VAT बाहेक")]
		public decimal ContractAmtEclVat { get; set; }
		[Display(Name = "PS रकम")]
		public decimal PsAmount { get; set; }
		[Display(Name = " VAT")]
		public decimal VAT { get; set; }
		[Display(Name = "बोलपत्र आव्हान मिति")]
		public string BolPatraDate { get; set; }
		[Display(Name = "बोलपत्र आव्हान मिति अङ्ग्रेजीमा")]
		public DateOnly BolPatraDateEnglish { get; set; }
		[Display(Name = "सम्झौता मिति")]
		public string ContractDate { get; set; }
		[Display(Name = "")]
		public DateOnly ContractDateEnglish { get; set; }
		[Display(Name = "योजनाको सुरु मिति")]
		public string ProjectStartDate { get; set; }
		[Display(Name = "")]
		public DateOnly ProjectStartDateEng { get; set; }
		[Display(Name = "आयोजना सम्पन्न मिति")]
		public string ProjectEndDate { get; set; }
		[Display(Name = "")]
		public DateOnly ProjectEndDateEng { get; set; }
		[Display(Name = "रहोबर")]
		public string Rahobar { get; set; }
		[Display(Name = "सम्झौता गर्न आउने ब्यक्तिको नाम")]
		public string SamjhautaPerson { get; set; }
		[Display(Name = "सम्झौता गर्न आउने ब्यक्तिको पद")]
		public string SamjhautaPersonPost { get; set; }
		[Display(Name = "प्रदेश")]
		public int StateId { get; set; }
		[Display(Name = "जिल्ला")]
		public int DistrictId { get; set; }
		[Display(Name = "स्थानिय पालिका")]
		public int PalikaId { get; set; }
		[Display(Name = "वडा नं")]
		public List<int> WardIds { get; set; }
		[Display(Name = "वडा नं")]
		public string Ward { get; set; }
		[Display(Name = "IFB नम्बर")]
		public string IFBNumber { get; set; }
		[Display(Name = "")]
		public bool IsDeleted { get; set; } = false;
		[Display(Name = "Employeer")]
		public string EmployeerId { get; set; }
		[Display(Name = "Contractor")]
		public string ContractorId { get; set; }
		[Display(Name = "सिफारिस गर्ने ")]
		public int SifarishId { get; set; }
		[Display(Name = "सिफारिस गर्ने पद ")]
		public int SifarishPadId { get; set; }
		[Display(Name = "स्वीकृत गर्ने")]
		public int SwikritId { get; set; }
		[Display(Name = "स्वीकृत गर्ने पद ")]
		public int SwikritPadId { get; set; }
		[Display(Name = "योजनाको नाम")]
		public string YojanaName { get; set; }
		[Display(Name = "ठेक्काको प्रकार")]
		public string ContractTypeName { get; set; }
		[Display(Name = "निर्माण व्यवसायीको नाम")]
		public string ConsultantName { get; set; }

		[Display(Name = "प्रदेश")]
		public string StateName { get; set; }
		[Display(Name = "जिल्ला")]
		public string DistrictName { get; set; }
		[Display(Name = "स्थानिय पालिका")]
		public string PalikaName { get; set; }

		public string SifarishPersonName { get; set; }
		public string SifarishPadName { get; set; }
		public string SwikritPersonName { get; set; }
		public string SwikritPadName { get; set; }
		public string SamitiName { get; set; }
		public string MainPersonAddress { get; set; }
		public int ContractSamjhautaDetailsId { get; set; }
		public string PrintContent { get; set; }
        public string CEOName { get; set; }

        //Bhuktani
        public decimal ContractAmt { get; set; }

		public decimal BhuktaniAmount { get; set; }
		public string BhuktaniDate { get; set; }

		public decimal PrabidhikDetailsAmount { get; set; }


		public decimal RunningBill { get; set; }

		public decimal BhuktaniPsAmount { get; set; }

		public decimal BhuktaniVAT { get; set; }
		public string PrabidhikDetailsMiti { get; set; }

		public decimal PaskiAmount { get; set; }

		public decimal KaryDiyekoAmount { get; set; }
		public string YojanaMulykanDate { get; set; }

		public decimal DharutiAmount { get; set; }

		public string GoshwaraVoucherNo { get; set; }
		public string BillNumber { get; set; }
		public string ConsultantAddress { get; set; }
    }
    public class InsuranceViewModel
	{
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
		public bool Status { get; set; } = true;
	}
	public class VariationViewModel
	{
		public int Id { get; set; }
		public int ConSamjhautaId { get; set; }
		public string VariationNumber { get; set; }
		public string ContractAmt { get; set; }
		public string Amount { get; set; }
		public string VoDate { get; set; }
		public DateTime? VoDateEng { get; set; }
		public string Remarks { get; set; }
		public string SamjhautaEndDate { get; set; }
		public DateTime? SamjhautaEndDateEng { get; set; }
        public int VariationTypeId { get; set; }
    }

	public class Con_YojanaViewModel
	{
        public int Id { get; set; }
        public Nullable<int> BudgetSubTypeId { get; set; }
		[Display(Name = "योजना नाम")]
		public string YojanaName { get; set; }
		[Display(Name = "वडा")]
		public Nullable<int> WardId { get; set; }
        public Nullable<int> BudgetTypeId { get; set; }
        public Nullable<int> FiscalYearId { get; set; }
        public Nullable<int> ShrotId { get; set; }
	
		public decimal Amount { get; set; }
		[Display(Name = "लागत रकम")]
		public decimal EstimatedAmount { get; set; }
        public decimal RemainingBudget { get; set; }
        public decimal SarkarBudget { get; set; }
        public decimal UpabhoktaBudget { get; set; }  
        public decimal OtherBudget { get; set; }
		[Display(Name = "योजना ठेगाना")]
		public string YojanaAddress { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAllowToDelete { get; set; }

		[Display(Name = "वडा")]
		public List<int> WardIds { get; set; }
		public List<Con_Yojana_WardViewModel> wardList { get; set; } = new List<Con_Yojana_WardViewModel>();
    }
	public class Con_Yojana_WardViewModel
	{
		public int Id { get; set; }
		public int WardId { get; set; }
		public int YojanaId { get; set; }
	}

	public class Con_ReportViewModel
	{
		public int YojanaId { get; set; }
		public int ReportId { get; set; }
		public int PostId { get; set; }
		public int EmpId { get; set; }
		public string PrintDate { get; set; }
		public DateTime? PrintDateEng { get; set; }
	}
	public class Con_ReportTypeViewModel
	{
		public int Id { get; set; }
		public string ReportName { get; set; }
		public string ReportCode { get; set; }
	}

	public class Con_PrintReportDetailViewModel
	{
        public int Id { get; set; }
        public int YojanaId { get; set; }
        [DisplayName("योजनाको नाम")]
        public string YojanaName { get; set; }
        public int? ContractSamjhautaId { get; set; }
        public int? ReportTypeId { get; set; }
        public string ReportName { get; set; }
        public string ReportCode { get; set; }
        public string PrintContent { get; set; }
        public string PrintDate { get; set; }
        public string PrintDateEng { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<Con_PrintReportDetailViewModel> ReportList = new List<Con_PrintReportDetailViewModel>();
    }
	public class Con_BhuktaniViewModel
	{
        public int ConBhuktaniId { get; set; }
        public int ContractSamjhautaId { get; set; }
        public int BhuktaniTypeId { get; set; }
       
        public decimal ContractAmt { get; set; }
       
        public decimal BhuktaniAmount { get; set; }
        public string BhuktaniDate { get; set; }
       
        public decimal PrabidhikDetailsAmount { get; set; }

       
        public decimal RunningBill { get; set; }
       
        public decimal PsAmount { get; set; }
       
        public decimal VAT { get; set; }
        public string PrabidhikDetailsMiti { get; set; }
       
        public decimal PaskiAmount { get; set; }
       
        public decimal KaryDiyekoAmount { get; set; }
        public string YojanaMulykanDate { get; set; }
       
        public decimal DharutiAmount { get; set; }

        public string GoshwaraVoucherNo { get; set; }
        public string BillNumber { get; set; }
        public bool Status { get; set; } = true;
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; } = DateTime.Now;
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public List<Con_BhuktaniViewModel> ContractBhuktaniList { get; set; }
		public List<Con_TaxDeductionViewModel> ConTaxList { get; set; } = new List<Con_TaxDeductionViewModel>();

	}

	public class Con_KarKattiViewModel
    {
      
        public int Con_katkattiId { get; set; }
        public string Name { get; set; }
       
        public decimal Value { get; set; }
		public Nullable<bool> Status { get; set; } = true;
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
	public class Con_TaxDeductionViewModel
	{
		
		public int TaxDeductionId { get; set; }
		public int ContractBhuktaniId { get; set; }
		public int ConKarkattiId { get; set; }
		public string Amount { get; set; }
		
	}
}

