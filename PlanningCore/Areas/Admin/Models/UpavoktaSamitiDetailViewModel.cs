using System.ComponentModel.DataAnnotations;

namespace PlanningCore.Areas.Admin.Models
{
	public class UpavoktaSamitiDetailViewModel
	{
		public int UpabhoktaSamitiDetailId { get; set; }
        [Display(Name = "उपभोक्ता समितिको नाम")]
        public Nullable<System.DateTime> Samiti_Estd_Date { get; set; }
		[Display(Name = "समिति गठन भएको मिति")]
		public string NepaliSamitiEstdDate { get; set; }
		[Display(Name = "दर्ता न")]
		public string DartaNo { get; set; }
		[Display(Name = "उपभोक्ता समितिको नाम :")]	
        public string Name { get; set; }
		[Display(Name = " समिति गठन भएको मितिः")]
		public string ContactNo { get; set; }
		//[Display(Name = " समिति गठन भएको मितिः")]
		//public string SahiDate { get; set; }
		[Display(Name = "उपस्थित लाभान्वितको संख्याः")]
		public decimal Beneficiaries_Attendance { get; set; }
		[Display(Name = " अनुपस्थित लाभान्वितको संख्याः")]
		public decimal Beneficiaries_Absent { get; set; }
		//[Display(Name = " समिति गठन भएको मितिः")]
		//public string AnugamanMember { get; set; }
		[Display(Name = "निवेदन  प्राप्त मितिः")]
		public string NibedanMiti { get; set; }
		//[Display(Name = " समिति गठन भएको मितिः")]
		//public string SamitiDate { get; set; }
		[Display(Name = " महिला उपस्थित:")]
		public string Female_Present { get; set; }
		[Display(Name = "पुरुष उपस्थित:")]
		public string Male_Present { get; set; }

		public bool Status { get; set; }
		public int? WardNo { get; set; }
		public int? FiscalYearId { get; set; }
		[Display(Name="ठेगाना ")]
		public string Address { get; set; }
		[Display(Name = "बैङ्कको नाम")]
		public int? BankId { get; set; }
        public string BankName { get; set; }
		[Display(Name = "खाता नम्बर")]
		public string AccountNumber { get; set; }
		public string FiscalYearName { get; set; }
        public string HakimName { get; set; }
        public string CreatedDate { get; set; }
		public int PlanningSamjhautaId { get; set; }
        public string PrintDate { get; set; }

        //Added
        public string Adakshya { get; set; }
		public string Kosadakshya { get; set; }
		public string Sachib { get; set; }
		public string AdakshyaPhone { get; set; }
		public string KosadakshyaPhone { get; set; }
		public string SachibPhone { get; set; }

        [Display(Name = "योजनाको नाम")]
        public List<int> YojanaIds { get; set; }
        public List<string> YojanaNames { get; set; }

        public string BankAddress { get; set; }

        //public List<UpavoktaSamitiDetailViewModel> SamitiList { get; set; } = new List<UpavoktaSamitiDetailViewModel>();
        public List<UpavoktaSamitiMemberDetailViewModel> samitiMemberDetaillist { get; set; } = new List<UpavoktaSamitiMemberDetailViewModel>();
		public List<AnugamanViewModel> AnugamanMemberList { get; set; } = new List<AnugamanViewModel>();
	}
	public class UpavoktaSamitiMemberDetailViewModel
	{
		public int UpabhoktaSamitiMemberDetailId { get; set; }
        public int UpabhoktaSamitiDetailId { get; set; }
        //[Display(Name = "पद")]
        //public int? PadaId { get; set; }
        [Display(Name = "सदस्यको नाम")]
        public string MemberName { get; set; }
		public string PadaName { get; set; }
        [Display(Name = "ठेगाना")]
        public string Address { get; set; }
        [Display(Name = "बुबाको नाम")]
        public string FatherName { get; set; }
        [Display(Name = "हजुर बुबाको नाम")]
        public string GrandFatherName { get; set; }
        [Display(Name = "उमेर")]
        public string Age { get; set; }
        [Display(Name = "जन्म मिति")]
        public string DOB { get; set; }
        [Display(Name = "सम्पर्क नम्बर")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string PhoneNo { get; set; }
		[Display(Name = "पद")]
		public int? SamitiPostId { get; set; }
		[Display(Name = "फोटो")]
		public IFormFile ImagePath { get; set; }
		public string PhotoPath { get; set; }
		public Nullable<bool> Status { get; set; }
		public string Adakshya { get; set; }
		public string Kosadakshya { get; set; }
		public string Sachib { get; set; }
		public string Sadasya { get; set; }
		[Display(Name = "नागरिकता नं.")]
        public string CitizenshipNumber { get; set; }

        //public UpavoktaSamitiMemberDetailViewModel Member { get; set; } = new UpavoktaSamitiMemberDetailViewModel();


    }

	public class AnugamanViewModel
	{
        public int AnugamanMemberId { get; set; }
        public int? UpabhoktaSamitiDetailId { get; set; }
        [Display(Name = "सदस्यको नाम")]
        public string Name { get; set; }
        [Display(Name = "पद")]
        public int? PostId { get; set; }
        [Display(Name = "ठेगाना")]
        public string Address { get; set; }
        [Display(Name = "सम्पर्क नम्बर")]
        public string Contact { get; set; }
        public string Email { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string DOB { get; set; }
        public string CitizenshipNo { get; set; }
        public bool? Status { get; set; }
		public string PadaName { get; set; }
		public List<AnugamanViewModel> AnugamanMemberList {  get; set; }=new List<AnugamanViewModel>();
    }
	public class GetInsertedFieldInSamjhautaViewModel
	{
		public int UpabhoktaSamitiDetailId { get; set; }
		public int TolbikashSamitiDetailId { get ; set; }
		public int YojanaId { get; set; }
		public int BudgetSourceId { get; set; }
		public int? UpaChhetraId { get; set; }
		public int RepresentativeNameId { get; set; }
		public int RepresentativePostId { get; set; }
		public decimal EstimatedAmount { get; set; }
		public int?  WardId { get; set; }
		public string YojanaAddress { get; set; }
		public bool IsSuperAdmin { get; set; }
		public bool IsUser { get; set; }
		public bool IsAdmin { get; set; }

	

	}
	//public class GetDataFromSamitiPostViewModel
	//{
	//	public int UpabhoktaSamitiDetailId { get; set; }
	//	public int TolbikashSamitiDetailId { get; set; }		
	//	public string RepresentativeName { get; set; }
	//	public int? RepresentativePostId { get; set; }		
	//	public string Address { get; set; }



	//}
	public class UpbhoktaSamitiMembersModel
	{
        public string SamitiName { get; set; }
        public string Post { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
    }

    public class UpabhoktaSamitiDetailDocsViewModel
    {
        public int UpabhoktaSamitiDetailId { get; set; }
		public List<UpabhoktaSamitiDetailDocsDetailsViewModel> DocsList { get; set; } = new List<UpabhoktaSamitiDetailDocsDetailsViewModel>();
    }

    public class UpabhoktaSamitiDetailDocsDetailsViewModel
    {
        public int UpabhoktaSamitiDetailDocTypeId { get; set; }
        public string DocPath { get; set; }
        public IFormFile DocsFile { get; set; }
    }

	public class DisplayUpbhokataDetailinPlaningSamjhuta
	{
        public string YojanaDetails { get; set; }
        public decimal EstimatedAmount { get; set; }
    }

}


