﻿namespace ThinkElectric.Services;

using Contracts;
using Data;
using Data.Models;
using Data.Models.Enums.Product;
using Microsoft.EntityFrameworkCore;
using ThinkElectric.Web.ViewModels.Product.Enums;
using Web.ViewModels.Product;

public class ProductService : IProductService
{
    private readonly ThinkElectricDbContext _dbContext;

    public ProductService(ThinkElectricDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<string> CreateAsync(ProductCreateViewModel modelProduct, string companyId, string imageId)
    {
        Product product = new Product()
        {
            Name = modelProduct.Name,
            ImageId = imageId,
            Price = modelProduct.Price,
            Quantity = modelProduct.Quantity,
            CreatedOn = DateTime.UtcNow,
            ProductType = ProductType.Scooter,
            CompanyId = Guid.Parse(companyId)
        };

        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();

        return product.Id.ToString();
    }

    public async Task<ProductAllQueryModel> AllByCompanyIdAsync(ProductAllQueryModel queryModel)
    {
        IQueryable<Product> productsQuery = _dbContext.Products
            .Where(p => p.CompanyId.ToString() == queryModel.CompanyId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            string wildCardSearchTerm = $"%{queryModel.SearchTerm.ToLower()}%";

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
                Price = p.Price.ToString("f2"),
                Quantity = p.Quantity,
                ProductType = p.ProductType.ToString(),
                CreatedOn = p.CreatedOn.ToString("d")
            })
            .ToArrayAsync();

        int totalPages = (int)Math.Ceiling(await productsQuery.CountAsync() / (double)queryModel.ProductsPerPage);

        queryModel.TotalPages = totalPages;

        queryModel.Products = products;

        return queryModel;
    }
}