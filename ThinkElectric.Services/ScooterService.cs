namespace ThinkElectric.Services;

using Contracts;
using Data;
using Data.Models;
using Data.Models.Enums.Scooter;
using Web.ViewModels.Scooter;

public class ScooterService : IScooterService
{
    private readonly ThinkElectricDbContext _dbContext;

    public ScooterService(ThinkElectricDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<string> CreateAsync(ScooterCreateViewModel model, string productId)
    {
        Scooter scooter = new Scooter()
        {
            Brand = model.Brand,
            Model = model.Model,
            Color = model.Color,
            Battery = model.Battery,
            Type = (ScooterType)model.ScooterType,
            EngineType = (ScooterEngineType)model.EngineType,
            BrakesType = (ScooterBrakesType)model.BrakesType,
            Range = model.Range,
            TopSpeed = model.TopSpeed,
            Weight = model.Weight,
            MaxLoad = model.MaxLoad,
            TireSize = model.TireSize,
            ChargingTime = model.ChargingTime,
            EnginePower = model.EnginePower,
            ProductId = Guid.Parse(productId)
        };

        await _dbContext.Scooters.AddAsync(scooter);
        await _dbContext.SaveChangesAsync();

        return scooter.Id.ToString();
    }
}
