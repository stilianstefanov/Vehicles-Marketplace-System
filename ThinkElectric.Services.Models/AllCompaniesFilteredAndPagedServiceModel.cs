namespace ThinkElectric.Services.Models;

using Web.ViewModels.Company;

public class AllCompaniesFilteredAndPagedServiceModel
{
    public AllCompaniesFilteredAndPagedServiceModel()
    {
        Companies = new HashSet<CompanyAllViewModel>();
    }

    public int TotalCompaniesCount { get; set; }

    public IEnumerable<CompanyAllViewModel> Companies { get; set; }
}
