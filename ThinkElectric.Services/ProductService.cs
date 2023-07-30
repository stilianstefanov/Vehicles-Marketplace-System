namespace ThinkElectric.Services;

using Contracts;
using Data;
using Data.Models;
using Data.Models.Enums.Product;
using Microsoft.EntityFrameworkCore;
using ThinkElectric.Web.ViewModels.Product.Enums;
using Web.ViewModels.CartItem;
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
        Product product = new Product()
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
                CompanyId = p.CompanyId.ToString().ToLower(),
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

    public async Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(string id)
    {
        ProductDetailsViewModel model = await _dbContext
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
        Product? product = await _dbContext
            .Products
            .Include(p => p.Scooter)
            .Include(p => p.Bike)
            .Include(p => p.Accessory)
            .Where(p => p.Id.ToString() == id)
            .FirstOrDefaultAsync();

        return product;
    }

    public async Task<ProductEditViewModel> GetProductEditViewModelByIdAsync(string id)
    {
        ProductEditViewModel model = await _dbContext
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
        string imageId = await _dbContext
            .Products
            .Where(p => p.Id.ToString() == id)
            .Select(p => p.ImageId)
            .FirstAsync();

        return imageId;
    }

    public async Task EditAsync(string id, ProductEditViewModel modelProduct)
    {
        Product product = await _dbContext
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
        ProductsHomeModel productsModel = new ProductsHomeModel();

        productsModel.ScooterProducts = await _dbContext
            .Products
            .Where(p => p.ProductType == ProductType.Scooter)
            .OrderByDescending(p => p.CreatedOn)
            .Take(3)
            .Select(p => new ProductViewModel()
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                ImageId = p.ImageId,
                Price = p.Price.ToString("f2"),
                Quantity = p.Quantity,
            })
            .ToArrayAsync();

        productsModel.BikeProducts = await _dbContext
            .Products
            .Where(p => p.ProductType == ProductType.Bike)
            .OrderByDescending(p => p.CreatedOn)
            .Take(3)
            .Select(p => new ProductViewModel()
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                ImageId = p.ImageId,
                Price = p.Price.ToString("f2"),
                Quantity = p.Quantity,
            })
            .ToArrayAsync();

        productsModel.AccessoryProducts = await _dbContext
            .Products
            .Where(p => p.ProductType == ProductType.Accessory)
            .OrderByDescending(p => p.CreatedOn)
            .Take(3)
            .Select(p => new ProductViewModel()
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                ImageId = p.ImageId,
                Price = p.Price.ToString("f2"),
                Quantity = p.Quantity,
            })
            .ToArrayAsync();

        return productsModel;
    }

    public async Task<bool> ProductExistsAsync(string id)
    {
        bool productExists = await _dbContext
            .Products
            .AnyAsync(p => p.Id.ToString() == id);

        return productExists;
    }

    public async Task DecreaseQuantityAsync(string orderId)
    {
       IEnumerable<OrderItem> orderItems = await _dbContext
           .OrderItems
           .Where(oi => oi.OrderId.ToString() == orderId)
           .ToArrayAsync();

       IEnumerable<Product> products = await _dbContext
           .Products
           .Where(p => orderItems.Any(oi => oi.ProductId == p.Id))
           .ToArrayAsync();

       foreach (Product product in products)
       {
           OrderItem orderItem = orderItems
               .First(oi => oi.ProductId == product.Id);

           product.Quantity -= orderItem.Quantity;
       }
       
       await _dbContext.SaveChangesAsync();
    }
}
