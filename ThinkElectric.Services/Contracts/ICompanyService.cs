namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Company;

public interface ICompanyService
{
    Task<string> CreateAsync(CompanyCreateViewModel model, string imageId, string addressId, string userId);
    Task<CompanyCreateViewModel> GetCompanyCreateViewModelByUserIdAsync(string id);
    Task<CompanyDetailsViewModel?> GetCompanyDetailsByIdAsync(string id);
    Task<bool> HasAlreadyCreatedCompanyAsync(string id);
    Task<bool> CompanyExistsByIdAsync(string id);
    Task<bool> IsUserCompanyOwnerAsync(string companyId, string userId);
    Task<CompanyEditViewModel> GetCompanyEditViewModelByIdAsync(string id);
    Task<string> GetImageIdByCompanyIdAsync(string id);
    Task<string> GetAddressIdByCompanyIdAsync(string id);
    Task EditAsync(CompanyEditViewModel model, string id);
    Task<CompaniesAllQueryModel> AllAsync(CompaniesAllQueryModel queryModel);
    Task<IEnumerable<CompanyAdminAllViewModel>> GetAllCompaniesForAdminAsync();
    Task<string> BlockCompanyByIdAsync(string id);
    Task<bool> CompanyExistsByIdForAdminAsync(string id);
    Task<string> UnblockCompanyByIdAsync(string id);
}
