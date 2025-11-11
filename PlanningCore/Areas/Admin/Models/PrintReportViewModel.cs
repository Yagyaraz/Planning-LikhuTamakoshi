using System.ComponentModel;

namespace PlanningCore.Areas.Admin.Models
{
	public class PrintReportViewModel
	{
		public int Id { get; set; }
		public int PlanningSamjhautaId { get; set; }
		
		public string ReportName { get; set; }
		public string PrintContent { get; set; }
		[DisplayName("योजनाको नाम")]
		public string SamjhautaName { get; set; }
		public List<PrintReportViewModel> ReportList = new List<PrintReportViewModel>();

        public string DocPath { get; set; }
        public string DocTypeName { get; set; }
    }
}
