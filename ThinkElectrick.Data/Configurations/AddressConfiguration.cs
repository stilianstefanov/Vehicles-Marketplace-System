namespace ThinkElectric.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasOne(a => a.User)
            .WithOne(u => u.Address)
            .HasForeignKey<ApplicationUser>(u => u.AddressId)
            .IsRequired(false);

        builder.HasOne(a => a.Company)
            .WithOne(c => c.Address)
            .HasForeignKey<Company>(c => c.AddressId)
            .IsRequired(false);
    }
}
