namespace ThinkElectric.Services;

using Contracts;
using Data;
using Data.Models;
using Web.ViewModels.Address;

public class AddressService : IAddressService
{
    private readonly ThinkElectricDbContext _dbContext;

    public AddressService(ThinkElectricDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Address> CreateAsync(AddressCreateViewModel modelAddress)
    {
        Address address = new Address()
        {
            Street = modelAddress.Street,
            City = modelAddress.City,
            ZipCode = modelAddress.ZipCode,
            Country = modelAddress.Country
        };

        await _dbContext.Addresses.AddAsync(address);
        await _dbContext.SaveChangesAsync();

        return address;
    }
}
