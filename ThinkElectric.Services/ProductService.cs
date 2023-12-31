﻿namespace ThinkElectric.Services;

using System.Globalization;
using Microsoft.EntityFrameworkCore;

using Contracts;
using Data;
using Data.Models;
using Data.Models.Enums.Product;
using Web.ViewModels.Product.Enums;
using Web.ViewModels.Product;

public class ProductService : IProductService
{
    private readonly ThinkElectricDbContext _dbContext;

    public ProductService(ThinkElectricDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<string> CreateAsync(ProductCreateViewModel modelProduct, string companyId, string imageId, ProductType productType)
    {
        var product = new Product()
        {
            Name = modelProduct.Name,
            ImageId = imageId,
            Price = modelProduct.Price,
            Quantity = modelProduct.Quantity,
            CreatedOn = DateTime.UtcNow,
            ProductType = productType,
            CompanyId = Guid.Parse(companyId)
        };

        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();

        return product.Id.ToString();
    }

    public async Task<ProductAllQueryModel> AllByCompanyIdAsync(ProductAllQueryModel queryModel)
    {
        var productsQuery = _dbContext.Products
            .Where(p => p.CompanyId.ToString() == queryModel.CompanyId && !p.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            var wildCardSearchTerm = $"%{queryModel.SearchTerm.ToLower()}%";

            productsQuery = productsQuery
                .Where(c => EF.Functions.Like(c.Name, wildCardSearchTerm));
        }

        productsQuery = queryModel.QueryProductType switch
        {
            QueryProductType.Scooter => productsQuery
                .Where(p => p.ProductType == ProductType.Scooter),
            QueryProductType.Bike => productsQuery
                .Where(p => p.ProductType == ProductType.Bike),
            QueryProductType.Accessory => productsQuery
                .Where(p => p.ProductType == ProductType.Accessory),
            _ => productsQuery
        };

        productsQuery = queryModel.ProductSorting switch
        {
            ProductSorting.NameDescending => productsQuery
                .OrderByDescending(p => p.Name),
            ProductSorting.NameAscending => productsQuery
                .OrderBy(p => p.Name),
            ProductSorting.PriceDescending => productsQuery
                .OrderByDescending(p => p.Price),
            ProductSorting.PriceAscending => productsQuery
                .OrderBy(p => p.Price),
            ProductSorting.QuantityDescending => productsQuery
                .OrderByDescending(p => p.Quantity),
            ProductSorting.QuantityAscending => productsQuery
                .OrderBy(p => p.Quantity),
            _ => productsQuery.OrderBy(p => p.Name)
        };

        IEnumerable<ProductAllViewModel> products = await productsQuery
            .Skip((queryModel.CurrentPage - 1) * queryModel.ProductsPerPage)
            .Take(queryModel.ProductsPerPage)
            .Select(p => new ProductAllViewModel()
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                ImageId = p.ImageId,
                CompanyId = p.CompanyId.ToString().ToLower(),
                Price = p.Price.ToString("f2"),
                Quantity = p.Quantity,
                ProductType = p.ProductType.ToString(),
                CreatedOn = p.CreatedOn.ToString("d")
            })
            .ToArrayAsync();

        var totalPages = (int)Math.Ceiling(await productsQuery.CountAsync() / (double)queryModel.ProductsPerPage);

        queryModel.TotalPages = totalPages;

        queryModel.Products = products;

        return queryModel;
    }

    public async Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(string id)
    {
        var model = await _dbContext
            .Products
            .Where(p => p.Id.ToString() == id)
            .Select(p => new ProductDetailsViewModel()
            {
                Name = p.Name,
                ImageId = p.ImageId,
                Price = p.Price.ToString("f2"),
                Quantity = p.Quantity,
                CompanyName = p.Company.Name,
                CompanyId = p.CompanyId.ToString(),
            })
            .FirstAsync();

        return model;
    }

    public async Task<Product?> GetProductByIdAsync(string id)
    {
        var product = await _dbContext
            .Products
            .Include(p => p.Scooter)
            .Include(p => p.Bike)
            .Include(p => p.Accessory)
            .Where(p => p.Id.ToString() == id && !p.IsDeleted)
            .FirstOrDefaultAsync();

        return product;
    }

    public async Task<ProductEditViewModel> GetProductEditViewModelByIdAsync(string id)
    {
        var model = await _dbContext
            .Products
            .Where(p => p.Id.ToString() == id)
            .Select(p => new ProductEditViewModel()
            {
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                ImageId = p.ImageId,
            })
            .FirstAsync();

        return model;
    }

    public async Task<string> GetImageIdByProductIdAsync(string id)
    {
        var imageId = await _dbContext
            .Products
            .Where(p => p.Id.ToString() == id)
            .Select(p => p.ImageId)
            .FirstAsync();

        return imageId;
    }

    public async Task EditAsync(string id, ProductEditViewModel modelProduct)
    {
        var product = await _dbContext
            .Products
            .Where(p => p.Id.ToString() == id)
            .FirstAsync();

        product.Name = modelProduct.Name;
        product.Price = modelProduct.Price;
        product.Quantity = modelProduct.Quantity;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<ProductsHomeModel> GetProductsForHomeAsync()
    {
        var productsModel = new ProductsHomeModel();

        productsModel.ScooterProducts = await _dbContext
            .Products
            .Where(p => p.ProductType == ProductType.Scooter && !p.IsDeleted)
            .OrderByDescending(p => p.CreatedOn)
            .Take(3)
            .Select(p => new ProductViewModel()
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                CompanyId = p.CompanyId.ToString(),
                ImageId = p.ImageId,
                Price = p.Price.ToString("f2"),
                Quantity = p.Quantity,
            })
            .ToArrayAsync();

        productsModel.BikeProducts = await _dbContext
            .Products
            .Where(p => p.ProductType == ProductType.Bike && !p.IsDeleted)
            .OrderByDescending(p => p.CreatedOn)
            .Take(3)
            .Select(p => new ProductViewModel()
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                CompanyId = p.CompanyId.ToString(),
                ImageId = p.ImageId,
                Price = p.Price.ToString("f2"),
                Quantity = p.Quantity,
            })
            .ToArrayAsync();

        productsModel.AccessoryProducts = await _dbContext
            .Products
            .Where(p => p.ProductType == ProductType.Accessory && !p.IsDeleted)
            .OrderByDescending(p => p.CreatedOn)
            .Take(3)
            .Select(p => new ProductViewModel()
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                CompanyId = p.CompanyId.ToString(),
                ImageId = p.ImageId,
                Price = p.Price.ToString("f2"),
                Quantity = p.Quantity,
            })
            .ToArrayAsync();

        return productsModel;
    }

    public async Task<bool> ProductExistsAsync(string id)
    {
        var productExists = await _dbContext
            .Products
            .AnyAsync(p => p.Id.ToString() == id && !p.IsDeleted);

        return productExists;
    }

    public async Task DecreaseQuantityAsync(string orderId)
    {
       IEnumerable<OrderItem> orderItems = await _dbContext
           .OrderItems
           .Where(oi => oi.OrderId.ToString() == orderId)
           .ToArrayAsync();

        var productIds = orderItems
            .Select(oi => oi.ProductId.ToString())
            .ToArray();

        IEnumerable<Product> products = await _dbContext
            .Products
            .Where(p => productIds.Contains(p.Id.ToString()))
            .ToArrayAsync();

       foreach (var product in products)
       {
           var orderItem = orderItems
               .First(oi => oi.ProductId.ToString() == product.Id.ToString());

           product.Quantity -= orderItem.Quantity;
       }
       
       await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> HasProductQuantityAsync(string id)
    {
        var hasProductQuantity = await _dbContext
            .Products
            .AnyAsync(p => p.Id.ToString() == id && p.Quantity > 0);

        return hasProductQuantity;
    }

    public async Task<bool> IsUserAuthorizedAsync(string id, string companyId)
    {
        var isUserAuthorized = await _dbContext
            .Products
            .AnyAsync(p => p.Id.ToString() == id && p.CompanyId.ToString() == companyId);

        return isUserAuthorized;
    }

    public async Task DeleteAsync(string id)
    {
        var product = await _dbContext
            .Products
            .Where(p => p.Id.ToString() == id)
            .FirstAsync();

        product.IsDeleted = true;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAllProductsByCompanyIdAsync(string companyId)
    {
        IEnumerable<Product> products = await _dbContext            
            .Products
            .Where(p => p.CompanyId.ToString() == companyId)
            .ToArrayAsync();

        foreach (var product in products)
        { 
            product.IsDeleted = true;
        }
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task RestoreAllProductsByCompanyIdAsync(string id)
    {
        IEnumerable<Product> products = await _dbContext
            .Products
            .Where(p => p.CompanyId.ToString() == id)
            .ToArrayAsync();

        foreach (var product in products)
        {
            product.IsDeleted = false;
        }
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductAdminAllViewModel>> GetAllProductsForAdminAsync()
    {
        IEnumerable<ProductAdminAllViewModel> products = await _dbContext
            .Products
            .Select(p => new ProductAdminAllViewModel()
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                Price = p.Price.ToString("f2"),
                Quantity = p.Quantity.ToString(),
                CreatedOn = p.CreatedOn.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                ProductType = p.ProductType.ToString(),
                Status = p.IsDeleted ? "Deleted" : "Active",
            })
            .ToArrayAsync();

        return products;
    }

    public async Task<bool> ProductExistsForAdminAsync(string id)
    {
        var productExists = await _dbContext
            .Products
            .AnyAsync(p => p.Id.ToString() == id);

        return productExists;
    }

    public async Task RestoreProductAsync(string id)
    {
        var product = await _dbContext
            .Products
            .Where(p => p.Id.ToString() == id)
            .FirstAsync();

        product.IsDeleted = false;

        await _dbContext.SaveChangesAsync();
    }
}
