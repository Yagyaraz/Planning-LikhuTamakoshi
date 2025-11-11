namespace SDIMS.Areas.Planning.Models.PlanningAnusuchiViewModel
{
    public class PlanningAnusuchi7ViewModel
    {
        public int Anusuchi7Id { get; set; }
        public Nullable<int> PlanningSamjhautaId { get; set; }
        public Nullable<System.DateTime> Samiti_Estd_Date { get; set; }
        public string Samiti_Address { get; set; }
        public string Established_Type { get; set; }
        public string Adakshya { get; set; }
        public string Total_Present_No { get; set; }
        public Nullable<bool> Status { get; set; }
        public List<PlanningAnusuchi7UpabhoktaSamitiViewModel> PlanningAnusuchi7UpabhoktaSamitiList { get; set; } = new List<PlanningAnusuchi7UpabhoktaSamitiViewModel>(); 
        public List<PlanningAnusuchi7AnugamanViewModel> PlanningAnusuchi7AnugamanList { get; set; } = new List<PlanningAnusuchi7AnugamanViewModel>(); 
    }
    public class PlanningAnusuchi7UpabhoktaSamitiViewModel
    {
        public int Anusuchi7UpabhoktaSamitiId { get; set; }
        public Nullable<int> Anusuchi7Id { get; set; }
        public string Position { get; set; }
        public string MemberName { get; set; }
        public string Gender { get; set; }
        public string FatherHusbandName { get; set; }
        public string GrandFatherName { get; set; }
        public string MobileNo { get; set; }
    }
    public class PlanningAnusuchi7AnugamanViewModel
    {
        public int Anusuchi7AnugamanSamitiId { get; set; }
        public int? Anusuchi7Id { get; set; }
        public string Position { get; set; }
        public string MemberName { get; set; }
        public string Gender { get; set; }
        public string FatherHusbandName { get; set; }
        public string GrandFatherName { get; set; }
        public string MobileNo { get; set; }
    }
}
