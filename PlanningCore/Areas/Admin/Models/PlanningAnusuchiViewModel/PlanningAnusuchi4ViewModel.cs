using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDIMS.Areas.Planning.Models.PlanningAnusuchiViewModel
{
    public class PlanningAnusuchi4ViewModel
    {
        public int Anusuchi4Id { get; set; }
        public int? PlanningSamjhautaId { get; set; }
        public string ProjectName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public string ProjectPlace { get; set; }
        public decimal? ProjectEstimatedAmount { get; set; }
        public int? FiscalYearId { get; set; }
        public string ProjectApprovalDate { get; set; }
        public string ProjectToFinishDate { get; set; }
        public string ProjectEndedDate { get; set; }
        public string SamitiExpensesApprovalDate { get; set; }
        public bool? Status { get; set; }
        public int? SamitiDetailId { get; set; }
        public string Adakshya { get; set; }
        public string SadasyaTotal { get; set; }
        public string FemaleSadasya { get; set; }
        public string MaleSadasya { get; set; }
        public decimal? FirstKista { get; set; }
        public decimal? SecondKista { get; set; }
        public decimal? ThirdKista { get; set; }
        public decimal? Janashram { get; set; }
        public decimal? BastugatSahayata { get; set; }
        public decimal? LagatSahabhagita { get; set; }
        public decimal? NagarpalikaAnudhan { get; set; }
        public decimal? UpabhoktaBitya { get; set; }
        public decimal? UpabhoktaJanasharamdan { get; set; }
        public decimal? SthaniyaSanstha { get; set; }
        public decimal? DartaNikaya { get; set; }
        public decimal? AnnyaSansthaSahayog { get; set; }
        public decimal? PradeshSarkar { get; set; }
        public decimal? SanghiyaSarkar { get; set; }
        public decimal? ChandaDaan { get; set; }
        public decimal? TotalIncome { get; set; }
        public List<PlanningAnusuchi4ExpenseViewModel> PlanningAnusuchi4ExpenseList { get; set; } = new List<PlanningAnusuchi4ExpenseViewModel>();
    }
    public class PlanningAnusuchi4ExpenseViewModel
    {
        public int Anusuchi4ExpenseId { get; set; }
        public int Anusuchi4ExpenseTypeId { get; set; }
        public int? Anusuchi4Id { get; set; }
        public string Jyalla { get; set; }
        public string NirmanSamagriKharid { get; set; }
        public string Dhuwani { get; set; }
        public string Bhada { get; set; }
        public string BewasthapanKharhca { get; set; }
    }
    public class PlanningAnusuchi4ExpenseTypeViewModel
    {
        public int Anusuchi4ExpenseTypeId { get; set; }
        public string ExpensesName { get; set; }
        public int? Anusuchi4Id { get; set; }
    }

    public class PlanningAnusuchi4IncomeViewModel
    {
        public int Anusuchi4IncomeId { get; set; }
        public int? Anusuchi4Id { get; set; }
        public decimal? FirstKista { get; set; }
        public decimal? SecondKista { get; set; }
        public decimal? ThirdKista { get; set; }
        public decimal? Janashram { get; set; }
        public decimal? BastugatSahayata { get; set; }
        public decimal? LagatSahabhagita { get; set; }
        public decimal? NagarpalikaAnudhan { get; set; }
        public decimal? UpabhoktaBitya { get; set; }
        public decimal? UpabhoktaJanasharamdan { get; set; }
        public decimal? SthaniyaSanstha { get; set; }
        public decimal? DartaNikaya { get; set; }
        public decimal? AnnyaSansthaSahayog { get; set; }
        public decimal? PradeshSarkar { get; set; }
        public decimal? SanghiyaSarkar { get; set; }
        public decimal? ChandaDaan { get; set; }
        public decimal? TotalIncome { get; set; }
    }
}
