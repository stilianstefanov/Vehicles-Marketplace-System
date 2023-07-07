namespace ThinkElectric.Web.ViewModels.Address;

using System.ComponentModel.DataAnnotations;
using static ThinkElectric.Common.EntityValidationConstants.Address;

public class AddressCreateViewModel
{
    [Required]
    [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
    public string Country { get; set; } = null!;

    [Required]
    [StringLength(CityMaxLength, MinimumLength = CityMinLength)]
    public string City { get; set; } = null!;

    [Required]
    [StringLength(StreetMaxLength, MinimumLength = StreetMinLength)]
    public string Street { get; set; } = null!;

    [Required]
    [StringLength(ZipCodeMaxLength, MinimumLength = ZipCodeMinLength)]
    public string ZipCode { get; set; } = null!;
}
