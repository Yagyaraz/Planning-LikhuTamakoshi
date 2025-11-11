using System.ComponentModel.DataAnnotations;

namespace PlanningCore.Areas.Admin.Models
{
    public class FormatViewModel
    {
        public string Id { get; set; }
        public string PlanningSamjhuataId {  get; set; }
        //Tolbikashmarfat
        public string Upakaran { get; set; }
        public string Aabadhi {  get; set; }    
        public PlanningSamjhautaViewModel PlanningSamjhauta { get; set; } = new PlanningSamjhautaViewModel();
        public UpavoktaSamitiDetailViewModel UpavoktaSamiti { get; set; } = new UpavoktaSamitiDetailViewModel();

    }
    public class YojanaKaryakramChecklistViewModel()
    {
        public int Id { get; set; }
        public int PlanningSamjhuataId { get; set; }
        public bool Bhelabata { get; set; }
        public bool MahilaPratinidhi { get; set; }
        public bool FemalePercentage { get; set; }
        public bool Rahobhar { get; set; }
        public bool KanunBamojim { get; set; }
        public bool AtleastTwoFemale { get; set; }
        public bool AnugamanSamiti { get; set; }
        public bool GathanNirnaya { get; set; }
        public bool Citizenship { get; set; }
        public bool LagatAnuman { get; set; }
        public bool PhotoOfworkingArea { get; set; }
        public bool FarfarakBaki { get; set; }
        public bool EkpariwarKobadiSadshya { get; set; }
        public bool WardSifarish { get; set; }
    }
}
