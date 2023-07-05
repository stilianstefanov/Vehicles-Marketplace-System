namespace ThinkElectric.Web.ViewModels.User;

using System.ComponentModel.DataAnnotations;
using static ThinkElectric.Common.EntityValidationConstants.User;
using static ThinkElectric.Common.EntityValidationErrors.User;

public class RegisterViewModel
{

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = PasswordConfirmationErrorMessage)]
    public string ConfirmPassword { get; set; } = null!;

    [Required]
    [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;

    [Required]
    [RegularExpression(PhoneNumberRegex, ErrorMessage = PhoneNumberErrorMessage)]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = null!;

    public bool IsCompany { get; set; }
}
