namespace ThinkElectric.Services;

using Contracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Accessory;

public class AccessoryService : IAccessoryService
{
    private readonly ThinkElectricDbContext _dbContext;

    public AccessoryService(ThinkElectricDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<string> CreateAsync(AccessoryCreateViewModel accessoryModel, string productId)
    {
        Accessory accessory = new Accessory()
        {
            Brand = accessoryModel.Brand,
            Description = accessoryModel.Description,
            CompatibleBrand = accessoryModel.CompatibleBrand,
            CompatibleModel = accessoryModel.CompatibleModel,
            ProductId = Guid.Parse(productId)
        };

        await _dbContext.Accessories.AddAsync(accessory);

        await _dbContext.SaveChangesAsync();

        return accessory.Id.ToString();
    }

    public async Task<AccessoryDetailsViewModel?> GetAccessoryDetailsByIdAsync(string id)
    {
        AccessoryDetailsViewModel? model = await _dbContext
            .Accessories
            .Where(a => a.Id.ToString() == id)
            .Select(a => new AccessoryDetailsViewModel()
            {
                Id = a.Id.ToString(),
                Brand = a.Brand,
                Description = a.Description,
                CompatibleBrand = a.CompatibleBrand,
                CompatibleModel = a.CompatibleModel,
                ProductId = a.ProductId.ToString(),
            })
            .FirstOrDefaultAsync();

        return model;
    }
}
