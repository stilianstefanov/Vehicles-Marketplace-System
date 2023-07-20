namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Product;

public interface IProductService
{
    Task<string> CreateAsync(ProductCreateViewModel modelProduct, string companyId, string imageId);
    Task<ProductAllQueryModel> AllByCompanyIdAsync(ProductAllQueryModel queryModel);
}
