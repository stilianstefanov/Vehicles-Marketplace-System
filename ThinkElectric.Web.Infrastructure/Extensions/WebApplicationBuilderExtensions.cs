namespace ThinkElectric.Web.Infrastructure.Extensions;

using System.Reflection;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

using Data.Models;
using static Common.GeneralApplicationConstants;

public static class WebApplicationBuilderExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
    {
        Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);

        if (serviceAssembly == null)
        {
            throw new InvalidOperationException("Invalid service type provided!");
        }

        Type[] implementationTypes = serviceAssembly
            .GetTypes()
            .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
            .ToArray();

        foreach (Type implementationType in implementationTypes)
        {
            Type? interfaceType = implementationType
                .GetInterface($"I{implementationType.Name}");

            if (interfaceType == null)
            {
                throw new InvalidOperationException(
                    $"No interface is provided for the service with name: {implementationType.Name}");
            }

            services.AddScoped(interfaceType, implementationType);
        }
    }

    public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        IServiceProvider serviceProvider = serviceScope.ServiceProvider;

        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        Task.Run(async () =>
        {
            if (await roleManager.RoleExistsAsync(AdminRoleName))
            {
                return;
            }

            var role = new IdentityRole<Guid>(AdminRoleName);

            await roleManager.CreateAsync(role);

            var adminUser = await userManager.FindByEmailAsync(DevelopmentAdminEmail);

            await userManager.AddToRoleAsync(adminUser, AdminRoleName);

            await userManager.AddClaimAsync(adminUser, new Claim("cartId", "3226EE7E-6E28-4C7C-B338-9EA6DF852957"));

            await userManager.AddClaimAsync(adminUser,
                new Claim("FullName", $"{adminUser.FirstName} {adminUser.LastName}"));
        })
        .GetAwaiter()
        .GetResult();

        return app;
    }
}
