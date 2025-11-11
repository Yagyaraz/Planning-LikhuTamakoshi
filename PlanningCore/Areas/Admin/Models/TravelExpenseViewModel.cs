using PlanningCore.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanningCore.Areas.Admin.Models
{
    public class TravelExpenseViewModel
    {
        public int Id { get; set; }
        [Display(Name ="कर्मचारीको नाम")]
        public string EmployeeName { get; set; }
        [Display(Name = "पद")]
        public int PostId { get; set; }
        [Display(Name = "भ्रमण गर्ने स्थान")]
        public string Location { get; set; }
        [Display(Name = "भ्रमणको उदेश्य")]
        public string Reason { get; set; }
        [Display(Name = "मिति देखि")]
        public string TravelFrom { get; set; }
        [Display(Name = "मिति सम्म")]
        public string TravelTo { get; set; }
        [Display(Name = "भ्रमण गर्ने साधन")]
        public string vehicle { get; set; }
        [Display(Name = "पेश्कि")]
        public string Advance { get; set; }
        [Display(Name = "बजेट नम्बर")]
        public string BudgetNumber { get; set; }
        [Display(Name = "चेक नम्बर")]
        public string ChequeNumber { get; set; }
        [Display(Name = "रकम")]
        public string Amount { get; set; }
        public string CreatedBy { get; set; } 
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        [Display(Name = "पद")]
        public string PostName { get; set; }
        public List<TravelRequiredItemsViewModel> TravelExpenseList { get; set; } = new List<TravelRequiredItemsViewModel>();
    }
    public class TravelRequiredItemsViewModel
    {
        
        public int TravelRequiredItemId { get; set; }
        public int TeavelExpenseId { get; set; }
        public string ItemNames { get; set; }
    }
}
