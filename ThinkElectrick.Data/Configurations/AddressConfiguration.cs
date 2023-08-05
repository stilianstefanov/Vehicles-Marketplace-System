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

        builder.HasData(SeedAddresses());
    }

    private List<Address> SeedAddresses()
    {
        List<Address> addresses = new List<Address>();

        var address = new Address()
        {
            Id = Guid.Parse("B5C6E6D3-1423-4F03-821E-D4438909F0DD"),
            City = "Sofia",
            Street = "Vitosha",
            ZipCode = "1000",
            Country = "Bulgaria"
        };

        addresses.Add(address);

        address = new Address()
        {
            Id = Guid.Parse("283A2377-59AE-491E-B70F-CE5F2643E563"),
            City = "Varna",
            Street = "Levski",
            ZipCode = "9000",
            Country = "Bulgaria"
        };

        addresses.Add(address);

        return addresses;
    }
}
