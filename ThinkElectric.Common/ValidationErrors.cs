namespace ThinkElectric.Common;

public static class ValidationErrors
{
    public const string PhoneNumberErrorMessage = "The phone number must be in the format +359 888 888 888 or 0888 888 888.";

    public const string PasswordConfirmationErrorMessage = "The password and confirmation password do not match.";

    public const string DateErrorMessage = "Please enter valid date in the specific format.";

    public const string ImageRequiredErrorMessage = "Please upload image in the format .jpg, .jpeg, or .png";

    public const string ImageFormatErrorMessage = "Image must be a .jpg, .jpeg, or .png file.";
}
