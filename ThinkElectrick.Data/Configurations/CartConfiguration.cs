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

        cart = new Cart()
        {
            Id = Guid.Parse("3226EE7E-6E28-4C7C-B338-9EA6DF852957"),
            UserId = Guid.Parse("EC09BD2A-4C64-476D-9997-E732562B0AB1"),
        };

        carts.Add(cart);

        return carts;
    }
}
