namespace ThinkElectric.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Models;


public class ThinkElectricDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ThinkElectricDbContext(DbContextOptions<ThinkElectricDbContext> options)
        : base(options)
    {
    }

    public DbSet<Accessory> Accessories { get; set; } = null!;

    public DbSet<Address> Addresses { get; set; } = null!;

    public DbSet<Bike> Bikes { get; set; } = null!;

    public DbSet<Cart> Carts { get; set; } = null!;

    public DbSet<CartItem> CartItems { get; set; } = null!;

    public DbSet<Company> Companies { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;

    public DbSet<OrderItem> OrderItems { get; set; } = null!;

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<Review> Reviews { get; set; } = null!;

    public DbSet<Scooter> Scooters { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder builder)
    {
        var configAssembly = Assembly.GetAssembly(typeof(ThinkElectricDbContext)) ??
                             Assembly.GetExecutingAssembly();

        builder.ApplyConfigurationsFromAssembly(configAssembly);

        base.OnModelCreating(builder);
    }
}
