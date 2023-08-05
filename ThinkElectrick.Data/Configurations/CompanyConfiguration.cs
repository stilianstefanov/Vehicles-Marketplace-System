namespace ThinkElectric.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasOne(c => c.User)
            .WithOne(u => u.Company)
            .HasForeignKey<Company>(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(SeedCompanies());
    }

    private List<Company> SeedCompanies()
    {
        List<Company> companies = new List<Company>();

        var company = new Company()
        {
            Id = Guid.Parse("C0781351-133E-4383-81E7-C95E20FA1273"),
            Name = "Vistaz",
            Email = "vistaz@abv.bg",
            PhoneNumber = "0888888888",
            Website = "https://vistazExample.bg/",
            Description = "Vistaz is a company that sells electric scooters and bikes. Really good quality!",
            FoundedDate = DateTime.Parse("2020-01-01"),
            UserId = Guid.Parse("95F65567-3392-47FD-AE6A-95D16DFDBFE2"),
            AddressId = Guid.Parse("B5C6E6D3-1423-4F03-821E-D4438909F0DD"),
            ImageId = "64ce4237f1bda0c4b930c421"
        };

        companies.Add(company);

        company = new Company()
        {
            Id = Guid.Parse("5D652EBF-7B4D-430C-9AF8-0C02B0256575"),
            Name = "Green Company",
            Email = "GreenCompany@abv.bg",
            PhoneNumber = "0777777777",
            Website = "https://GreenCompanyExample.bg/",
            Description =
                "Green Company is a company that sells high quality electric scooters bikes and accessories. Enjoy our products!",
            FoundedDate = DateTime.Parse("2019-01-01"),
            UserId = Guid.Parse("553D4978-B893-4670-8808-3C91D6165C82"),
            AddressId = Guid.Parse("283A2377-59AE-491E-B70F-CE5F2643E563"),
            ImageId = "64ce53d31dee708ccae9319d"
        };

        companies.Add(company);

        return companies;
    }
}
