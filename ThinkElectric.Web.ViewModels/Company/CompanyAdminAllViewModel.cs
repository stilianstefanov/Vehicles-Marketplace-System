namespace ThinkElectric.Web.ViewModels.Company;

using Address;

public class CompanyAdminAllViewModel
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public AddressViewModel Address { get; set; } = null!;

    public string UserFullName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string Status { get; set; } = null!;
}
