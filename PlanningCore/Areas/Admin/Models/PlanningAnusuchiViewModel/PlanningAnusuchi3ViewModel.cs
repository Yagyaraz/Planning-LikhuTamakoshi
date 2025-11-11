namespace PlanningCore.Areas.Planning.Models.PlanningAnusuchiViewModel
{
    public class PlanningAnusuchi3ViewModel
    {
        public int Anusuchi3Id { get; set; }
        public int PlanningSamjhautaId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectPlace { get; set; }
        public decimal? EstimatedAmount { get; set; }
        public string ProjectStartDate_Nep { get; set; }
        public DateTime? ProjectStartDate_Eng { get; set; }
        public string ProjectEndDate_Nep { get; set; }
        public DateTime? ProjectEndDate_Eng { get; set; }
        public string UpabhoktaSamitiName { get; set; }
        public string AdakshyaName { get; set; }
        public string TotalMember { get; set; }
        public string TotalMaleNo { get; set; }
        public string TotalFemaleNo { get; set; }
        public string TotalBenificialNo { get; set; }
        public string RohabarName { get; set; }
        public string RohabarPostion { get; set; }
        public string RohabarDate { get; set; }
        public bool? Status { get; set; }

        public List<PlanningAnusuchi3IncomeViewModel> PlanningAnusuchi3IncomeList { get; set; } = new List<PlanningAnusuchi3IncomeViewModel>();
        public List<PlanningAnusuchi3BhuktaniViewModel> PlanningAnusuchi3BhuktaniList { get; set; } = new List<PlanningAnusuchi3BhuktaniViewModel>();
        public List<PlanningAnusuchi3ExpensesViewModel> PlanningAnusuchi3ExpensesList { get; set; } = new List<PlanningAnusuchi3ExpensesViewModel>();
        public List<PlanningAnusuchi3MaujatViewModel> PlanningAnusuchi3MaujatList { get; set; } = new List<PlanningAnusuchi3MaujatViewModel>();
        public List<PlanningAnusuchi3ProjectWorkDetail> PlanningAnusuchi3ProjectWorkDetailList { get; set; } = new List<PlanningAnusuchi3ProjectWorkDetail>();
        public List<PlanningAnusuchi3WorkDivisionViewModel> PlanningAnusuchi3WorkDivisionList { get; set; } = new List<PlanningAnusuchi3WorkDivisionViewModel>();
        public List<PlanningAnusuchi3MemberViewModel> PlanningAnusuchi3MemberList { get; set; } = new List<PlanningAnusuchi3MemberViewModel>();
    }
    public class PlanningAnusuchi3IncomeViewModel
    {
        public int Anusuchi3IncomeId { get; set; }
        public string IncomeSource { get; set; }
        public decimal? AmountQuantity { get; set; }
        public string IncomeRemarks { get; set; }
    }
    public class PlanningAnusuchi3BhuktaniViewModel
    {
        public int Anusuchi3BhuktaniId { get; set; }
        public int? Anusuchi3Id { get; set; }
        public string BhuktaniDetail { get; set; }
        public decimal? BhuktaniAmount { get; set; }
    }
    public class PlanningAnusuchi3ExpensesTypeViewModel
    {
        public int Anusuchi3ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; }
    }
    public class PlanningAnusuchi3ExpensesViewModel
    {
        public int AnuSuchi3ExpensesId { get; set; }
        public Nullable<int> Anusuchi3ExpenseTypeId { get; set; }
        public string Anusuchi3ExpenseTypeName { get; set; }
        public Nullable<int> Anusuchi3Id { get; set; }
        public string ExpensesDetails { get; set; }
        public string ExpensesRate { get; set; }
        public string ExpensesQuantity { get; set; }
        public string ExpensesTotal { get; set; }
    //    public Nullable<int> ExpenseTypeId { get; set; }
   
    }
    public class PlanningAnusuchi3MaujatViewModel
    {
        public int Anusuchi3MaujatId { get; set; }
        public int? Anusuchi3Id { get; set; }
        public string MaujatDetail { get; set; }
        public decimal? MaujatAmount { get; set; }
        public string MaujatRemarks { get; set; }
    }
    public class PlanningAnusuchi3ProjectWorkDetail
    {
        public int Anusuchi3ProjectWorkDetailId { get; set; }
        public int? Anusuchi3Id { get; set; }
        public string WorkDetail { get; set; }
        public string WorkPlan { get; set; }
        public string WorkProgress { get; set; }
    }
    public class PlanningAnusuchi3WorkDivisionViewModel
    {
        public int Anusuchi3WorkDivisionId { get; set; }
        public int? Anusuchi3Id { get; set; }
        public int? PadaId { get; set; }
        public string MemberName { get; set; }
        public string PadaName { get; set; }
        public string WorkDescription { get; set; }
        public string Kaifiyat { get; set; }    
    }
    public class PlanningAnusuchi3MemberViewModel
    {
        public int Anusuchi3MemberId { get; set; }
        public int?Anusuchi3Id { get; set; }
        public string MemberName { get; set; }

    }

}
