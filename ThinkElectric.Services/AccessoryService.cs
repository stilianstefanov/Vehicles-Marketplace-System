namespace ThinkElectric.Services;

using Contracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using ThinkElectric.Web.ViewModels.Accessory.Enums;
using Web.ViewModels.Accessory;
using Web.ViewModels.Product;

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
            .Where(a => a.Id.ToString() == id && !a.Product.IsDeleted)
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
            .AnyAsync(a => a.Id.ToString() == id && !a.Product.IsDeleted);

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

    public async Task<AccessoryAllQueryModel> GetAllFilteredAndPagedAsync(AccessoryAllQueryModel queryModel)
    {
        IQueryable<Accessory> accessoriesQuery = _dbContext
            .Accessories
            .Where(a => !a.Product.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            string wildCardSearchTerm = $"%{queryModel.SearchTerm.ToLower()}%";

            accessoriesQuery = accessoriesQuery
                .Where(s => EF.Functions.Like(s.Product.Name, wildCardSearchTerm) ||
                            EF.Functions.Like(s.Brand, wildCardSearchTerm) ||
                            EF.Functions.Like(s.CompatibleBrand, wildCardSearchTerm) ||
                            EF.Functions.Like(s.CompatibleModel, wildCardSearchTerm));
        }

        accessoriesQuery = queryModel.AccessorySorting switch
        {
            AccessorySorting.NameDescending => accessoriesQuery.OrderByDescending(a => a.Product.Name),
            AccessorySorting.NameAscending => accessoriesQuery.OrderBy(a => a.Product.Name),
            AccessorySorting.PriceDescending => accessoriesQuery.OrderByDescending(a => a.Product.Price),
            AccessorySorting.PriceAscending => accessoriesQuery.OrderBy(a => a.Product.Price),
            AccessorySorting.QuantityDescending => accessoriesQuery.OrderByDescending(a => a.Product.Quantity),
            AccessorySorting.QuantityAscending => accessoriesQuery.OrderBy(a => a.Product.Quantity),
            _ => accessoriesQuery.OrderBy(a => a.Product.Name)
        };

        IEnumerable<AccessoryAllViewModel> accessories = await accessoriesQuery
            .Skip((queryModel.CurrentPage - 1) * queryModel.AccessoriesPerPage)
            .Take(queryModel.AccessoriesPerPage)
            .Select(a => new AccessoryAllViewModel()
            {
                Id = a.Id.ToString(),
                Brand = a.Brand,
                CompatibleBrand = a.CompatibleBrand,
                CompatibleModel = a.CompatibleModel,
                Product = new ProductViewModel()
                {
                    Id = a.ProductId.ToString(),
                    Name = a.Product.Name,
                    Price = a.Product.Price.ToString("f2"),
                    Quantity = a.Product.Quantity,
                    ImageId = a.Product.ImageId.ToString()
                }
            })
            .ToArrayAsync();

        int totalPages = (int)Math.Ceiling(await accessoriesQuery.CountAsync() / (double)queryModel.AccessoriesPerPage);

        queryModel.TotalPages = totalPages;

        queryModel.Accessories = accessories;

        return queryModel;
    }
}
