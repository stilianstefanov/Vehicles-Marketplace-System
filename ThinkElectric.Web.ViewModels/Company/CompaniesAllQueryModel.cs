namespace ThinkElectric.Web.ViewModels.Company;

using System.ComponentModel.DataAnnotations;
using Enums;

using static Common.GeneralApplicationConstants;
using static Common.EntityValidationConstants.CompanyQuery;

public class CompaniesAllQueryModel
{
    public CompaniesAllQueryModel()
    {
        this.CurrentPage = DefaultPage;
        this.CompaniesPerPage = DefaultItemsPerPage;

        Companies = new HashSet<CompanyAllViewModel>();
    }

    [MaxLength(SearchTermMaxLength)]
    [Display(Name = "Search")]
    public string? SearchTerm { get; set; }

    [Range(SortingMinValue, SortingMaxValue)]
    public CompanySorting CompanySorting { get; set; }

    public int CurrentPage { get; set; }

    [Range(PerPageMinValue, PerPageMaxValue)]
    public int CompaniesPerPage { get; set; }

    public int TotalPages { get; set; }

    public IEnumerable<CompanyAllViewModel> Companies { get; set; }
}
