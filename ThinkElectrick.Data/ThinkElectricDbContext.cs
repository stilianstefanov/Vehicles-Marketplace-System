namespace ThinkElectric.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Configurations;
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

    public DbSet<Post> Posts { get; set; } = null!;

    public DbSet<Comment> Comments { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new CartConfiguration());
        builder.ApplyConfiguration(new AddressConfiguration());
        builder.ApplyConfiguration(new CompanyConfiguration());
        builder.ApplyConfiguration(new ProductEntityConfiguration());
        builder.ApplyConfiguration(new ScooterConfiguration());
        builder.ApplyConfiguration(new BikeConfiguration());
        builder.ApplyConfiguration(new AccessoryConfiguration());
        builder.ApplyConfiguration(new PostConfiguration());

        base.OnModelCreating(builder);
    }
}
