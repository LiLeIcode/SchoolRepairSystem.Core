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
                //"admin,Ordinary",拆分处理
                //string[] role = value.Split(",");
                //foreach (string s in role)
                //{
                //    if (s == requirement.RoleName)
                //    {
                //        context.Succeed(requirement);
                //        break;
                //    }
                //}
                if (value == requirement.RoleName)
                    context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

    }
}