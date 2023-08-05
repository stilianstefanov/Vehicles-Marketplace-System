namespace ThinkElectric.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;
using Models.Enums.Scooter;

public class ScooterConfiguration : IEntityTypeConfiguration<Scooter>
{
    public void Configure(EntityTypeBuilder<Scooter> builder)
    {
        builder.HasData(SeedScooters());
    }

    private List<Scooter> SeedScooters()
    {
        List<Scooter> scooters = new List<Scooter>();

        var scooter = new Scooter()
        {
            Id = Guid.Parse("3108D779-0C6E-4178-B309-5A9D4EE11D29"),
            Brand = "Kaabo",
            Model = "Wolf Warrior 10",
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
            ProductId = Guid.Parse("BD96C711-32E7-483B-B1FE-9C19C1E0A936")
        };

        scooters.Add(scooter);

        scooter = new Scooter()
        {
            Brand = "Kaabo",
            Model = "Mantis King",
            Color = "Black",
            Battery = "Li-Ion 60V 24.5Ah",
            Type = ScooterType.OffRoad,
            EngineType = ScooterEngineType.DualMotor,
            BrakesType = ScooterBrakesType.Hydraulic,
            Range = 100,
            TopSpeed = 70,
            Weight = 40,
            MaxLoad = 120,
            TireSize = 10,
            ChargingTime = 10,
            EnginePower = 2000,
            ProductId = Guid.Parse("1728DC0C-96D8-4869-886E-4829DB33A103")
        };

        scooters.Add(scooter);

        scooter = new Scooter()
        {
            Brand = "Kaabo",
            Model = "Mantis 10 Pro",
            Color = "Red",
            Battery = "Li-Ion 60V 18.5Ah",
            Type = ScooterType.Performance,
            EngineType = ScooterEngineType.DualMotor,
            BrakesType = ScooterBrakesType.Mechanical,
            Range = 70,
            TopSpeed = 60,
            Weight = 30,
            MaxLoad = 120,
            TireSize = 10,
            ChargingTime = 9,
            EnginePower = 1000,
            ProductId = Guid.Parse("67245662-BC0A-4F41-B53E-A9FB4BBDAA9F")
        };

        scooters.Add(scooter);

        scooter = new Scooter()
        {
            Brand = "Xiaomi",
            Model = "Mi Pro 2",
            Color = "Black",
            Battery = "Li-Ion 47V 12.5Ah",
            Type = ScooterType.Lightweight,
            EngineType = ScooterEngineType.SingleMotor,
            BrakesType = ScooterBrakesType.Electric,
            Range = 45,
            TopSpeed = 25,
            Weight = 18,
            MaxLoad = 100,
            TireSize = 8,
            ChargingTime = 6,
            EnginePower = 350,
            ProductId = Guid.Parse("253A48E4-63C2-4564-B911-B098F37D8370")
        };

        scooters.Add(scooter);

        scooter = new Scooter()
        {
            Brand = "Xiaomi",
            Model = "Mi 365",
            Color = "White",
            Battery = "Li-Ion 47V 10.5Ah",
            Type = ScooterType.Lightweight,
            EngineType = ScooterEngineType.SingleMotor,
            BrakesType = ScooterBrakesType.Electric,
            Range = 35,
            TopSpeed = 25,
            Weight = 17,
            MaxLoad = 100,
            TireSize = 8,
            ChargingTime = 6,
            EnginePower = 300,
            ProductId = Guid.Parse("D96BB9EA-2CEA-4D39-BB90-E8A94D58A819")
        };

        scooters.Add(scooter);

        scooter = new Scooter()
        {
            Brand = "Xiaomi",
            Model = "Mi Amg",
            Color = "Gray",
            Battery = "Li-Ion 47V 13.5Ah",
            Type = ScooterType.Lightweight,
            EngineType = ScooterEngineType.SingleMotor,
            BrakesType = ScooterBrakesType.Electric,
            Range = 40,
            TopSpeed = 25,
            Weight = 18,
            MaxLoad = 120,
            TireSize = 8,
            ChargingTime = 7,
            EnginePower = 400,
            ProductId = Guid.Parse("D3B10C99-12E3-4E69-9771-70DAFAC10BB3")
        };

        scooters.Add(scooter);

        return scooters;
    }
}
