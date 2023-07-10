namespace ThinkElectric.Web.ViewModels.Company;

using System.ComponentModel.DataAnnotations;
using Address;
using Microsoft.AspNetCore.Http;
using static ThinkElectric.Common.EntityValidationConstants.Company;
using static Common.ValidationErrors;

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

    public IFormFile? ImageFile { get; set; }

    public AddressCreateViewModel Address { get; set; } = null!;
}
