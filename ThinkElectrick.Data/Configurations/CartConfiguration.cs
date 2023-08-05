namespace ThinkElectric.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasOne(c => c.User)
            .WithOne(u => u.Cart)
            .HasForeignKey<Cart>(c => c.UserId);

        builder.HasData(SeedCarts());
    }

    private List<Cart> SeedCarts()
    {
        var carts = new List<Cart>();

        var cart = new Cart()
        {
            Id = Guid.Parse("D6271C21-CAA6-46A3-8776-D3152F6A1432"),
            UserId = Guid.Parse("A44DAD0E-BD5F-4394-9F8D-AD5A31E5C24B"),
        };

        carts.Add(cart);

        return carts;
    }
}
