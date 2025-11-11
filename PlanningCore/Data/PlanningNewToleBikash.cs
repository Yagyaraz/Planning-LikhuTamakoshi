using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlanningCore.Data
{
    public class NewToleBikash
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Samiti_Estd_Date { get; set; }
        public string NepaliSamitiEstdDate { get; set; }
        public string DartaNo { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Beneficiaries_Attendance { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Beneficiaries_Absent { get; set; }
        public string NibedanMiti { get; set; }
        public string Female_Present { get; set; }
        public string Male_Present { get; set; }
        public int? BankId { get; set; }
        public string AccountNumber { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; } = true;
        public int? WardNo { get; set; }
        public int? FiscalYearId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }

    public class NewToleBikashYojanas
    {
        [Key]
        public int Id { get; set; }
        public int NewToleBikashId { get; set; }
        public int YojanaId { get; set; }

        [ForeignKey(nameof(NewToleBikashId))]
        public NewToleBikash NewToleBikash { get; set; }
        [ForeignKey(nameof(YojanaId))]
        public YojanaSetup YojanaSetup { get; set; }
    }

    public class NewToleBikashMemberDetail
    {
        [Key]
        public int Id { get; set; }
        public int NewToleBikashlId { get; set; }
        public string MemberName { get; set; }
        public int SamitiPostId { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string DOB { get; set; }
        public DateTime DOBEng { get; set; }
        public string ImagePath { get; set; }
        public string CitizenshipNumber { get; set; }
        public bool Status { get; set; } = true;

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }

    public class NewToleBikashAnugamanMember
    {
        [Key]
        public int Id { get; set; }
        public int NewToleBikashId { get; set; }
        public string Name { get; set; }
        public int PostId { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public bool Status { get; set; } = true;

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }

}
