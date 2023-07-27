namespace ThinkElectric.Web.ViewModels.Bike;

using System.ComponentModel.DataAnnotations;
using static Common.EntityValidationConstants.BikeQuery;

public class BikeAllQueryModel
{
    [MaxLength(SearchTermMaxLength)]
    [Display(Name = "Search")]
    public string? SearchTerm { get; set; }
}
