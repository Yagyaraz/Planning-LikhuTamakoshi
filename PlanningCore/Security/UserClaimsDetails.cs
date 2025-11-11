using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PlanningCore.Data;
using System.Security.Claims;

namespace PlanningCore.Security
{
    public class UserClaimsDetails : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        private readonly PlanningContext _context;
        public UserClaimsDetails(UserManager<ApplicationUser> userManager
        , RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options, PlanningContext context)
        : base(userManager, roleManager, options)
        {
            _context = context;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            //var aa = identity.RoleClaimType
            identity.AddClaim(new Claim("FullName", user.FullName ?? ""));
            identity.AddClaim(new Claim("Post", user.Post ?? ""));
            var IsRolePresent = _context.UserRoles.Any(x => x.UserId == user.Id);
            identity.AddClaim(new Claim("IsRolePresent", IsRolePresent.ToString()));

            return identity;
        }
    }
}
