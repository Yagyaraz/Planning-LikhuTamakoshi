using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanningCore.Models
{
    public class MainSettingViewModel
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "स्थानिय तह")]
        public string Name { get; set; }
    
        [EmailAddress]
        [Display(Name = "इमेल")]
        public string Email { get; set; }
        [Display(Name = "सम्पर्क व्यक्ति")]
        public string ContactName { get; set; }
        [Display(Name = "फोन नं")]
        public string ContactNumber { get; set; }
        [Display(Name = "मोबाइल नं")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string MobileNumber { get; set; }
        [Display(Name = "फ्याक्स नं")]
        public string FaxNo { get; set; }
        [Display(Name = "वेबसाइट")]
        public string Website { get; set; }
        [Display(Name = "ठेगाना")]
        public string Address { get; set; }
        [Display(Name = "ठेगाना अंग्रेजीमा")]
        public string Address2 { get; set; }

        [Display(Name = "Logo")]
        public string LogoPath { get; set; }
        [Display(Name = "Logo")]
        public IFormFile ProfileImage { get; set; }
        [Display(Name = "स्थानिय तह प्रकार")]
        public int PalikaType { get; set; }
        public string PalikaTypeName { get; set; }
        public string PalikaTypeNameEng { get; set; }
        [Display (Name ="प्रदेश")]
        public int? StateId { get; set; }
        [Display(Name = "जिल्ला")]
        public int? DistrictId { get; set; }
        [Display(Name = "स्थानिय तह")]
        public int? PalikaId { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string PalikaName { get; set; }  
        public string StateNameEng { get; set; }
        public string DistrictNameEng { get; set; }
        public string PalikaNameEng { get; set; }
        [Display(Name="Is English LetterHead ")]
        public bool  EnglishLetterHead { get; set; }

        [NotMapped]
        public string WardName { get; set; }

    }
    public class WardViewModel
    {
        public int Id { get; set; }
		[Display(Name = "वडा कार्यालयको नाम")]
		public string Name { get; set; }
		[Display(Name = "Name English")]
		public string Name_En { get; set; }
        [Display(Name="वडा न.")]
        public string Code { get; set; }
        public int? PalikaId { get; set; }
        public string PalikaName { get; set; }
		[Display(Name = "ठेगाना")]
		public string Address { get; set; }
        [Display(Name = "ठेगाना(English)")]
        public string AddressEng { get; set; }
        [Display(Name = "अध्यक्ष")]
        public string Adakshya { get; set; }
        [Display(Name = "सचिव")]
        public string Sachib { get; set; }
        [Display(Name = "कोषाध्यक्ष")]
        public string Koshadhaskhya { get; set; }
        [Display(Name = "अध्यक्ष फोन न.")]
        public string AdakshyaContact { get; set; }
        [Display(Name = "सचिव फोन न.")]
        public string SachibContact { get; set; }
        [Display(Name = "कोषाध्यक्ष फोन न.")]
        public string KoshadhaskhyaContact { get; set; }

    }
}
