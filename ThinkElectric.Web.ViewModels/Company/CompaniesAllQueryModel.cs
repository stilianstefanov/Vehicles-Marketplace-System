namespace ThinkElectric.Web.ViewModels.Company;

using System.ComponentModel.DataAnnotations;
using Enums;

using static Common.GeneralApplicationConstants;

public class CompaniesAllQueryModel
{
    public CompaniesAllQueryModel()
    {
        this.CurrentPage = DefaultPage;
        this.CompaniesPerPage = DefaultCompaniesPerPage;

        Companies = new HashSet<CompanyAllViewModel>();
    }
    
    [Display(Name = "Search")]
    public string? SearchTerm { get; set; }

    public CompanySorting CompanySorting { get; set; }

    public int CurrentPage { get; set; }

    public int CompaniesPerPage { get; set; }

    public int TotalPages { get; set; }

    public IEnumerable<CompanyAllViewModel> Companies { get; set; }
}
