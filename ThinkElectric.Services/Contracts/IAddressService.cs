namespace ThinkElectric.Services.Contracts;

using Data.Models;
using Web.ViewModels.Address;

public interface IAddressService
{
    Task<string> CreateAsync(AddressCreateViewModel modelAddress);

    Task<AddressViewModel> GetAddressByIdAsync(string id);
}
