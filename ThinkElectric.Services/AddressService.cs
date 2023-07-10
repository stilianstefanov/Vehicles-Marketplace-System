namespace ThinkElectric.Services;

using Contracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
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

    public async Task<AddressViewModel> GetAddressByCompanyIdAsync(string companyId)
    {
        AddressViewModel model = await _dbContext
            .Addresses
            .Where(a => a.CompanyId.ToString() == companyId)
            .Select(a => new AddressViewModel()
            {
                Street = a.Street,
                City = a.City,
                ZipCode = a.ZipCode,
                Country = a.Country
            })
            .FirstAsync();

        return model;
    }
}
