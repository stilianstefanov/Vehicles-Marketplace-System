namespace ThinkElectric.Web.ViewModels.Company;

using Address;

public class CompanyDetailsViewModel
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Website { get; set; }

    public string Description { get; set; } = null!;

    public DateTime FoundedDate { get; set; }

    public Byte[] ImageData { get; set; } = null!;

    public string ImageType { get; set; } = null!;

    public AddressViewModel Address { get; set; } = null!;
}
