namespace ThinkElectric.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using Models.Enums.Bike;

public class BikeConfiguration : IEntityTypeConfiguration<Bike>
{
    public void Configure(EntityTypeBuilder<Bike> builder)
    {
        builder.HasData(SeedBikes());
    }

    private List<Bike> SeedBikes()
    {
        List<Bike> bikes = new List<Bike>();

        var bike = new Bike()
        {
            Id = Guid.Parse("2C2A92EC-55D2-46E0-841B-9024C1659CB1"),
            Brand = "ADO",
            Model = "A16XE",
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
            ProductId = Guid.Parse("0D233DD0-BD97-4BEE-BC9E-DD96CCEF5D12"),
        };

        bikes.Add(bike);

        bike = new Bike()
        {
            Id = Guid.Parse("E56A719F-AD51-4BFA-B24B-EAD6EF17192C"),
            Brand = "ADO",
            Model = "A20 AIR",
            Color = "Gray",
            FrameMaterial = "Aluminum",
            Battery = "48V 15Ah",
            Type = BikeType.City,
            FrameType = BikeFrameType.Foldable,
            SuspensionType = BikeSuspensionType.Front,
            BrakesType = BikeBrakesType.DiscBrakes,
            EngineType = BikeEngineType.Middle,
            FrameSize = 18,
            GearsCount = 6,
            Range = 50,
            TopSpeed = 35,
            Weight = 20,
            MaxLoad = 100,
            WheelSize = 14,
            ChargingTime = 7,
            EnginePower = 300,
            ProductId = Guid.Parse("F6B1216E-E1A6-4B83-A2B4-DC58F30D0E8E"),
        };

        bikes.Add(bike);

        bike = new Bike()
        {
            Id = Guid.Parse("5D9EC56D-D23A-49CB-B7E8-39EBD8F5C302"),
            Brand = "ADO",
            Model = "20 F Beast",
            Color = "Black",
            FrameMaterial = "Carbon",
            Battery = "48V 20Ah",
            Type = BikeType.Mountain,
            FrameType = BikeFrameType.NotFoldable,
            SuspensionType = BikeSuspensionType.Full,
            BrakesType = BikeBrakesType.HydraulicBrakes,
            EngineType = BikeEngineType.Rear,
            FrameSize = 22,
            GearsCount = 10,
            Range = 70,
            TopSpeed = 40,
            Weight = 35,
            MaxLoad = 150,
            WheelSize = 18,
            ChargingTime = 9,
            EnginePower = 600,
            ProductId = Guid.Parse("485B227B-69D8-4A3C-BF73-7F25007225C5"),
        };

        bikes.Add(bike);

        return bikes;
    }
}
