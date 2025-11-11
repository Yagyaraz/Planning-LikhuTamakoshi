using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlanningCore.Data
{
    public class ThekkaShrotType
    {
        [Key]
        public int ThekkaShrotTypeId { get; set; }
        public string ThekkaShrotName { get; set; }
        public string ThekkaShrotName_Nep { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Status { get; set; }
    }

    public class ThekkaFiscalYear
    {
        [Key]
        public int ThekkaFiscalYearId { get; set; }
        public int ThekkaSamjhautaId { get; set; }
        public Nullable<int> FiscalYearId { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
    public class ThekkaWard
    {
        [Key]
        public int ThekkaWardId { get; set; }
        public Nullable<int> WardId { get; set; }
        public Nullable<int> ThekkaSamjautaId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
    public class ThekkaShrot
    {
        [Key]
        public int ThekkaShrotId { get; set; }
        public Nullable<int> ThekkaSamjhuataId { get; set; }
        public Nullable<int> ThekkaShrotTypeId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        [ForeignKey("ThekkaSamjhuataId")]
        public virtual ThekkaSamjhauta ThekkaSamjauta { get; set; }
        [ForeignKey("ThekkaShrotTypeId")]
        public virtual ThekkaShrotType ThekkaShrotType { get; set; }
    }
    public class ThekkaSamjhauta
    {
        [Key]
        public int ThekkaSamjhautaId { get; set; }
        public int FiscalYearId { get; set; }
        public string CompanyName { get; set; }
        public string ProjectName { get; set; }
        public string ThekkaNo { get; set; }
        public string TotalAmt { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Contengency { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Kul_Lagat_WithoutContengency { get; set; }
        public string BolpatraDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string SamjautaMiti { get; set; }
        public Nullable<bool> Status { get; set; }
        public string BolPatraDatakoName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ContengencyPercentage { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }



    }
    public class ThekkaBhuktaniType
    {
        [Key]
        public int ThekkaBhuktaniTypeId { get; set; }
        public string ThekkaBhuktaniTypeName { get; set; }
        public Nullable<bool> Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

    }
    public class ThekkaBhuktani
    {
        [Key]
        public int ThekkaBhuktaniId { get; set; }
        public Nullable<int> ThekkaSamjautaId { get; set; }
        public Nullable<int> ThekkaBhuktaniTypeId { get; set; }
        public string ThekkaName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Samjauta_Budget_Amount { get; set; }
        public string Dev_OrganisationName { get; set; }
        public string SamjautaDateThapAfter { get; set; }
        public string SamjautaDateThapBefore { get; set; }
        public string DateThap { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Remaining_Budget { get; set; }
        public string Kafiyat { get; set; }
        public string SamjhautaMiti { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [ForeignKey("ThekkaSamjautaId")]
        public virtual ThekkaSamjhauta ThekkaSamjauta { get; set; }
        [ForeignKey("ThekkaBhuktaniTypeId")]
        public virtual ThekkaBhuktaniType ThekkaBhuktaniType { get; set; }
    }
    public class ThekkaBhuktaniMyadThap
    {
        [Key]
        public int ThekkaBhuktaniMyadThapId { get; set; }
        public Nullable<int> ThekkaBhuktaniId { get; set; }
        public string MyadThapMiti { get; set; }
        [ForeignKey("ThekkaBhuktaniId")]
        public virtual ThekkaBhuktani ThekkaBhuktani { get; set; }
    }

}
