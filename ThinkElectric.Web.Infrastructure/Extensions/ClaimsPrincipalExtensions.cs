namespace ThinkElectric.Web.Infrastructure.Extensions;

using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static string? GetId(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public static string? GetCompanyId(this ClaimsPrincipal user)
    {
        return user.FindFirst("companyId")?.Value;
    }
}
