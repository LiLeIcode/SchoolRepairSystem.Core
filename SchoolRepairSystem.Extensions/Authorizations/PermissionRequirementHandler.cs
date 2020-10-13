using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SchoolRepairSystem.Common.Helper;

namespace SchoolRepairSystem.Extensions.Authorizations
{
    public class PermissionRequirementHandler:AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            ClaimsPrincipal principal = context.User;
            Claim claim = principal.FindFirst(x => x.Type == ClaimTypes.Role);
            if (claim!=null)
            {
                string value = claim.Value;
                if (value==requirement.RoleName)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }

    }
}