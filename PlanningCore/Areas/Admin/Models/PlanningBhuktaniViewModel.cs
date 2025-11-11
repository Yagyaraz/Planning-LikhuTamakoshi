using System.ComponentModel.DataAnnotations;

namespace PlanningCore.Areas.Admin.Models
{
	public class PlanningBhuktaniViewModel
	{
		public int PlanningBhuktaniId { get; set; }
		public int KarkattiId { get; set; }
		[Display(Name ="आर्थिक बर्ष")]
		public int FiscalYearId { get; set; }
		[Display(Name = "उपभोक्ता समिति")]
		public string Nirman_Upabhokta { get; set; }
		[Display(Name = "योजना / कार्यक्रम")]
		public string Aayojana_Karyakram { get; set; }
		[Display(Name = "उपभोक्ता समितिको खाता नम्बर")]
		public string UpaBhoktaSamitiAccNumber { get; set; }
		[Display(Name = "कुल लगत.ईष्टमेट.")]
		public decimal Kul_La_Ie { get; set; }
		[Display(Name = "पालिका विनियोजित")]
		public decimal NaPa_Binayajit { get; set; }
		[Display(Name = "अन्य")]
		public decimal Others { get; set; }
		[Display(Name = "जन सहभागिता")]
		public decimal Jana_Sahabagita { get; set; }
		[Display(Name = "पेश्की रकम")]
		public decimal Peski { get; set; }
		[Display(Name = "प्राविधिक मूल्याङ्कन")]
		public decimal Technical_Amount { get; set; }
		[Display(Name = "कन्टेन्जेन्सी ")]
		public decimal Kantigenci { get; set; }
		[Display(Name = "भुक्तानी रकम")]
		public decimal Remaining_Bhuktani_Amount { get; set; }
		[Display(Name = "अग्रिम आय कर")]
		public decimal Agrim_Shulka { get; set; }
		[Display(Name = "बहाल कर")]
		public decimal Bahal_Kar { get; set; }
		[Display(Name = "मर्मत सम्भार")]
		public decimal MarmatShmar { get; set; }
		[Display(Name = "जम्मा कट्टी रकम")]
		public decimal Katti_Rakam { get; set; }
		[Display(Name ="")]
		public decimal Aanya_Raaya { get; set; }
		[Display(Name = "अध्यक्षको नाम")]
		public string AdakshyaName { get; set; }
		[Display(Name = "सम्झौता मिति")]
		public string SamjhautaDate { get; set; }
		[Display(Name = "फरफारक गरिएको मिति")]
		public string FarfarakDate { get; set; }
		
		public decimal Farchot_Amount { get; set; }
		[Display(Name = "रनिङ्ग बिल")]
		public decimal Running_Bhuktani { get; set; }
		[Display(Name = "कार्यसम्पन्न अनुसार घटाउन पर्ने रकम")]
		public decimal KaryasampannaAnushar { get; set; }
		[Display(Name = "सामाजिक सुरक्षा कर")]
		public decimal Samajik_Surekchya { get; set; }
		[Display(Name = "पारिश्रमिक कर")]
		public decimal Parishramik { get; set; }
		[Display(Name = "ढुवानी ")]
		public decimal Dhuwani { get; set; }
		[Display(Name = "रोयल्टी")]
		public decimal Royality { get; set; }
        public string  BankName { get; set; }
        public string  AccountNumber { get; set; }
        public string  Address { get; set; }
        public string  KaryasampannaDate { get; set; }
        public string PrintDate { get; set; }

        public Nullable<int> PlanningSamjhautaId { get; set; }
		[Display(Name = "भुक्तानि प्रकार")]
		public Nullable<int> BhuktaniTypeId { get; set; }
		public Nullable<bool> IsBhuktaniApproval { get; set; }
		public Nullable<bool> Status { get; set; }
		public string YojanaName { get; set; }
		public int WardNo { get; set; }
		public string FiscalYearName { get; set; }
        public UpavoktaSamitiDetailViewModel UpabhoktaSamitiDetails { get; set; } = new UpavoktaSamitiDetailViewModel();
		public List<PlanningBhuktaniViewModel> PlanningBhuktaniKarKattiViewModelList { get; set; } = new List<PlanningBhuktaniViewModel>();
		public List<PlanningBhuktaniViewModel> PlanningBhuktaniList { get; set; }
	}
	public class GetInsertedDataFromSamjhauta
	{
        public int PlanningSamjhautaId { get; set; }
        public int FiscalYearId { get; set; }
        public int YojanaSetupId { get; set; }
        public int? WardId { get; set; }
		public int BudgetSourceId { get; set; }
		public int RepresentativeNameId { get; set; }
		public int RepresentativePostId { get; set; }
		public string YojanaAddress { get; set; }
		public decimal EstimatedAmount { get; set; }
		public int UpabhoktaSamitiDetailId { get; set; }
		public int TolbikashSamitiDetailId { get; set; }
		public string SamjhautaDate { get; set; }
		public decimal Municipality { get; set; }
	}

}
