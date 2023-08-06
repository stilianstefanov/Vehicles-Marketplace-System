namespace ThinkElectric.Web.Infrastructure.Helpers;

using Microsoft.AspNetCore.Authorization;

using static Common.GeneralApplicationConstants;

public class CompanyOrAdminAuthorizationHandler : AuthorizationHandler<CompanyOrAdminRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CompanyOrAdminRequirement requirement)
    {
        if (context.User.IsInRole(AdminRoleName) || context.User.HasClaim(c => c.Type == "companyId"))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
