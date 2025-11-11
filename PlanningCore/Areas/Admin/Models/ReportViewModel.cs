namespace PlanningCore.Areas.Admin.Models
{
    public class BhuktaniReportViewModel
    {
        public string ProjectName { get; set; }
        public string EstimatedAmount { get; set; }
        public string MinicipalityAmount { get; set; }
        public string Janasahabhagita { get; set; }
        public string Peski { get; set; }
        public string wardId { get; set; }
        public int BhuktaniTypeId { get; set; }
        public string BhuktaniTypeName { get; set; }
    }

    public class RepotSamjhutaViewModel
    {
        public string FiscalName { get; set; }
        public string BudgetName { get; set; }
        public string YojanaName { get; set; }
        public string WardName { get; set; }
        public string YojanaAddress { get; set; }
        public string SamitiName { get; set; }
        public string StartMiti { get; set; }
        public string EndMiti { get; set; }
        public decimal BinyojitRkm { get; set; }
        public decimal LagatRkm { get; set; }
    }

    public class RepotInCompleteSamjhutaViewModel
    {
        public string FiscalName { get; set; }
        public string BudgetName { get; set; }
        public string YojanaName { get; set; }
        public string WardName { get; set; }
        public string YojanaAddress { get; set; }
        public decimal BinyojitRkm { get; set; }
    }

    public class RepotCompleteSamjhutaViewModel
    {
        public string FiscalName { get; set; }
        public string BudgetName { get; set; }
        public string YojanaName { get; set; }
        public string WardName { get; set; }
        public string YojanaAddress { get; set; }
        public decimal BinyojitRkm { get; set; }
        public string EndMiti { get; set; }
    }

    public class RepotBhotikPratiwadanViewModel
    {
        public string FiscalName { get; set; }
        public string BudgetName { get; set; }
        public string YojanaName { get; set; }
        public string WardName { get; set; }
        public string YojanaAddress { get; set; }
        public string SamitiName { get; set; }
        public decimal BinyojitRkm { get; set; }
        public decimal LagatRkm { get; set; }
        public string SamjhutaMiti { get; set; }
        public string EndMiti { get; set; }
        public string Bhotik { get; set; }
        public string Remarks { get; set; }
    }
    public class RepotBitiyaPratiwadanViewModel
    {
        public string FiscalName { get; set; }
        public string BudgetName { get; set; }
        public string YojanaName { get; set; }
        public string WardName { get; set; }
        public string YojanaAddress { get; set; }
        public decimal LagatRkm { get; set; }
        public decimal PaskiRkm { get; set; }
        public decimal FirstKistRkm { get; set; }
        public decimal SecoundKistRkm { get; set; }
        public decimal ThirdKistRkm { get; set; }
        public string Bhotik { get; set; }
        public string Remarks { get; set; }
    }
    public class RepotTotalBhukataniViewModel
    {
        public string FiscalName { get; set; }
        public string BudgetName { get; set; }
        public string YojanaName { get; set; }
        public string WardName { get; set; }
        public decimal BinyojitRkm { get; set; }
        public decimal LagatRkm { get; set; }
        public decimal Contingency { get; set; }
        public decimal JanShabhagita { get; set; }
        public decimal SamjhutaRkm { get; set; }
        public decimal PrabidhikRkm { get; set; }
        public decimal KhudJanShabhagita { get; set; }
        public decimal RoyaltyRkm { get; set; }
        public decimal Other { get; set; }
        public decimal TotalKattiRkm { get; set; }
        public decimal KhudKistaRkm { get; set; }
        public string Remarks { get; set; }
    }
    public class RepotWardWiseViewModel
    {
        public string FiscalName { get; set; }
        public string BudgetName { get; set; }
        public string YojanaName { get; set; }
        public string WardName { get; set; }
        public string YojanaAddress { get; set; }
        public decimal LagatRkm { get; set; }
    }
    public class RepotWorkWiseViewModel
    {
        public string FiscalName { get; set; }
        public string BudgetName { get; set; }
        public string YojanaName { get; set; }
        public string WardName { get; set; }
        public string Unit { get; set; }
        public string SamitiName { get; set; }
        public string EndMiti { get; set; }
    }
}
