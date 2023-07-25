namespace ThinkElectric.Services;

using Microsoft.EntityFrameworkCore;

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

    public async Task<ScooterDetailsViewModel?> GetScooterDetailsByIdAsync(string id)
    {
        ScooterDetailsViewModel? model = await _dbContext
            .Scooters
            .Where(s => s.Id.ToString() == id)
            .Select(s => new ScooterDetailsViewModel()
            {
                Id = s.Id.ToString(),
                Brand = s.Brand,
                Model = s.Model,
                Color = s.Color,
                Battery = s.Battery,
                ScooterType = s.Type.ToString(),
                EngineType = s.EngineType.ToString(),
                BrakesType = s.BrakesType.ToString(),
                Range = s.Range,
                TopSpeed = s.TopSpeed,
                Weight = s.Weight,
                MaxLoad = s.MaxLoad,
                TireSize = s.TireSize,
                ChargingTime = s.ChargingTime,
                EnginePower = s.EnginePower,
                ProductId = s.ProductId.ToString(),

            })
            .FirstOrDefaultAsync();

        return model;
    }

    public async Task<bool> IsScooterExistingAsync(string id)
    {
        bool isScooterExisting = _dbContext
            .Scooters
            .Any(s => s.Id.ToString() == id);

        return isScooterExisting;
    }

    public async Task<bool> IsUserAuthorizedToEditAsync(string id, string userCompanyId)
    {
        bool isAuthorized = await _dbContext
            .Scooters
            .AnyAsync(s => s.Id.ToString() == id && s.Product.CompanyId.ToString() == userCompanyId);

        return isAuthorized;
    }

    public async Task<ScooterEditViewModel> GetScooterEditViewModelByIdAsync(string id)
    {
        ScooterEditViewModel model = await _dbContext
            .Scooters
            .Where(s => s.Id.ToString() == id)
            .Select(s => new ScooterEditViewModel()
            {
                Brand = s.Brand,
                Model = s.Model,
                Color = s.Color,
                Battery = s.Battery,
                ScooterType = (int)s.Type,
                EngineType = (int)s.EngineType,
                BrakesType = (int)s.BrakesType,
                Range = s.Range,
                TopSpeed = s.TopSpeed,
                Weight = s.Weight,
                MaxLoad = s.MaxLoad,
                TireSize = s.TireSize,
                ChargingTime = s.ChargingTime,
                EnginePower = s.EnginePower,
                ProductId = s.ProductId.ToString(),
            })
            .FirstAsync();

        return model;
    }
}
