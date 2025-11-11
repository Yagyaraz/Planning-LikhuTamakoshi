using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PlanningCore.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly ILogger<object> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string userId;
        public new AuthorizationPolicy Policy { get; }

        public CustomAuthorizeAttribute(ILogger<object> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        }


        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var httpContext = context.HttpContext;
            bool isAuthorized = httpContext.User.Identity.IsAuthenticated;

            var area = context.RouteData.Values["area"];
            var controller = context.RouteData.Values["controller"] ?? "";
            var action = context.RouteData.Values["action"];

            _logger.LogInformation("User Log Date and Time : " + DateTime.UtcNow + " User Id : " + userId + " Url : " + _httpContextAccessor.HttpContext.Request.Host + _httpContextAccessor.HttpContext.Request.Path);
            // If Controller or Action AllowAnonymous
            if (HasAllowAnonymous(context))
                return;

            var policyEvaluator = context.HttpContext.RequestServices.GetRequiredService<IPolicyEvaluator>();
            var authenticateResult = await policyEvaluator.AuthenticateAsync(Policy, context.HttpContext);
            var authorizeResult = await policyEvaluator.AuthorizeAsync(Policy, authenticateResult, context.HttpContext, context);

            var result = new UnauthorizedResult();

            if (isAuthorized)
            {
                if (httpContext.User.IsInRole(UserRoles.Administrator) || httpContext.User.IsInRole(UserRoles.SuperAdmin) || httpContext.User.IsInRole(UserRoles.User))
                {
                    return;
                }

                if (area != null)
                {
                    if (area.Equals("Identity"))
                        return;
                    if (httpContext.User.IsInRole(UserRoles.User))
                    {
                        context.Result = result;
                    }
                }
                return;
            }
            else
            {
                context.Result = new RedirectResult("/Identity/Account/Login");
                //  context.Result = new RedirectResult("/LandingPage/Index");
            }
            return;
        }
        private static bool HasAllowAnonymous(AuthorizationFilterContext context)
        {
            if (context.Filters.Any(x => x is IAllowAnonymousFilter))
                return true;

            // When doing endpoint routing, MVC does not add AllowAnonymousFilters for AllowAnonymousAttributes that
            // were discovered on controllers and actions. To maintain compat with 2.x,
            // we'll check for the presence of IAllowAnonymous in endpoint metadata.
            var endpoint = context.HttpContext.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return true;
            }

            return false;
        }

        //private static void Redirect(AuthorizationFilterContext context)
        //{
        //    context.Result = new RedirectResult("/Identity/Account/Login");
        //}
    }
}
