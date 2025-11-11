using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanningCore.Data
{
    public class PrintReport
    {
        [Key]
        public int Id { get; set; }
        public int PlanningSamjhautaId { get; set; }
        public string ReportName { get; set; }  
        public string PrintContent { get; set; }  
        public string CreatedBy { get; set; }  
        public DateTime CreatedDate { get; set; }
        [ForeignKey("PlanningSamjhautaId")]
        public virtual PlanningSamjhauta PlanningSamjhauta { get; set; }

    }
    public class PrintReportUpabhokta
    {
        [Key]
        public int Id { get; set; }
        public int UpabhoktaSamitiDetailId { get; set; }
        public string ReportName { get; set; }
        public string PrintContent { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }

}
