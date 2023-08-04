namespace ThinkElectric.Services;

using Contracts;
using Data;
using Data.Models;
using Data.Models.Enums.Bike;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Bike;
using Web.ViewModels.Bike.Enums;
using Web.ViewModels.Product;

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
            .Where(b => b.Id.ToString() == id && !b.Product.IsDeleted)
            .Select(b => new BikeDetailsViewModel()
            {
                Id = b.Id.ToString(),
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
            .AnyAsync(b => b.Id.ToString() == id && !b.Product.IsDeleted);

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

    public async Task<string> GetProductIdByBikeIdAsync(string id)
    {
        string productId = await _dbContext
            .Bikes
            .Where(b => b.Id.ToString() == id)
            .Select(b => b.ProductId.ToString())
            .FirstAsync();

        return productId;
    }

    public async Task EditAsync(string id, BikeEditViewModel bikeModel)
    {
        Bike bike = await _dbContext
            .Bikes
            .Where(b => b.Id.ToString() == id)
            .FirstAsync();

        bike.Brand = bikeModel.Brand;
        bike.Model = bikeModel.Model;
        bike.Color = bikeModel.Color;
        bike.Battery = bikeModel.Battery;
        bike.FrameMaterial = bikeModel.FrameMaterial;
        bike.Type = (BikeType)bikeModel.BikeType;
        bike.FrameType = (BikeFrameType)bikeModel.FrameType;
        bike.SuspensionType = (BikeSuspensionType)bikeModel.SuspensionType;
        bike.BrakesType = (BikeBrakesType)bikeModel.BrakesType;
        bike.EngineType = (BikeEngineType)bikeModel.EngineType;
        bike.FrameSize = bikeModel.FrameSize;
        bike.GearsCount = bikeModel.GearsCount;
        bike.Range = bikeModel.Range;
        bike.TopSpeed = bikeModel.TopSpeed;
        bike.Weight = bikeModel.Weight;
        bike.MaxLoad = bikeModel.MaxLoad;
        bike.WheelSize = bikeModel.WheelSize;
        bike.ChargingTime = bikeModel.ChargingTime;
        bike.EnginePower = bikeModel.EnginePower;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<BikeAllQueryModel> GetAllFilteredAndPagedAsync(BikeAllQueryModel queryModel)
    {
        IQueryable<Bike> bikesQuery = _dbContext
            .Bikes
            .Where(b => !b.Product.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            string wildCardSearchTerm = $"%{queryModel.SearchTerm.ToLower()}%";

            bikesQuery = bikesQuery
                .Where(s => EF.Functions.Like(s.Product.Name, wildCardSearchTerm) ||
                            EF.Functions.Like(s.Brand, wildCardSearchTerm) ||
                            EF.Functions.Like(s.Model, wildCardSearchTerm) ||
                            EF.Functions.Like(s.Color, wildCardSearchTerm));
        }

        bikesQuery = queryModel.BikeSorting switch
        {
            BikeSorting.NameDescending => bikesQuery.OrderByDescending(s => s.Product.Name),
            BikeSorting.NameAscending => bikesQuery.OrderBy(s => s.Product.Name),
            BikeSorting.PriceDescending => bikesQuery.OrderByDescending(s => s.Product.Price),
            BikeSorting.PriceAscending => bikesQuery.OrderBy(s => s.Product.Price),
            BikeSorting.QuantityDescending => bikesQuery.OrderByDescending(s => s.Product.Quantity),
            BikeSorting.QuantityAscending => bikesQuery.OrderBy(s => s.Product.Quantity),
            BikeSorting.RangeDescending => bikesQuery.OrderByDescending(s => s.Range),
            BikeSorting.RangeAscending => bikesQuery.OrderBy(s => s.Range),
            BikeSorting.MaxSpeedDescending => bikesQuery.OrderByDescending(s => s.TopSpeed),
            BikeSorting.MaxSpeedAscending => bikesQuery.OrderBy(s => s.TopSpeed),
            BikeSorting.EnginePowerDescending => bikesQuery.OrderByDescending(s => s.EnginePower),
            BikeSorting.EnginePowerAscending => bikesQuery.OrderBy(s => s.EnginePower),
            _ => bikesQuery.OrderByDescending(s => s.Product.Name)
        };

        bikesQuery = queryModel.QueryBikeType switch
        {
            QueryBikeType.City => bikesQuery.Where(s => s.Type == BikeType.City),
            QueryBikeType.Mountain => bikesQuery.Where(s => s.Type == BikeType.Mountain),
            QueryBikeType.Road => bikesQuery.Where(s => s.Type == BikeType.Road),
            QueryBikeType.Cargo => bikesQuery.Where(s => s.Type == BikeType.Cargo),
            QueryBikeType.FatTire => bikesQuery.Where(s => s.Type == BikeType.FatTire),
            _ => bikesQuery
        };

        bikesQuery = queryModel.QueryBikeSuspensionType switch
        {
            QueryBikeSuspensionType.None => bikesQuery.Where(s => s.SuspensionType == BikeSuspensionType.None),
            QueryBikeSuspensionType.Front => bikesQuery.Where(s => s.SuspensionType == BikeSuspensionType.Front),
            QueryBikeSuspensionType.Rear => bikesQuery.Where(s => s.SuspensionType == BikeSuspensionType.Rear),
            QueryBikeSuspensionType.Full => bikesQuery.Where(s => s.SuspensionType == BikeSuspensionType.Full),
            _ => bikesQuery
        };

        bikesQuery = queryModel.QueryBikeFrameType switch
        {
            QueryBikeFrameType.Foldable => bikesQuery.Where(s => s.FrameType == BikeFrameType.Foldable),
            QueryBikeFrameType.NotFoldable => bikesQuery.Where(s => s.FrameType == BikeFrameType.NotFoldable),
            _ => bikesQuery
        };

        bikesQuery = queryModel.QueryBikeEngineType switch
        {
            QueryBikeEngineType.Rear => bikesQuery.Where(s => s.EngineType == BikeEngineType.Rear),
            QueryBikeEngineType.Middle => bikesQuery.Where(s => s.EngineType == BikeEngineType.Middle),
            _ => bikesQuery
        };

        bikesQuery = queryModel.QueryBikeBrakesType switch
        {
            QueryBikeBrakesType.RimBrakes => bikesQuery.Where(s => s.BrakesType == BikeBrakesType.RimBrakes),
            QueryBikeBrakesType.DiscBrakes => bikesQuery.Where(s => s.BrakesType == BikeBrakesType.DiscBrakes),
            QueryBikeBrakesType.HydraulicBrakes => bikesQuery.Where(s => s.BrakesType == BikeBrakesType.HydraulicBrakes),
            _ => bikesQuery
        };

        IEnumerable<BikeAllViewModel> bikes = await bikesQuery
            .Skip((queryModel.CurrentPage - 1) * queryModel.BikesPerPage)
            .Take(queryModel.BikesPerPage)
            .Select(b => new BikeAllViewModel()
            {
                Id = b.Id.ToString(),
                Brand = b.Brand,
                Model = b.Model,
                Color = b.Color,
                BrakesType = b.BrakesType.ToString(),
                EngineType = b.EngineType.ToString(),
                FrameType = b.FrameType.ToString(),
                SuspensionType = b.SuspensionType.ToString(),
                BikeType = b.Type.ToString(),
                Range = b.Range,
                MaxSpeed = b.TopSpeed,
                EnginePower = b.EnginePower,
                Product = new ProductViewModel()
                {
                    Id = b.Product.Id.ToString(),
                    Name = b.Product.Name,
                    Price = b.Product.Price.ToString("f2"),
                    Quantity = b.Product.Quantity,
                    ImageId = b.Product.ImageId
                }
            })
            .ToArrayAsync();

        int totalPages = (int)Math.Ceiling(await bikesQuery.CountAsync() / (double)queryModel.BikesPerPage);

        queryModel.TotalPages = totalPages;

        queryModel.Bikes = bikes;

        return queryModel;
    }
}
