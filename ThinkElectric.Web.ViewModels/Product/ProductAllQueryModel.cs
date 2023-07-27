namespace ThinkElectric.Web.ViewModels.Product;

using System.ComponentModel.DataAnnotations;
using Enums;
using static Common.EntityValidationConstants.ProductQuery;
using static Common.GeneralApplicationConstants;

public class ProductAllQueryModel
{
    public ProductAllQueryModel()
    {
        this.CurrentPage = DefaultPage;
        this.ProductsPerPage = DefaultItemsPerPage;

        Products = new HashSet<ProductAllViewModel>();
    }

    public string? CompanyId { get; set; }

    [MaxLength(SearchTermMaxLength)]
    [Display(Name = "Search")]
    public string? SearchTerm { get; set; }

    [Range(SortingMinValue, SortingMaxValue)]
    public ProductSorting ProductSorting { get; set; }

    [Range(ProductTypeMinValue, ProductTypeMaxValue)]
    public QueryProductType QueryProductType { get; set; }

    public int CurrentPage { get; set; }

    [Range(PerPageMinValue, PerPageMaxValue)]
    public int ProductsPerPage { get; set; }

    public int TotalPages { get; set; }

    public IEnumerable<ProductAllViewModel> Products { get; set; }
}
