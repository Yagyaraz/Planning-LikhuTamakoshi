using System.ComponentModel.DataAnnotations;

namespace PlanningCore.Areas.Admin.Models
{
    public class AnugamanPartibedanViewModel
    {

        public int Id { get; set; }
        public int PlanningSamjhautaId { get; set; }
        public string ParikxanDate { get; set; }
        public string SampannaType { get; set; }
        public string BillRakam { get; set; }
        public bool IsNirnayaBhayako { get; set; }
        public bool IsBibad { get; set; }
        public bool IsGambirBibad { get; set; }
        public string BibadDetails { get; set; }
        public string BahiyaJokhim { get; set; }
        public bool IsJimmewari { get; set; }
        public string JimmwariDetails { get; set; }
        public bool IsBankAccountOppen { get; set; }
        public string BankAcocuntDetail { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string AccountRemarks { get; set; }
        public bool PratakshyaFaida { get; set; }
        public string GharDhuri { get; set; }
        public string PratakshyaFaidaDetails { get; set; }
        public bool IsSuchanaPati { get; set; }
        public bool IsAambhela { get; set; }
        public string EngineerName { get; set; }
        public decimal? BhuktaniSifarishRakam { get; set; }
        public string KaryanayanDetails { get; set; }
        public string SamasyaSamadhanUpaya { get; set; }
        public string AnugamanBibaran { get; set; }
        public string BeforePhotos { get; set; }
        public string AfterPhotos { get; set; }
        public string BetweenPhotos { get; set; }
        public IFormFile BeforePhotosImg { get; set; }
        public IFormFile AfterPhotosImg { get; set; }
        public IFormFile BetweenPhotosImg { get; set; }
        public string KhataSanchalak1Name { get; set; }
        public string KhataSanchalak1Post { get; set; }
        public string KhataSanchalak2Post { get; set; }
        public string KhataSanchalak3Post { get; set; }
        public string KhataSanchalak1Gender { get; set; }
        public string KhataSanchalak1Remarks { get; set; }
        public string KhataSanchalak2Name { get; set; }
        public string KhataSanchalak2Gender { get; set; }
        public string KhataSanchalak2Remarks { get; set; }
        public string KhataSanchalak3Name { get; set; }
        public string KhataSanchalak3Gender { get; set; }
        public string KhataSanchalak3Remarks { get; set; }
        public string AnugamanKarta1Name { get; set; }
        public string AnugamanKarta1Post { get; set; }
        public string AnugamanKarta1Remarks { get; set; }

        public string AnugamanKarta2Name { get; set; }
        public string AnugamanKarta2Post { get; set; }
        public string AnugamanKarta2Remarks { get; set; }

        public string AnugamanKarta3Name { get; set; }
        public string AnugamanKarta3Post { get; set; }
        public string AnugamanKarta3Remarks { get; set; }

        public string AnugamanKarta4Name { get; set; }
        public string AnugamanKarta4Post { get; set; }
        public string AnugamanKarta4Remarks { get; set; }

        public string AnugamanKarta5Name { get; set; }
        public string AnugamanKarta5Post { get; set; }
        public string AnugamanKarta5Remarks { get; set; }
        public PlanningSamjhautaViewModel PlanningSamjhauta {  get; set; }=new PlanningSamjhautaViewModel();
    }

}

