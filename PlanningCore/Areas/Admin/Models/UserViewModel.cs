using System.ComponentModel.DataAnnotations;

namespace PlanningCore.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "नाम")]
        public string Name { get; set; }
        [Display(Name = "ई-मेल")]
        public string UserName { get; set; }
        [Display(Name = "रोल")]
        public string Role { get; set; }
        [Display(Name = "वडा")]
        public int? WardId { get; set; }
        [Display(Name = "शाखा")]        
        public string WardName { get; set; }    
        [Display(Name = "अवस्था")]
        public bool Active { get; set; }
    }
}
