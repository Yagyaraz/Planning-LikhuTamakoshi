using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Printing;

namespace PlanningCore.Data
{
    public class AnugamanSamitiSetup
    {
        [Key]
        public int Id { get; set; }
        public int? YojanaId { get; set; }
        public int? WardId { get; set; }
        public int FiscalYearId { get; set; }
        public int AnugumanType { get; set; }
        public bool IsDeleted {  get; set; }
    }

    public class AnugamanSamitiMember
    {
        [Key]
        public int AnugamanMemberId { get; set; }
        public int? AnugamanSamitiId { get; set; }
        public int? WardId { get; set; } 
        public string Name { get; set; }
        public int? PostId { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        [ForeignKey("AnugamanSamitiId")]
        public AnugamanSamitiSetup AnugamanSamitiSetup { get; set; }
    }
}
