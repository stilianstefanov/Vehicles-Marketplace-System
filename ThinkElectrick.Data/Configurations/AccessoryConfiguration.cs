namespace ThinkElectric.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

public class AccessoryConfiguration : IEntityTypeConfiguration<Accessory>
{
    public void Configure(EntityTypeBuilder<Accessory> builder)
    {
        builder.HasData(SeedAccessories());
    }

    private List<Accessory> SeedAccessories()
    {
        List<Accessory> accessories = new List<Accessory>();

        var accessory = new Accessory()
        {
            Id = Guid.Parse("58605343-B867-4905-BFA3-364A8D3940D3"),
            Brand = "Kaabo",
            Description = "Original Kaabo bag for the your Kaabo Mantis scooter!",
            CompatibleBrand = "Kaabo",
            CompatibleModel = "Mantis",
            ProductId = Guid.Parse("3E044B98-8123-4273-B51F-9CF0DFC13760")
        };

        accessories.Add(accessory);

        accessory = new Accessory()
        {
            Id = Guid.Parse("9318D0EA-30DB-4431-9C30-AE1993F17728"),
            Brand = "Kaabo",
            Description = "Original Kaabo lock for your kaabo wolf warrior scooter!",
            CompatibleBrand = "Kaabo",
            CompatibleModel = "Wolf Warrior",
            ProductId = Guid.Parse("D2AB0180-A9B8-470E-8A0F-9ACACCB0C9BC")
        };

        accessories.Add(accessory);

        accessory = new Accessory()
        {
            Id = Guid.Parse("AE2BBAD3-A635-4860-B2C0-3260F87CE97B"),
            Brand = "SideTech",
            Description = "High quality side mirrors for Xiaomi Pro2 scooters!",
            CompatibleBrand = "Xiaomi",
            CompatibleModel = "Pro 2",
            ProductId = Guid.Parse("8FE19082-5089-4AC5-88D9-51FDFA48FE10")
        };

        accessories.Add(accessory);

        return accessories;
    }
}
