using System.ComponentModel.DataAnnotations;

namespace PlanningCore.Areas.Admin.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public int? WardId { get; set; }
        public string Name { get; set; }
        public string WardName { get; set; }
        public int? PadaId { get; set; }
        public string PadaName { get; set; }
    }
    public class PadaViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class UnitViewModel
    {
        public int Id { get; set; }
        [Display(Name = "नाम")]
		public string Name { get; set; }
        [Display(Name = "नाम (Eng)")]
		public string NameEng { get; set; }
    }

    public class BankViewModel
    {
        public int Id { get; set; }
        [Display(Name = "नाम")]
        public string BankName_Nep { get; set; }
        [Display(Name = "नाम (Eng)")]
        public string BankName_Eng { get; set; }
        [Display(Name = "ठेगाना")]
        public string Address { get; set; }
    }
    public class AnugamanSamitiSetupViewModel
    {
        public int Id { get; set; }
        public int? YojanaId { get; set; }
        public int? WardId { get; set; }
        public int FiscalYearId { get; set; }
        public int AnuhumanSamitiType { get; set; }
        public string AnuhumanSamitiTypeName {  get; set; }
        public bool Status {  get; set; }
        public List<AnugamanSamitiMemberViewModel> AnugamanSamitiList { get; set; } = new List<AnugamanSamitiMemberViewModel>();
        public List<AnugamanSamitiMemberViewModel> AnugamanSamitiListPalika { get; set; } = new List<AnugamanSamitiMemberViewModel>();

    }

    public class AnugamanSamitiMemberViewModel
    {
        public int AnugamanMemberId { get; set; }
        public int? AnugumanSamitiId {  get; set; }
        public string Name { get; set; }
        public string PostName { get; set; }
        public int? PostId { get; set; }
        public int? WardID { get; set; }
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
    }

}
