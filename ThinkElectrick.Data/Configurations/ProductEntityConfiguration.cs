namespace ThinkElectric.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using Models.Enums.Product;

public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
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

        builder.HasData(SeedProducts());
    }

    private List<Product> SeedProducts()
    {
        List<Product> products = new List<Product>();

        var product = new Product()
        {
            Id = Guid.Parse("BD96C711-32E7-483B-B1FE-9C19C1E0A936"),
            Name = "Kaabo Wolf Warrior 10",
            Price = 4999.99M,
            Quantity = 10,
            ProductType = ProductType.Scooter,
            CreatedOn = new DateTime(2023, 7, 25, 8, 45, 25),
            CompanyId = Guid.Parse("C0781351-133E-4383-81E7-C95E20FA1273"),
            ImageId = "64ce5c39ad51218262de3f60"
        };

        products.Add(product);

        product = new Product()
        {
            Id = Guid.Parse("1728DC0C-96D8-4869-886E-4829DB33A103"),
            Name = "Kaabo Mantis King",
            Price = 2999.99M,
            Quantity = 15,
            ProductType = ProductType.Scooter,
            CreatedOn = new DateTime(2023, 7, 24, 8, 45, 25),
            CompanyId = Guid.Parse("C0781351-133E-4383-81E7-C95E20FA1273"),
            ImageId = "64ce5f7abd83b3123fc9ab71"
        };

        products.Add(product);

        product = new Product()
        {
            Id = Guid.Parse("67245662-BC0A-4F41-B53E-A9FB4BBDAA9F"),
            Name = "Kaabo Mantis 10 Pro",
            Price = 1500.99M,
            Quantity = 20,
            ProductType = ProductType.Scooter,
            CreatedOn = new DateTime(2023, 7, 20, 7, 45, 25),
            CompanyId = Guid.Parse("C0781351-133E-4383-81E7-C95E20FA1273"),
            ImageId = "64ce64743037f396fd344914"
        };

        products.Add(product);

        product = new Product()
        {
            Id = Guid.Parse("253A48E4-63C2-4564-B911-B098F37D8370"),
            Name = "Xiaomi Mi Pro 2",
            Price = 500.99M,
            Quantity = 15,
            ProductType = ProductType.Scooter,
            CreatedOn = new DateTime(2023, 5, 25, 8, 45, 25),
            CompanyId = Guid.Parse("5D652EBF-7B4D-430C-9AF8-0C02B0256575"),
            ImageId = "64ce66572b18cc1f74d4f76b"
        };

        products.Add(product);

        product = new Product()
        {
            Id = Guid.Parse("D96BB9EA-2CEA-4D39-BB90-E8A94D58A819"),
            Name = "Xiaomi Mi 365",
            Price = 450.99M,
            Quantity = 30,
            ProductType = ProductType.Scooter,
            CreatedOn = new DateTime(2023, 2, 26, 8, 45, 25),
            CompanyId = Guid.Parse("5D652EBF-7B4D-430C-9AF8-0C02B0256575"),
            ImageId = "64ce67d918f465b345e8e244"
        };

        products.Add(product);

        product = new Product()
        {
            Id = Guid.Parse("D3B10C99-12E3-4E69-9771-70DAFAC10BB3"),
            Name = "Xiaomi 365 Amg",
            Price = 799.99M,
            Quantity = 5,
            ProductType = ProductType.Scooter,
            CreatedOn = new DateTime(2023, 5, 25, 8, 25, 25),
            CompanyId = Guid.Parse("5D652EBF-7B4D-430C-9AF8-0C02B0256575"),
            ImageId = "64ce68eaffdd5b60bd0f0da6"
        };

        products.Add(product);

        product = new Product()
        {
            Id = Guid.Parse("0D233DD0-BD97-4BEE-BC9E-DD96CCEF5D12"),
            Name = "ADO A16XE",
            Price = 1100.99M,
            Quantity = 15,
            ProductType = ProductType.Bike,
            CreatedOn = new DateTime(2023, 3, 15, 8, 45, 25),
            CompanyId = Guid.Parse("C0781351-133E-4383-81E7-C95E20FA1273"),
            ImageId = "64ce6ac5332b4fcd3cfbf213"
        };

        products.Add(product);

        product = new Product()
        {
            Id = Guid.Parse("F6B1216E-E1A6-4B83-A2B4-DC58F30D0E8E"),
            Name = "ADO A20 AIR",
            Price = 1000.99M,
            Quantity = 10,
            ProductType = ProductType.Bike,
            CreatedOn = new DateTime(2023, 7, 11, 4, 45, 25),
            CompanyId = Guid.Parse("C0781351-133E-4383-81E7-C95E20FA1273"),
            ImageId = "64ce6e7274be46075f7f39ef"
        };

        products.Add(product);

        product = new Product()
        {
            Id = Guid.Parse("485B227B-69D8-4A3C-BF73-7F25007225C5"),
            Name = "ADO 20 F BEAST",
            Price = 2000.99M,
            Quantity = 5,
            ProductType = ProductType.Bike,
            CreatedOn = new DateTime(2023, 7, 17, 8, 25, 25),
            CompanyId = Guid.Parse("5D652EBF-7B4D-430C-9AF8-0C02B0256575"),
            ImageId = "64ce6fc7a61cf5992be7bf4f"
        };

        products.Add(product);

        product = new Product()
        {
            Id = Guid.Parse("3E044B98-8123-4273-B51F-9CF0DFC13760"),
            Name = "Kaabo Bag",
            Price = 50.99M,
            Quantity = 35,
            ProductType = ProductType.Accessory,
            CreatedOn = new DateTime(2023, 3, 3, 8, 45, 25),
            CompanyId = Guid.Parse("5D652EBF-7B4D-430C-9AF8-0C02B0256575"),
            ImageId = "64ce7219abc83a6ff8f1da33"
        };

        products.Add(product);

        product = new Product()
        {
            Id = Guid.Parse("D2AB0180-A9B8-470E-8A0F-9ACACCB0C9BC"),
            Name = "Kaabo Disc Lock",
            Price = 70.99M,
            Quantity = 10,
            ProductType = ProductType.Accessory,
            CreatedOn = new DateTime(2023, 7, 8, 8, 19, 25),
            CompanyId = Guid.Parse("C0781351-133E-4383-81E7-C95E20FA1273"),
            ImageId = "64ce737583efd7dc2360de20"
        };

        products.Add(product);

        product = new Product()
        {
            Id = Guid.Parse("8FE19082-5089-4AC5-88D9-51FDFA48FE10"),
            Name = "Xiaomi Side Mirrors",
            Price = 40.99M,
            Quantity = 15,
            ProductType = ProductType.Accessory,
            CreatedOn = new DateTime(2023, 6, 16, 6, 45, 25),
            CompanyId = Guid.Parse("C0781351-133E-4383-81E7-C95E20FA1273"),
            ImageId = "64ce74f819d865ac7cfd73e6"
        };

        products.Add(product);

        return products;
    }
}
