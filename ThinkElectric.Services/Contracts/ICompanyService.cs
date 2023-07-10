namespace ThinkElectric.Services.Contracts;

using Data.Models;
using Web.ViewModels.Company;

public interface ICompanyService
{
    Task CreateAsync(CompanyCreateViewModel model, string imageId, Address address, string userId);
    Task<CompanyCreateViewModel> GetCompanyCreateViewModelAsync(string id);
    Task<CompanyDetailsViewModel?> GetCompanyDetailsAsync(string id);
}
