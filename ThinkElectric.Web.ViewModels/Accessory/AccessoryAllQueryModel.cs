namespace ThinkElectric.Web.ViewModels.Accessory;

using System.ComponentModel.DataAnnotations;
using Enums;
using static Common.EntityValidationConstants.AccessoryQuery;
using static Common.GeneralApplicationConstants;

public class AccessoryAllQueryModel
{
    public AccessoryAllQueryModel()
    {
        this.CurrentPage = DefaultPage;
        this.AccessoriesPerPage = DefaultItemsPerPage;

        Accessories = new HashSet<AccessoryAllViewModel>();
    }

    [MaxLength(SearchTermMaxLength)]
    [Display(Name = "Search")]
    public string? SearchTerm { get; set; }

    [Range(SortingMinValue, SortingMaxValue)]
    public AccessorySorting AccessorySorting { get; set; }

    public int CurrentPage { get; set; }

    [Range(PerPageMinValue, PerPageMaxValue)]
    public int AccessoriesPerPage { get; set; }

    public int TotalPages { get; set; }

    public IEnumerable<AccessoryAllViewModel> Accessories { get; set; }
}
