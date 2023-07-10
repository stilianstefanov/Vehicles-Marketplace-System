namespace ThinkElectric.Services.Contracts;

using Data.Models;
using Web.ViewModels.Address;

public interface IAddressService
{
    Task<Address> CreateAsync(AddressCreateViewModel modelAddress);

    Task<AddressViewModel?> GetAddressByCompanyIdAsync(string companyId);
}
