namespace ThinkElectric.Web.ViewModels.Company;

using Address;
using Review;

public class CompanyDetailsViewModel
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Website { get; set; }

    public string Description { get; set; } = null!;

    public DateTime FoundedDate { get; set; }

    public double Rating => Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;

    public string ImageId { get; set; } = null!;

    public ImageViewModel Image { get; set; } = null!;

    public AddressViewModel Address { get; set; } = null!;

    public IEnumerable<ReviewViewModel> Reviews { get; set; } = null!;
}
