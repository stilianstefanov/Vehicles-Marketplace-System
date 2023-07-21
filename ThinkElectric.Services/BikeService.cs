namespace ThinkElectric.Services;

using Contracts;
using Data;
using Data.Models;
using Data.Models.Enums.Bike;
using Web.ViewModels.Bike;

public class BikeService : IBikeService
{
    private readonly ThinkElectricDbContext _dbContext;

    public BikeService(ThinkElectricDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<string> CreateAsync(BikeCreateViewModel bikeModel, string productId)
    {
        Bike bike = new Bike()
        {
            Brand = bikeModel.Brand,
            Model = bikeModel.Model,
            Color = bikeModel.Color,
            Battery = bikeModel.Battery,
            FrameMaterial = bikeModel.FrameMaterial,
            Type = (BikeType)bikeModel.BikeType,
            FrameType = (BikeFrameType)bikeModel.FrameType,
            SuspensionType = (BikeSuspensionType)bikeModel.SuspensionType,
            BrakesType = (BikeBrakesType)bikeModel.BrakesType,
            EngineType = (BikeEngineType)bikeModel.EngineType,
            FrameSize = bikeModel.FrameSize,
            GearsCount = bikeModel.GearsCount,
            Range = bikeModel.Range,
            TopSpeed = bikeModel.TopSpeed,
            Weight = bikeModel.Weight,
            MaxLoad = bikeModel.MaxLoad,
            WheelSize = bikeModel.WheelSize,
            ChargingTime = bikeModel.ChargingTime,
            EnginePower = bikeModel.EnginePower,
            ProductId = Guid.Parse(productId)
        };

        await _dbContext.Bikes.AddAsync(bike);
        await _dbContext.SaveChangesAsync();

        return bike.Id.ToString();
    }
}
