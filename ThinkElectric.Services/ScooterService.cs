namespace ThinkElectric.Services;

using Microsoft.EntityFrameworkCore;

using Contracts;
using Data;
using Data.Models;
using Data.Models.Enums.Scooter;
using Web.ViewModels.Product;
using Web.ViewModels.Scooter;
using Web.ViewModels.Scooter.Enums;

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

    public async Task<string> GetProductIdByScooterIdAsync(string id)
    {
        string productId = await _dbContext
            .Scooters
            .Where(s => s.Id.ToString() == id)
            .Select(s => s.ProductId.ToString())
            .FirstAsync();

        return productId;
    }

    public async Task EditAsync(string id, ScooterEditViewModel scooterModel)
    {
        Scooter scooter = await _dbContext
            .Scooters
            .Where(s => s.Id.ToString() == id)
            .FirstAsync();

        scooter.Brand = scooterModel.Brand;
        scooter.Model = scooterModel.Model;
        scooter.Color = scooterModel.Color;
        scooter.Battery = scooterModel.Battery;
        scooter.Type = (ScooterType)scooterModel.ScooterType;
        scooter.EngineType = (ScooterEngineType)scooterModel.EngineType;
        scooter.BrakesType = (ScooterBrakesType)scooterModel.BrakesType;
        scooter.Range = scooterModel.Range;
        scooter.TopSpeed = scooterModel.TopSpeed;
        scooter.Weight = scooterModel.Weight;
        scooter.MaxLoad = scooterModel.MaxLoad;
        scooter.TireSize = scooterModel.TireSize;
        scooter.ChargingTime = scooterModel.ChargingTime;
        scooter.EnginePower = scooterModel.EnginePower;


        await _dbContext.SaveChangesAsync();
    }

    public async Task<ScooterAllQueryModel> GetAllFilteredAndPagedAsync(ScooterAllQueryModel queryModel)
    {
        IQueryable<Scooter> scootersQuery = _dbContext
            .Scooters
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            string wildCardSearchTerm = $"%{queryModel.SearchTerm.ToLower()}%";

            scootersQuery = scootersQuery
                .Where(s => EF.Functions.Like(s.Product.Name, wildCardSearchTerm) ||
                                   EF.Functions.Like(s.Brand, wildCardSearchTerm) ||
                                   EF.Functions.Like(s.Model, wildCardSearchTerm) ||
                                   EF.Functions.Like(s.Color, wildCardSearchTerm));
        }

        scootersQuery = queryModel.ScooterSorting switch
        {
            ScooterSorting.NameDescending => scootersQuery.OrderByDescending(s => s.Product.Name),
            ScooterSorting.NameAscending => scootersQuery.OrderBy(s => s.Product.Name),
            ScooterSorting.PriceDescending => scootersQuery.OrderByDescending(s => s.Product.Price),
            ScooterSorting.PriceAscending => scootersQuery.OrderBy(s => s.Product.Price),
            ScooterSorting.QuantityDescending => scootersQuery.OrderByDescending(s => s.Product.Quantity),
            ScooterSorting.QuantityAscending => scootersQuery.OrderBy(s => s.Product.Quantity),
            ScooterSorting.RangeDescending => scootersQuery.OrderByDescending(s => s.Range),
            ScooterSorting.RangeAscending => scootersQuery.OrderBy(s => s.Range),
            ScooterSorting.MaxSpeedDescending => scootersQuery.OrderByDescending(s => s.TopSpeed),
            ScooterSorting.MaxSpeedAscending => scootersQuery.OrderBy(s => s.TopSpeed),
            ScooterSorting.EnginePowerDescending => scootersQuery.OrderByDescending(s => s.EnginePower),
            ScooterSorting.EnginePowerAscending => scootersQuery.OrderBy(s => s.EnginePower),
            _ => scootersQuery.OrderByDescending(s => s.Product.Name)
        };

        scootersQuery = queryModel.QueryScooterType switch
        {
            QueryScooterType.Lightweight => scootersQuery.Where(s => s.Type == ScooterType.Lightweight),
            QueryScooterType.OffRoad => scootersQuery.Where(s => s.Type == ScooterType.OffRoad),
            QueryScooterType.VespaStyle => scootersQuery.Where(s => s.Type == ScooterType.VespaStyle),
            QueryScooterType.LongRange => scootersQuery.Where(s => s.Type == ScooterType.LongRange),
            QueryScooterType.Performance => scootersQuery.Where(s => s.Type == ScooterType.Performance),
            QueryScooterType.SitDown => scootersQuery.Where(s => s.Type == ScooterType.SitDown),
            _ => scootersQuery
        };

        scootersQuery = queryModel.QueryScooterEngineType switch
        {
            QueryScooterEngineType.DualMotor => scootersQuery.Where(s => s.EngineType == ScooterEngineType.DualMotor),
            QueryScooterEngineType.SingleMotor => scootersQuery.Where(
                s => s.EngineType == ScooterEngineType.SingleMotor),
            _ => scootersQuery
        };

        scootersQuery = queryModel.QueryScooterBrakesType switch
        {
            QueryScooterBrakesType.Electric => scootersQuery.Where(s => s.BrakesType == ScooterBrakesType.Electric),
            QueryScooterBrakesType.Hydraulic => scootersQuery.Where(s => s.BrakesType == ScooterBrakesType.Hydraulic),
            QueryScooterBrakesType.Mechanical => scootersQuery.Where(s => s.BrakesType == ScooterBrakesType.Mechanical),
            _ => scootersQuery
        };

        IEnumerable<ScooterAllViewModel> scooters = await scootersQuery
            .Skip((queryModel.CurrentPage - 1) * queryModel.ScootersPerPage)
            .Take(queryModel.ScootersPerPage)
            .Select(s => new ScooterAllViewModel()
            {
                Id = s.Id.ToString(),
                Brand = s.Brand,
                Model = s.Model,
                Color = s.Color,
                ScooterType = s.Type.ToString(),
                EngineType = s.EngineType.ToString(),
                BrakesType = s.BrakesType.ToString(),
                Range = s.Range,
                MaxSpeed = s.TopSpeed,
                EnginePower = s.EnginePower,
                Product = new ProductViewModel()
                {
                    Id = s.ProductId.ToString(),
                    Name = s.Product.Name,
                    Price = s.Product.Price.ToString("f2"),
                    Quantity = s.Product.Quantity,
                    ImageId = s.Product.ImageId
                }
            })
        .ToArrayAsync();

        int totalPages = (int)Math.Ceiling(await scootersQuery.CountAsync() / (double)queryModel.ScootersPerPage);

        queryModel.TotalPages = totalPages;

        queryModel.Scooters = scooters;

        return queryModel;
    }
}
