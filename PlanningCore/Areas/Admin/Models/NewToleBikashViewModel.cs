using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanningCore.Areas.Admin.Models
{
    public class NewToleBikashViewModel
    {
        public int Id { get; set; }
		[Display(Name = "गठन भएको मिति")]
        public DateTime? Samiti_Estd_Date { get; set; }
		[Display(Name = "गठन भएको मिति")]
        public string NepaliSamitiEstdDate { get; set; }
		[Display(Name = "दर्ता न")]
        public string DartaNo { get; set; }
        [Display(Name = "नाम")]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
		[Display(Name = "उपस्थित लाभान्वितको संख्या")]
        public decimal Beneficiaries_Attendance { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
		[Display(Name = " अनुपस्थित लाभान्वितको संख्या")]
        public decimal Beneficiaries_Absent { get; set; }
		[Display(Name = "निवेदन  प्राप्त मिति")]
        public string NibedanMiti { get; set; }
		[Display(Name = " महिला उपस्थित")]
        public string Female_Present { get; set; }
		[Display(Name = "पुरुष उपस्थित")]
        public string Male_Present { get; set; }
		[Display(Name = "बैङ्कको नाम")]
        public int? BankId { get; set; }
		[Display(Name = "खाता नम्बर")]
        public string AccountNumber { get; set; }
        public int PlanningSamjhautaId { get; set; }
        public string PrintDate { get; set; }
        [Display(Name ="ठेगाना ")]
        public string Address { get; set; }
        [Display(Name = "योजनाको नाम")]
        public List<int> YojanaIds { get; set; }

        [NotMapped]
        public string BankName { get; set; }
        [NotMapped]
        public List<string> YojanaNames { get; set; }

        public List<NewToleBikashMemberDetailViewModel> NewToleBikashMemberList { get; set; } = new List<NewToleBikashMemberDetailViewModel>();
        public List<NewToleBikashAnugamanMemberViewModel> AnugamanMemberList { get; set; } = new List<NewToleBikashAnugamanMemberViewModel>();
    }

    public class NewToleBikashMemberDetailViewModel
    {
        public int Id { get; set; }
        public int NewToleBikashlId { get; set; }
        [Display(Name = "सदस्यको नाम")]
        public string MemberName { get; set; }
        [Display(Name = "पद")]

        public int SamitiPostId { get; set; }
        [Display(Name = "ठेगाना")]
        public string Address { get; set; }
        [Display(Name = "सम्पर्क नम्बर")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string PhoneNo { get; set; }
        [Display(Name = "जन्म मिति")]
        public string DOB { get; set; }
        [Display(Name = "उमेर")]
        public string Age { get; set; }
        public DateTime DOBEng { get; set; }
        [Display(Name = "फोटो")]
        public string ImagePath { get; set; }
        [Display(Name ="नागरिकता न.")]
        public string CitizenshipNumber { get; set; }
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        public string SamitiPostName { get; set; }
    }

    public class NewToleBikashAnugamanMemberViewModel
    {
        public int Id { get; set; }
        public int NewToleBikashId { get; set; }
        [Display(Name = "सदस्यको नाम")]
        public string Name { get; set; }
        [Display(Name = "पद")]
        public int PostId { get; set; }
        [Display(Name = "ठेगाना")]
        public string Address { get; set; }
        [Display(Name = "सम्पर्क नम्बर")]
        public string Contact { get; set; }

        [NotMapped]
        public string PostName { get; set; }
    }
}
