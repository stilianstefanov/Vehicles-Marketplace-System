namespace ThinkElectric.Services.Contracts;

using Data.Models;
using Data.Models.Enums.Product;
using Web.ViewModels.Product;

public interface IProductService
{
    Task<string> CreateAsync(ProductCreateViewModel modelProduct, string companyId, string imageId, ProductType productType);
    Task<ProductAllQueryModel> AllByCompanyIdAsync(ProductAllQueryModel queryModel);
    Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(string id);
    Task<Product?> GetProductByIdAsync(string id);
    Task<ProductEditViewModel> GetProductEditViewModelByIdAsync(string id);
    Task<string> GetImageIdByProductIdAsync(string id);
    Task EditAsync(string id, ProductEditViewModel modelProduct);
    Task<ProductsHomeModel> GetProductsForHomeAsync();
    Task<bool> ProductExistsAsync(string id);
    Task DecreaseQuantityAsync(string orderId);
    Task<bool> HasProductQuantityAsync(string id);
    Task<bool> IsUserAuthorizedAsync(string id, string companyId);
    Task DeleteAsync(string id);
    Task DeleteAllProductsByCompanyIdAsync(string companyId);
    Task RestoreAllProductsByCompanyIdAsync(string id);
    Task<IEnumerable<ProductAdminAllViewModel>> GetAllProductsForAdminAsync();
    Task<bool> ProductExistsForAdminAsync(string id);
    Task RestoreProductAsync(string id);
}
