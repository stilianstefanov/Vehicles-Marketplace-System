namespace ThinkElectric.Web.ViewModels.Company;

using System.ComponentModel.DataAnnotations;
using static ThinkElectric.Common.EntityValidationConstants.Address;
using static ThinkElectric.Common.EntityValidationConstants.Company;
using static ThinkElectric.Common.EntityValidationErrors;

public class CompanyCreateViewModel
{
    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [RegularExpression(PhoneNumberRegex, ErrorMessage = PhoneNumberErrorMessage)]
    public string PhoneNumber { get; set; } = null!;

    [Url]
    [MaxLength(WebSiteMaxLength)]
    public string? Website { get; set; }

    [Required]
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
    public string Description { get; set; } = null!;

    public DateTime? FoundedDate { get; set; }

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
