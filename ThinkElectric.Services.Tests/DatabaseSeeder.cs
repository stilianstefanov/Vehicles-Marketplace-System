namespace ThinkElectric.Services.Tests;

using Data;
using Data.Models;
using Data.Models.Enums.Product;

public static class DatabaseSeeder
{
    public static ApplicationUser CompanyUser;

    public static Address TestAddress;

    public static Company TestCompany;

    public static Product TestProduct;

    public static Product TestProduct2;

    public static Product TestProduct3;

    public static void SeedDatabase(ThinkElectricDbContext _dbContext)
    {
        CompanyUser = new ApplicationUser()
        {
            UserName = "testUser@abv.bg",
            Email = "testUser@abv.bg",
            NormalizedUserName = "TESTUSER@ABV.BG",
            NormalizedEmail = "TESTUSER@ABV.BG",
            FirstName = "Test",
            LastName = "Testov",
            PhoneNumber = "0888888888",
            PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
            ConcurrencyStamp = "caf271d7-0ba7-4ab1-8d8d-6d0e3711c27d",
            SecurityStamp = "ca32c787-626e-4234-a4e4-8c94d85a2b1c"
        };

        TestAddress = new Address()
        {
            Street = "TestStreet",
            City = "TestCity",
            Country = "TestCountry",
            ZipCode = "TestZipCode",
        };

        TestCompany = new Company()
        {
            Name = "Vistaz",
            Email = "vistaz@abv.bg",
            PhoneNumber = "0888888888",
            Website = "https://vistazExample.bg/",
            Description = "Vistaz is a company that sells electric scooters and bikes. Really good quality!",
            FoundedDate = DateTime.Parse("2020-01-01"),
            UserId = CompanyUser.Id,
            AddressId = TestAddress.Id,
            ImageId = "64ce4237f1bda0c4b930c421"
        };

        TestProduct = new Product()
        {
            Name = "Test Scooter",
            Price = 4999.99M,
            Quantity = 10,
            ProductType = ProductType.Scooter,
            CreatedOn = new DateTime(2023, 7, 25, 8, 45, 25),
            CompanyId = TestCompany.Id,
            ImageId = "64ce5c39ad51218262de3f60"
        };

        TestProduct2 = new Product()
        {
            Name = "Test Bike",
            Price = 2999.99M,
            Quantity = 15,
            ProductType = ProductType.Bike,
            CreatedOn = new DateTime(2023, 7, 24, 8, 45, 25),
            CompanyId = TestCompany.Id,
            ImageId = "64ce5f7abd83b3123fc9ab71"
        };

        TestProduct3 = new Product()
        {
            Name = "Test Accessory",
            Price = 50.99M,
            Quantity = 35,
            ProductType = ProductType.Accessory,
            CreatedOn = new DateTime(2023, 3, 3, 8, 45, 25),
            CompanyId = TestCompany.Id,
            ImageId = "64ce7219abc83a6ff8f1da33"
        };

        _dbContext.Users.Add(CompanyUser);
        _dbContext.Addresses.Add(TestAddress);
        _dbContext.Companies.Add(TestCompany);
        _dbContext.Products.Add(TestProduct);
        _dbContext.Products.Add(TestProduct2);
        _dbContext.Products.Add(TestProduct3);

        _dbContext.SaveChanges();
    }
}
