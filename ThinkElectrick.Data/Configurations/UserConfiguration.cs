namespace ThinkElectric.Data.Configurations;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasData(SeedUsers());
    }

    private List<ApplicationUser> SeedUsers()
    {
        var users = new List<ApplicationUser>();

        var hasher = new PasswordHasher<ApplicationUser>();

        var user = new ApplicationUser()
        {
            Id = Guid.Parse("EC09BD2A-4C64-476D-9997-E732562B0AB1"),
            UserName = "admin@abv.bg",
            Email = "admin@abv.bg",
            NormalizedUserName = "ADMIN@ABV.BG",
            NormalizedEmail = "ADMIN@ABV.BG",
            FirstName = "Admin",
            LastName = "Admin",
            PhoneNumber = "0888888888",
        };

        user.PasswordHash = hasher.HashPassword(user, "123456");
        users.Add(user);

        user = new ApplicationUser()
        {
            Id = Guid.Parse("95F65567-3392-47FD-AE6A-95D16DFDBFE2"),
            UserName = "companyuser1@abv.bg",
            Email = "companyuser1@abv.bg",
            NormalizedUserName = "COMPANYUSER1@ABV.BG",
            NormalizedEmail = "COMPANYUSER1@ABV.BG",
            FirstName = "CompanyUser1",
            LastName = "CompanyUser1",
            PhoneNumber = "0999999999",
        };

        user.PasswordHash = hasher.HashPassword(user, "123456");
        users.Add(user);

        user = new ApplicationUser()
        {
            Id = Guid.Parse("553D4978-B893-4670-8808-3C91D6165C82"),
            UserName = "companyuser2@abv.bg",
            Email = "companyuser2@abv.bg",
            NormalizedUserName = "COMPANYUSER2@ABV.BG",
            NormalizedEmail = "COMPANYUSER2@ABV.BG",
            FirstName = "CompanyUser2",
            LastName = "CompanyUser2",
            PhoneNumber = "0777777777",
        };

        user.PasswordHash = hasher.HashPassword(user, "123456");
        users.Add(user);

        user = new ApplicationUser()
        {
            Id = Guid.Parse("A44DAD0E-BD5F-4394-9F8D-AD5A31E5C24B"),
            UserName = "buyeruser@abv.bg",
            Email = "buyeruser@abv.bg",
            NormalizedUserName = "BUYERUSER@ABV.BG",
            NormalizedEmail = "BUYERUSER@ABV.BG",
            FirstName = "BuyerUser",
            LastName = "BuyerUser",
            PhoneNumber = "0666666666",
        };

        user.PasswordHash = hasher.HashPassword(user, "123456");
        users.Add(user);

        return users;
    }
}
