namespace ThinkElectric.Services.Contracts;

using Data.Models;
using Web.ViewModels.Address;

public interface IAddressService
{
    Task<string> CreateAsync(AddressCreateViewModel modelAddress);
    Task<AddressViewModel> GetAddressDetailsByIdAsync(string id);
    Task<AddressEditViewModel> GetAddressEditByIdAsync(string id);
}
