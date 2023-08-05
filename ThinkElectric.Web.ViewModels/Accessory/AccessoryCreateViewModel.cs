namespace ThinkElectric.Web.ViewModels.Accessory;

using System.ComponentModel.DataAnnotations;

using Product;
using static ThinkElectric.Common.EntityValidationConstants.Accessory;

public class AccessoryCreateViewModel
{
    public ProductCreateViewModel Product { get; set; } = null!;

    [Required]
    [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
    public string Brand { get; set; } = null!;

    [Required]
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
    public string Description { get; set; } = null!;

    [Required]
    [StringLength(CompatibleBrandMaxLength, MinimumLength = CompatibleBrandMinLength)]
    public string CompatibleBrand { get; set; } = null!;

    [Required]
    [StringLength(CompatibleModelMaxLength, MinimumLength = CompatibleModelMinLength)]
    public string CompatibleModel { get; set; } = null!;
}
