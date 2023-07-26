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

    public async Task<bool> IsAccessoryExistingAsync(string id)
    {
        bool isAccessoryExisting = await _dbContext
            .Accessories
            .AnyAsync(a => a.Id.ToString() == id);

        return isAccessoryExisting;
    }

    public async Task<bool> IsUserAuthorizedToEditAsync(string id, string companyId)
    {
        bool isUserAuthorizedToEdit = await _dbContext
            .Accessories
            .AnyAsync(a => a.Id.ToString() == id && a.Product.CompanyId.ToString() == companyId);

        return isUserAuthorizedToEdit;
    }

    public async Task<AccessoryEditViewModel> GetAccessoryEditViewModelByIdAsync(string id)
    {
        AccessoryEditViewModel model = await _dbContext
            .Accessories
            .Where(a => a.Id.ToString() == id)
            .Select(a => new AccessoryEditViewModel()
            {
                Brand = a.Brand,
                Description = a.Description,
                CompatibleBrand = a.CompatibleBrand,
                CompatibleModel = a.CompatibleModel,
                ProductId = a.ProductId.ToString(),
            })
            .FirstAsync();

        return model;
    }

    public async Task<string> GetProductIdByAccessoryIdAsync(string id)
    {
        string productId = await _dbContext
            .Accessories
            .Where(a => a.Id.ToString() == id)
            .Select(a => a.ProductId.ToString())
            .FirstAsync();

        return productId;
    }

    public async Task EditAsync(string id, AccessoryEditViewModel accessoryModel)
    {
        Accessory accessory = await _dbContext
            .Accessories
            .Where(a => a.Id.ToString() == id)
            .FirstAsync();

        accessory.Brand = accessoryModel.Brand;
        accessory.Description = accessoryModel.Description;
        accessory.CompatibleBrand = accessoryModel.CompatibleBrand;
        accessory.CompatibleModel = accessoryModel.CompatibleModel;

        await _dbContext.SaveChangesAsync();
    }
}
