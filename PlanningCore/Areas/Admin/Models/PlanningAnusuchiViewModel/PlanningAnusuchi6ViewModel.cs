namespace SDIMS.Areas.Planning.Models.PlanningAnusuchiViewModel
{
    public class PlanningAnusuchi6ViewModel
    {
        public int Anusuchi6Id { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        public string ProjectName { get; set; }
        public string WardNo { get; set; }
        public string StreetName { get; set; }
        public string UpabhoktaSamitiName { get; set; }
        public string Adakshya { get; set; }
        public string Sachib { get; set; }
        public decimal? AnudanRakam { get; set; }
        public decimal? ChandaRakam { get; set; }
        public decimal? JanaSahabhagitaRakam { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalExpensesTillNow { get; set; }
        public Nullable<bool> Status { get; set; }

        public PlanningAnusuchi6JanasahabhagitaViewModel PlanningAnusuchi6Janasahabhagita { get; set; } = new PlanningAnusuchi6JanasahabhagitaViewModel();
        public PlanningAnusuhci6KaryalayaViewModel PlanningAnusuchi6Karyalaya { get; set; } = new PlanningAnusuhci6KaryalayaViewModel();
        public List<PlanningAnusuchi6SolutionViewModel> PlanningAnusuchi6SolutionList { get; set; } = new List<PlanningAnusuchi6SolutionViewModel>();
    }

    public class PlanningAnusuchi6JanasahabhagitaViewModel
    {
        public int Anusuchi6JanasahabhagitaId { get; set; }
        public Nullable<int> Anusuchi6Id { get; set; }
        public decimal? JanasahabhagitaAmount { get; set; }
        public decimal? SharamAmount { get; set; }
        public decimal? JinsiAmount { get; set; }
        public decimal? TechnicalReviewAmount { get; set; }
        public decimal? UpabhiktaDecisionAmount { get; set; }
        public decimal? KistaRakamDemand { get; set; }
        public string Field_Supervise_Decision { get; set; }
        public string Main_Expenses { get; set; }
    }
    public class PlanningAnusuhci6KaryalayaViewModel
    {
        public int Anusuchi6KaryalayaId { get; set; }
        public Nullable<int> Anusuchi6Id { get; set; }
        public decimal? Karyalaya_Amount { get; set; }
        public decimal? Nirman_Samagri_Amount { get; set; }
        public decimal? Dashya_Amount { get; set; }
        public decimal? Adashya_Amount { get; set; }
        public decimal? Others_Amount { get; set; }
        public decimal? Travel_Expenses_Amount { get; set; }
        public decimal? Technical_Supervise_Amount { get; set; }
        public decimal? Masalanda_Amount { get; set; }
    }
    public class PlanningAnusuchi6SolutionViewModel
    {
        public int Anusuchi6SolutionId { get; set; }
        public int? Anusuchi6Id { get; set; }
        public string Solutions { get; set; }
    }
}
