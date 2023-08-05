namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Address;

public interface IAddressService
{
    Task<string> CreateAsync(AddressCreateViewModel modelAddress);
    Task<AddressViewModel?> GetAddressDetailsByIdAsync(string id);
    Task<AddressEditViewModel> GetAddressEditByIdAsync(string id);
    Task EditAsync(string id, AddressEditViewModel modelAddress);
}
