namespace ThinkElectric.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.CreatedOn)
            .HasDefaultValue(DateTime.UtcNow);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        builder.HasMany(p => p.Reviews)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.OrderItems)
            .WithOne(oi => oi.Product)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.CartItems)
            .WithOne(ci => ci.Product)
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(p => p.Scooter)
            .WithOne(s => s.Product)
            .HasForeignKey<Scooter>(s => s.ProductId)
            .IsRequired(false);

        builder.HasOne(p => p.Bike)
            .WithOne(b => b.Product)
            .HasForeignKey<Bike>(b => b.ProductId)
            .IsRequired(false);

        builder.HasOne(p => p.Accessory)
            .WithOne(a => a.Product)
            .HasForeignKey<Accessory>(a => a.ProductId)
            .IsRequired(false);
    }
}
