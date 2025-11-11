namespace PlanningCore.Models
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            list = new List<DashboardDataViewModel>();
        }
        public List<DashboardDataViewModel> list { get; set; }
        public class DashboardDataViewModel
        {
            public string Name { get; set; }
            public string Amount { get; set; }
            public string TotalYojana { get; set; }
            public string TotalSamjhauta { get; set; }
            public string NonSamjhauta { get; set; }
            public string TotalSamiti { get; set; }
            public string TotalTolBikashSamiti { get; set; }
            //thekka
            public string TotalThekkaAmount { get; set; }
            public string TotalThekka { get; set; }
            public string TotalThekkaSamjhauta { get; set; }
            public string TotalConsultant { get; set; }
            public string ThekkaNonSamjhauta { get; set; }

        }
        public class PieChartViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Amount { get; set; }

        }
    }
}
