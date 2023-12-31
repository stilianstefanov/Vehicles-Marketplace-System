namespace ThinkElectric.Web;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

using Data;
using Data.Models;
using Data.MongoDb;
using Infrastructure.Extensions;
using Infrastructure.ModelBinders;
using Services.Contracts;
using Data.MongoDb.Models;
using Infrastructure.Helpers;

using Services;
using static Common.GeneralApplicationConstants;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<ThinkElectricDbContext>(options =>
            options.UseSqlServer(connectionString));


        builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
                options.Password.RequireDigit = builder.Configuration.GetValue<bool>("Identity:Password:RequireDigit");
                options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
                options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ThinkElectricDbContext>();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = $"/User/Login";
            options.LogoutPath = $"/User/Logout";
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(CompanyOnlyPolicyName, policy =>
            {
                policy.RequireClaim("companyId");
            });

            options.AddPolicy(BuyerOrAdminPolicyName, policy =>
            {
                policy.RequireClaim("cartId");
            });

            options.AddPolicy(CompanyOrAdminPolicyName, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(new CompanyOrAdminRequirement());
            });
        });

        builder.Services.AddScoped<IAuthorizationHandler, CompanyOrAdminAuthorizationHandler>();

        builder.Services.Configure<ImageStoreDatabaseSettings>(
            builder.Configuration.GetSection("ImageStoreDatabase"));

        builder.Services.AddScoped<UserManager<ApplicationUser>>();
        builder.Services.AddScoped<SignInManager<ApplicationUser>>();

        builder.Services.AddApplicationServices(typeof(IProductService));
        builder.Services.AddSingleton<IImageService, ImageService>();


        builder.Services.AddControllersWithViews()
            .AddMvcOptions(options =>
            {
                options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

        var app = builder.Build();

        SeedMongoDbData(builder.Configuration);

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error/500");
            app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.SeedAdministrator();

        app.UseEndpoints(config =>
        {
            config.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );
            config.MapDefaultControllerRoute();
            config.MapRazorPages();
        });

        app.Run();
    }

    private static void SeedMongoDbData(IConfiguration configuration)
    {
        var mongoDbSettings = configuration.GetSection("ImageStoreDatabase").Get<ImageStoreDatabaseSettings>();

        var seeder = new ImageSeeder(Options.Create(mongoDbSettings));

        seeder.SeedData();
    }
}
