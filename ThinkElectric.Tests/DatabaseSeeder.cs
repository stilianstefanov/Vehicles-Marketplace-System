namespace ThinkElectric.Tests;

using Data;
using Data.Models;
using Data.Models.Enums.Bike;
using Data.Models.Enums.Product;
using Data.Models.Enums.Scooter;

public static class DatabaseSeeder
{
    public static ApplicationUser CompanyUser;

    public static ApplicationUser BuyerUser;

    public static Cart TestCart;

    public static Address TestAddress;

    public static Company TestCompany;

    public static Product TestProduct;

    public static Product TestProduct2;

    public static Product TestProduct3;

    public static Scooter TestScooter;

    public static Bike TestBike;

    public static Accessory TestAccessory;

    public static Order TestOrder;

    public static OrderItem TestOrderItem1;

    public static OrderItem TestOrderItem2;

    public static CartItem TestCartItem;

    public static CartItem TestCartItem2;

    public static void SeedDatabase(ThinkElectricDbContext _dbContext)
    {
        CompanyUser = new ApplicationUser()
        {
            UserName = "testCompanyUser@abv.bg",
            Email = "testCompanyUser@abv.bg",
            NormalizedUserName = "TESTCOMPANYUSER@ABV.BG",
            NormalizedEmail = "TESTCOMPANYUSER@ABV.BG",
            FirstName = "Test",
            LastName = "Testov",
            PhoneNumber = "0888888888",
            PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
            ConcurrencyStamp = "caf271d7-0ba7-4ab1-8d8d-6d0e3711c27d",
            SecurityStamp = "ca32c787-626e-4234-a4e4-8c94d85a2b1c"
        };

        BuyerUser = new ApplicationUser()
        {
            UserName = "testBuyerUser@abv.bg",
            Email = "testBuyerUser@abv.bg",
            NormalizedUserName = "TESTBUYERUSER@ABV.BG",
            NormalizedEmail = "TESTBUYERUSER@ABV.BG",
            FirstName = "TestBuyer",
            LastName = "TestovBuyer",
            PhoneNumber = "0888888888",
            PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
            ConcurrencyStamp = "a67bc566-bbaa-4287-80df-eb9aaf278686",
            SecurityStamp = "75866877-5b49-4377-96e5-e269574d7e65"
        };

        TestCart = new Cart()
        {
            UserId = BuyerUser.Id,
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

        TestScooter = new Scooter()
        {
            Brand = "Test Scooter Brand",
            Model = "Test Scooter Model",
            Color = "Black",
            Battery = "Li-Ion 70V 35Ah",
            Type = ScooterType.OffRoad,
            EngineType = ScooterEngineType.DualMotor,
            BrakesType = ScooterBrakesType.Hydraulic,
            Range = 110,
            TopSpeed = 80,
            Weight = 46,
            MaxLoad = 150,
            TireSize = 11,
            ChargingTime = 12,
            EnginePower = 2400,
            ProductId = TestProduct.Id
        };

        TestBike = new Bike()
        {
            Brand = "Test Bike Brand",
            Model = "Test Bike Model",
            Color = "Black",
            FrameMaterial = "Aluminum",
            Battery = "48V 13Ah",
            Type = BikeType.City,
            FrameType = BikeFrameType.Foldable,
            SuspensionType = BikeSuspensionType.Front,
            BrakesType = BikeBrakesType.HydraulicBrakes,
            EngineType = BikeEngineType.Rear,
            FrameSize = 20,
            GearsCount = 7,
            Range = 60,
            TopSpeed = 25,
            Weight = 25,
            MaxLoad = 120,
            WheelSize = 15,
            ChargingTime = 6,
            EnginePower = 250,
            ProductId = TestProduct2.Id,
        };

        TestAccessory = new Accessory()
        {
            Brand = "Test Accessory Brand",
            Description = "Test Accessory Description!!!",
            CompatibleBrand = "Kaabo",
            CompatibleModel = "Mantis",
            ProductId = TestProduct3.Id
        };

        TestOrder = new Order()
        {
            UserId = BuyerUser.Id,
            CreatedOn = DateTime.Parse("2021-01-01"),
        };

        TestOrderItem1 = new OrderItem()
        {
            OrderId = TestOrder.Id,
            ProductId = TestProduct.Id,
            Quantity = 1,
        };

        TestOrderItem2 = new OrderItem()
        {
            OrderId = TestOrder.Id,
            ProductId = TestProduct2.Id,
            Quantity = 2,
        };

        TestCartItem = new CartItem()
        {
            CartId = TestCart.Id,
            ProductId = TestProduct.Id,
        };

        TestCartItem2 = new CartItem()
        {
            CartId = TestCart.Id,
            ProductId = TestProduct2.Id,
        };

        _dbContext.Users.Add(CompanyUser);
        _dbContext.Users.Add(BuyerUser);
        _dbContext.Addresses.Add(TestAddress);
        _dbContext.Companies.Add(TestCompany);
        _dbContext.Carts.Add(TestCart);
        _dbContext.Products.Add(TestProduct);
        _dbContext.Products.Add(TestProduct2);
        _dbContext.Products.Add(TestProduct3);
        _dbContext.Scooters.Add(TestScooter);
        _dbContext.Bikes.Add(TestBike);
        _dbContext.Accessories.Add(TestAccessory);
        _dbContext.Orders.Add(TestOrder);
        _dbContext.OrderItems.Add(TestOrderItem1);
        _dbContext.OrderItems.Add(TestOrderItem2);
        _dbContext.CartItems.Add(TestCartItem);
        _dbContext.CartItems.Add(TestCartItem2);

        _dbContext.SaveChanges();
    }
}
