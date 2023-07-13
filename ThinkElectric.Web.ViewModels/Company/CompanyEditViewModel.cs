namespace ThinkElectric.Web.ViewModels.Company;

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Address;
using static ThinkElectric.Common.EntityValidationConstants.Company;
using static Common.ErrorMessages;

public class CompanyEditViewModel
{
    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [RegularExpression(PhoneNumberRegex, ErrorMessage = PhoneNumberErrorMessage)]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = null!;

    [Url]
    [MaxLength(WebSiteMaxLength)]
    public string? Website { get; set; }

    [Required]
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
    public string Description { get; set; } = null!;

    [Display(Name = "Founded Date")]
    public DateTime? FoundedDate { get; set; }

    public IFormFile? ImageFile { get; set; }

    public AddressEditViewModel Address { get; set; } = null!;
}
