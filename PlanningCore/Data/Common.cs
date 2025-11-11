using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlanningCore.Data
{
    public class Gender
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string Code { get; set; }
    }
    public class BloodGroup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Education
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
    }
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
    }
    public class Nationality
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_Eng { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; } = true;
        public bool IsDefault { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
    public class Language
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_Eng { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; } = true;
        public bool IsDefault { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
    public class FiscalYear
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_En { get; set; }
        public string Code { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public bool IsActive { get; set; }
        public string DateFrom { get; set; }
        public DateTime DateFromEng { get; set; }
        public string DateTo { get; set; }
        public DateTime DateToEng { get; set; }
        public int? PreviousFiscalYearId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
    public class Office
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_En { get; set; }
        public int PalikaType { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int PalikaId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }

        [ForeignKey("PalikaId")]
        public virtual Palika Palika { get; set; }
    }
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string Code { get; set; }
        public bool? Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? WardId { get; set; }
        public int? PalikaId { get; set; }
    }
    public class SubDepartment
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string Code { get; set; }
        public int? DepartmentId { get; set; }
        public bool? Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? WardId { get; set; }
        public int? PalikaId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
    }
    public class State
    {
        [Key]
        public int StateId { get; set; }
        public string StateName { get; set; }
        [Required]
        public string StateNameNep { get; set; }
        public string StateCode { get; set; }
    }
    public class District
    {
        [Key]
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public string DistrictNameNep { get; set; }
        public string DistrictCode { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }
    }
    public class Palika
    {
        [Key]
        public int PalikaId { get; set; }
        [Required]
        public int DistrictId { get; set; }
        public string PalikaName { get; set; }
        [Required]
        public string PalikaNameNep { get; set; }
        public string PalikaCode { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
    }
    public class Ward
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_En { get; set; }
        public string Code { get; set; }
        public string AddressEng { get; set; }
        public string Adakshya { get; set; }
        public string Sachib { get; set; }
        public string Koshadhaskhya { get; set; }
        public string AdakshyaContact { get; set; }
        public string SachibContact { get; set; }
        public string KoshadhaskhyaContact { get; set; }
        public bool? Status { get; set; }
        public string Address { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

    }
    public class Pada
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderBy { get; set; }
        public bool IsDeleted { get; set; } 
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }

    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public int? PadaId { get; set; }
        public int? WardId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public string DeletedDate { get; set; }
        [ForeignKey("PadaId")]
        public virtual Pada Pada { get; set; }
    }

	public class Unit
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string NameEng { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
