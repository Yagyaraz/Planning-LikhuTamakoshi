using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanningCore.Data
{
    public class TeavelExpense
    {
        [Key]
        public  int  Id { get; set; }
        public string EmployeeName { get; set; }
        public int   PostId { get; set; }
        public string Location { get; set; }
        public string Reason { get; set; }
        public string TravelFrom { get; set; }
        public string TravelTo { get; set; }
        public string vehicle { get; set; }
        public string Advance { get; set; }
        public string BudgetNumber { get; set; }
        public string ChequeNumber { get; set; }
        public string Amount { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        [ForeignKey("PostId")]
        public  Pada Pada { get; set; }

    }
    public class TravelRequiredItems
    {
        [Key]
        public int Id { get; set; }
        public int TeavelExpenseId { get; set; }
        public string ItemNames { get; set; }

        [ForeignKey("TeavelExpenseId")]
        public TeavelExpense TeavelExpense { get; set; }
        
    }
}
