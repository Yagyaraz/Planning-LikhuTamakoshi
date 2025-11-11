namespace SDIMS.Areas.Planning.Models.PlanningAnusuchiViewModel
{
    public class PlanningAnusuchi1ViewModel
    {
        public int Id { get; set; }
        public int PlanningSamjhautaId { get; set; }
        public string UpabhoktaSamitiName { get; set; }
        public string Adakshya { get; set; }
        public string Upadakshya { get; set; }
        public string Kosadakshya { get; set; }
        public string Sachib { get; set; }
        public string EstablishDate_Nep { get; set; }
        public DateTime EstablishDate_Eng { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public bool Status { get; set; }
     //   public List<PlanningAnusuchi1ViewModel> Anusuchi1ViewModelList { get; set; } = new List<PlanningAnusuchi1ViewModel>();

    }
}
