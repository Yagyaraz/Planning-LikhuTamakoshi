namespace SDIMS.Areas.Planning.Models.PlanningAnusuchiViewModel
{
    public class PlanningAnusuchi5ViewModel
    {
        public int AnuSuchi5Id { get; set; }
        public int? PlanningSamjhautaId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectRunOrg { get; set; }
        public string UpabhoktaSamitiAdakshya { get; set; }
        public decimal? ProjectEstimateAmount { get; set; }
        public decimal? MunicipalityAmount { get; set; }
        public decimal? JanaSahabhagitaAmount { get; set; }
        public decimal? OtherOrgSourceAmount { get; set; }
        public string ProjectContractDate { get; set; }
        public string ProjectEndDate { get; set; }
        public string TotalProjectBeneficiaries { get; set; }
        public bool? Status { get; set; }
    }
}
