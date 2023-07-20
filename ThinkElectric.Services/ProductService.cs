namespace ThinkElectric.Services;

using Contracts;
using Data;
using Data.Models;
using Data.Models.Enums.Product;
using Web.ViewModels.Product;

public class ProductService : IProductService
{
    private readonly ThinkElectricDbContext _dvContext;

    public ProductService(ThinkElectricDbContext dvContext)
    {
        _dvContext = dvContext;
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

        await _dvContext.Products.AddAsync(product);
        await _dvContext.SaveChangesAsync();

        return product.Id.ToString();
    }
}
