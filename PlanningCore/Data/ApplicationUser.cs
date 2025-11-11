using Microsoft.AspNetCore.Identity;

namespace PlanningCore.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public int? WardId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SubDepartmentId { get; set; }
        public int? CounterId { get; set; }
        public int? EmployeeId { get; set; }
        public string Role { get; set; }
        public string Post { get; set; }
    }
}
