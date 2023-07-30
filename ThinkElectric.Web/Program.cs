namespace ThinkElectric.Web;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Models;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ThinkElectric.Data.MongoDb.Models;

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
            .AddEntityFrameworkStores<ThinkElectricDbContext>();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = $"/User/Login";
            options.LogoutPath = $"/User/Logout";
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("CompanyOnly", policy =>
            {
                policy.RequireClaim("companyId");
            });

            options.AddPolicy("BuyerOnly", policy =>
            {
                policy.RequireClaim("cartId");
            });
        });

        builder.Services.Configure<ImageStoreDatabaseSettings>(
            builder.Configuration.GetSection("ImageStoreDatabase"));

        builder.Services.AddScoped<UserManager<ApplicationUser>>();
        builder.Services.AddScoped<SignInManager<ApplicationUser>>();

        builder.Services.AddApplicationServices(typeof(IProductService));


        builder.Services.AddControllersWithViews()
            .AddMvcOptions(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

        var app = builder.Build();

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

        app.UseEndpoints(config =>
        {
            config.MapDefaultControllerRoute();
            config.MapRazorPages();
        });

        app.Run();
    }
}
