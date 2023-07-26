namespace ThinkElectric.Services;

using Contracts;
using Data;
using Data.Models;
using Data.Models.Enums.Bike;
using Microsoft.EntityFrameworkCore;
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

    public async Task<BikeDetailsViewModel?> GetBikeDetailsByIdAsync(string id)
    {
        BikeDetailsViewModel? model = await _dbContext
            .Bikes
            .Where(b => b.Id.ToString() == id)
            .Select(b => new BikeDetailsViewModel()
            {
                Brand = b.Brand,
                Model = b.Model,
                Color = b.Color,
                Battery = b.Battery,
                FrameMaterial = b.FrameMaterial,
                BikeType = b.Type.ToString(),
                FrameType = b.FrameType.ToString(),
                SuspensionType = b.SuspensionType.ToString(),
                BrakesType = b.BrakesType.ToString(),
                EngineType = b.EngineType.ToString(),
                FrameSize = b.FrameSize,
                GearsCount = b.GearsCount,
                Range = b.Range,
                TopSpeed = b.TopSpeed,
                Weight = b.Weight,
                MaxLoad = b.MaxLoad,
                WheelSize = b.WheelSize,
                ChargingTime = b.ChargingTime,
                EnginePower = b.EnginePower,
                ProductId = b.ProductId.ToString()
            })
            .FirstOrDefaultAsync();

        return model;
    }

    public async Task<bool> IsBikeExistingAsync(string id)
    {
        bool isExisting = await _dbContext
            .Bikes
            .AnyAsync(b => b.Id.ToString() == id);

        return isExisting;
    }

    public async Task<bool> IsUserAuthorizedToEditAsync(string id, string companyId)
    {
        bool isAuthorized = await _dbContext
            .Bikes
            .AnyAsync(b => b.Id.ToString() == id && b.Product.CompanyId.ToString() == companyId);

        return isAuthorized;
    }

    public async Task<BikeEditViewModel> GetBikeEditViewModelByIdAsync(string id)
    {
        BikeEditViewModel model = await _dbContext
            .Bikes
            .Where(b => b.Id.ToString() == id)
            .Select(b => new BikeEditViewModel()
            {
                Brand = b.Brand,
                Model = b.Model,
                Color = b.Color,
                Battery = b.Battery,
                FrameMaterial = b.FrameMaterial,
                BikeType = (int)b.Type,
                FrameType = (int)b.FrameType,
                SuspensionType = (int)b.SuspensionType,
                BrakesType = (int)b.BrakesType,
                EngineType = (int)b.EngineType,
                FrameSize = b.FrameSize,
                GearsCount = b.GearsCount,
                Range = b.Range,
                TopSpeed = b.TopSpeed,
                Weight = b.Weight,
                MaxLoad = b.MaxLoad,
                WheelSize = b.WheelSize,
                ChargingTime = b.ChargingTime,
                EnginePower = b.EnginePower,
                ProductId = b.ProductId.ToString()
            })
            .FirstAsync();

        return model;
    }
}
