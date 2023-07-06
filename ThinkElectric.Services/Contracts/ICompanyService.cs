namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Company;

public interface ICompanyService
{
    Task CreateAsync(CompanyCreateViewModel model);
}
