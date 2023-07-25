namespace ThinkElectric.Web.ViewModels.Product;

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static ThinkElectric.Common.EntityValidationConstants.Product;

public class ProductEditViewModel
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

    public string? ImageId { get; set; }

    public ImageViewModel? CurrentImage { get; set; }

    public IFormFile? NewImage { get; set; }
}
