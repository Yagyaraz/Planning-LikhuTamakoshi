using Microsoft.AspNetCore.Routing.Constraints;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanningCore.Data
{
    public class MainSetting
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string LogoPath { get; set; } = "/img/logo.png";
        public string FaxNo { get; set; }
        public string Website { get; set; }
        public int PalikaType { get; set; }
        public DateTime? ExpireDate { get; set; }
        public int? StateId { get; set; }
        public int? DistrictId { get; set; }
        public int? PalikaId { get; set; }
        public bool EnglishLetterHead { get; set; }

        [ForeignKey(nameof(StateId))]
        public State State { get; set; }

        [ForeignKey(nameof(DistrictId))]
        public District District { get; set; }
        [ForeignKey(nameof(PalikaId))]
        public Palika Palika { get; set; }


    }
}
