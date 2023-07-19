namespace ThinkElectric.Web.ViewModels.Product;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using static ThinkElectric.Common.EntityValidationConstants.Product;


public class ProductCreateViewModel
{
    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
    public decimal Price { get; set; }

    [Required]
    [Range(QuantityMinValue, QuantityMaxValue)]
    public int Quantity { get; set; }

    public IFormFile? ImageFile { get; set; }
}
