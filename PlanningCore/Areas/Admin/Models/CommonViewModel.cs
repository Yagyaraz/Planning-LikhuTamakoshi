using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlanningCore.Areas.Admin.Models
{
    public class FiscalYearViewModel
    {
        public int Id { get; set; }
        [Display (Name="आर्थिक बर्ष")]
        public string Name { get; set; }
        public string Name_En { get; set; }
        public string Code { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        [Display(Name = "सक्रिय")]
        public bool IsActive { get; set; }
        [Display(Name = "मिति देखि")]
        public string DateFrom { get; set; }     
        public DateTime DateFromEng { get; set; }
        [Display(Name = "मिति सम्म")]
        public string DateTo { get; set; }
        public DateTime DateToEng { get; set; }
        public int? PreviousFiscalYearId { get; set; }
    }
    public class OfficeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_En { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int PalikaId { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string PalikaName { get; set; }
    }
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string Code { get; set; }
    }
    public class SubDepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string Code { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
    //public class WardViewModel
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Name_En { get; set; }
    //    public string Code { get; set; }
    //    public int? PalikaId { get; set; }
    //    public string PalikaName { get; set; }
    //    public string Address { get; set; }
    //}
    public class WorkAreaViewModel
    {

        public int WorkAreaId { get; set; }
        [Required]
        public string WorkAreaName { get; set; }
        public Nullable<bool> Status { get; set; }

        public WorkTypeViewModel workTypeViewModel { get; set; }
    }
    public class WorkTypeViewModel
    {
        public int WorkTypeId { get; set; }
        public string WorkTypeName { get; set; }
        public Nullable<bool> Status { get; set; }

    }
    public class ChettraViewModel
    {
        public int ChettraId { get; set; }
        [Display(Name = "क्षेत्र")]
        [Required(ErrorMessage = "कृपया क्षेत्र राख्नुहोस")]
        public string ChettraName { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
    public class UpaChetraViewModel
    {
        public int UpaChettraId { get; set; }
        [Required]
        public int ChettraId { get; set; }
		[Display(Name = "क्षेत्र")]
		public string ChettraName { get; set; }
		[Display(Name = "उप-क्षेत्र")]
		public string UpaChettra { get; set; }
        public string KharchaSirshark { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
    public class UpaChetraDetailViewModel
    {
        public int UpaChetraDetailId { get; set; }
		[Display(Name = "उप-क्षेत्र")]
        public int UpaChetraId { get; set; }
		[Display(Name = "नाम")]
        public string Name { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        public string UpaChetraName { get; set; }
    }



    public class KarKattiViewModel
    {
        public int KarKattiId { get; set; }
        [Display(Name = "कन्टेन्जेन्सी")]
        public decimal Contigency { get; set; }
        [Display(Name = "मर्मत सम्भार")]
        public decimal MarmatSambhar { get; set; }
        [Display(Name = "सामाजिक सुरक्षा कर")]
        public decimal SamajikSurekchya { get; set; }
        [Display(Name = "वाहाल कर")]
        public decimal BahalKar { get; set; }
        [Display(Name = "अग्रिम आय कर")]
        public decimal AgrimShulka { get; set; }
        [Display(Name = "पारिश्रमिक कर")]
        public decimal Parishramik { get; set; }
        [Display(Name = "रोयल्टी कर")]
        public decimal Royality { get; set; }
        [Display(Name = "ढुवानी कर")]
        public decimal Dhuwani { get; set; }
        public Nullable<bool> Status { get; set; } = true;
    }
    public class DocumentTypeViewModel
    {
        public int DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
    public class BhuktaniTypeViewModel
    {
        public int BhuktaniTypeId { get; set; }
        public string BhuktaniTypeName { get; set; }
        public Nullable<bool> Status { get; set; }
        public string DeletedBy { get; set; }
    }
    public class ThekkaShrotTypeViewModel
    {
        public int ThekkaShrotTypeId { get; set; }
        public string ThekkaShrotName { get; set; }
        public string ThekkaShrotName_Nep { get; set; }
        public Nullable<bool> Status { get; set; }
    }
    public class ThekkaKarkattiViewModel
    {
        public int ThekkaKarKattiId { get; set; }
        public decimal Vat { get; set; }
        public decimal Mobilization { get; set; }
        public decimal Dharauti { get; set; }
        public decimal AayaKar { get; set; }
        public Nullable<bool> Status { get; set; }
    }
    public class ThekkaBhuktaniTypeViewModel
    {
        public int ThekkaBhuktaniTypeId { get; set; }
        public string ThekkaBhuktaniTypeName { get; set; }
        public Nullable<bool> Status { get; set; }
    }
    public class SartaSetupViewModel
    {
        public int SartaSetupId { get; set; }
        [Display(Name ="नाम")]
        public string Name { get; set; }
		[Display(Name = "विवरण")]
		public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
    }
    #region YojanaSetup
    public class YojanaSetupViewModel
    {

        public int YojanaSetupId { get; set; }

        [Required(ErrorMessage = "योजनको नाम राख्नुहोस")]
        [Display(Name = "योजनको नाम")]
        public string YojanaName { get; set; }
        [Required(ErrorMessage = "अनुमानित रकम राख्नुहोस")]
        [Display(Name = "अनुमानित रकम")]
        public decimal EstimatedAmount { get; set; }
        [Display(Name = "वडा न.")]
        public Nullable<int> WardId { get; set; }

        [Display(Name = "प्रदेश/सरकार रकम")]
        public decimal SarkarBudget { get; set; }
        [Display(Name = "उपभोक्ता लागत सहभागिता")]
        public decimal UpabhoktaBudget { get; set; }
        [Display(Name = "अन्य श्रोत")]
        public decimal OtherBudget { get; set; }
        [Display(Name = "उपभोक्ता समितिको नाम")]
        public Nullable<int> UpabhoktaSamitiDetailId { get; set; }
		[Display(Name = "टोल बिकास समितिको नाम")]
		public Nullable<int> TolBikashSamitiDetailId { get; set; }
        public string Shrot { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> FiscalYearId { get; set; }
        public string BudgetTypeName { get; set; }
        public string BudgetSubTypeName { get; set; }
        public string UpaChhetra { get; set; }
        public string Kharcha_Sirshak { get; set; }
        public Nullable<int> BudgetSubTypeId { get; set; }
        public Nullable<int> BudgetTypeId { get; set; }
		[Display(Name = "वडा कार्यलयको नाम")]
		public string WardName { get; set; }

	}
	#endregion

	
}
