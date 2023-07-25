namespace ThinkElectric.Services.Contracts;

using Data.Models;
using Data.Models.Enums.Product;
using Web.ViewModels.Product;

public interface IProductService
{
    Task<string> CreateAsync(ProductCreateViewModel modelProduct, string companyId, string imageId, ProductType productType);
    Task<ProductAllQueryModel> AllByCompanyIdAsync(ProductAllQueryModel queryModel);
    Task<ProductViewModel> GetProductDetailsByIdAsync(string id);
    Task<Product?> GetProductByIdAsync(string id);
}
