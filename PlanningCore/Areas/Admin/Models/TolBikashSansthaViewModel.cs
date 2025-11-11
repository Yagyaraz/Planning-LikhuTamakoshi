using PlanningCore.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanningCore.Areas.Admin.Models
{
    public class TolBikashSansthaViewModel
    {
        public int TolBikashSansthaId { get; set; }
        [Required]
        [Display(Name = "बिद्यालय व्यवस्थापन समितिको नाम")]
        public string TolBikashSansthaName { get; set; }
        [Display(Name = "समिति दर्ता न")]
        public string DartaNo { get; set; }
        [Display(Name = "समितिको बैठक बसेको मितिः")]
        public string TolSamitiEstdDate { get; set; }
        [Display(Name = "समिति गठन गर्दा उपस्थित लाभान्वितको संख्या")]
        public string Beneficiaries_Attendance { get; set; }
        [Display(Name = "समिति गठन गर्दा अनुपस्थित लाभान्वितको संख्या")]
        public string Beneficiaries_Absent { get; set; }
        [Display(Name = "कर्मचारीको उपस्थिति मिति")]
        public string UpastithiDate { get; set; }
        [Display(Name = "महिला उपस्थित")]
        public string Female_Present { get; set; }
        [Display(Name = "अनुगमन समिति संख्या")]
        public string AnugamanMember { get; set; }
        [Display(Name = "निवेदन प्राप्त मिति")]
        public string NibedanMiti { get; set; }
        [Display(Name = "ठेगाना ")]
        public string Address { get; set; }
        public string PrintDate { get; set; }

        public int? WardNo { get; set; }
        
        public int? FiscalYearId { get; set; }
        [Display(Name ="योजनाको नाम")]
        public int YojanaId { get; set; }
        [Display(Name = "योजनाको नाम")]
        public string YojanaName { get; set; }
        [Display(Name = "बैंकको नाम")]
        public string BankName { get; set; }
        [Display(Name = "खाता नम्बर")]
        public string AccountNumber { get; set; }
        public decimal BiupurjiAmount { get; set; }
		public List<TolBikashSansthaMemberViewModel> tolBikashSansthaMemberList { get; set; } = new List<TolBikashSansthaMemberViewModel>();
    }
    public class TolBikashSansthaMemberViewModel
    {
        public int TolBikashSansthaMemberId { get; set; }
        public int TolBikashSansthaId { get; set; }
        [Display(Name = "नाम")]
        [Required(ErrorMessage = ("कृपया नाम राख्नुहोस"))]
        public string Name { get; set; }
        [Display(Name = "ठेगाना")]
        public string Address { get; set; }
        [Display(Name = "सम्पर्क नम्बर")]
        public string PhoneNumber { get; set; }
        [Display(Name = "नागरिकता नम्बर")]
        public string NagariktaNumber { get; set; }
        [Display(Name = "बुबाको नाम")]
        public string FatherName { get; set; }
        [Display(Name = "हजुरबुवाको नाम")]
        public string GrandFatherName { get; set; }
        [Display(Name = "फाइल अप्लोड")]
        public string ImagePath { get; set; }
		[Display(Name = "पद")]
		[Required(ErrorMessage = ("कृपया पद छान्नुहोस"))]
		public int? SchoolPostId { get; set; }
		public string PostName { get; set; }

	}
    public class BiupurjiViewModel
    {
     
        public int Id { get; set; }
        public int TolBikashSansthaId { get; set; }
        public string Amount { get; set; }
      
    }
}
