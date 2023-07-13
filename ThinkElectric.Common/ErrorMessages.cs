namespace ThinkElectric.Common;

public static class ErrorMessages
{
    public const string PhoneNumberErrorMessage = "The phone number must be in the format +359 888 888 888 or 0888 888 888.";

    public const string PasswordConfirmationErrorMessage = "The password and confirmation password do not match.";

    public const string DateErrorMessage = "Please enter valid date in the specific format.";

    public const string ImageRequiredErrorMessage = "Please upload image in the format .jpg, .jpeg, or .png";

    public const string ImageFormatErrorMessage = "Image must be a .jpg, .jpeg, or .png file.";


    public const string UncompletedRegistrationErrorMessage = "Please finish you registration to continue!";

    public const string UnexpectedErrorMessage = "An unexpected error occurred while proceeding you request. Please try again later or contact administrator";

    public const string CompanyNotFoundErrorMessage = "Company with the provided Id does not exist. Please try again!";

    public const string AlreadyRegisteredAsCompanyErrorMessage = "You are already registered as a company!";

    public const string NotRegisteredAsCompanyErrorMessage = "You are not registered as a company!";
}
