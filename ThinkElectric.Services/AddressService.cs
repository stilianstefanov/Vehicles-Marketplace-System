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


    public async Task<string> CreateAsync(AddressCreateViewModel modelAddress)
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

        return address.Id.ToString();
    }

    public async Task<AddressViewModel?> GetAddressDetailsByIdAsync(string id)
    {
        AddressViewModel? model = await _dbContext
            .Addresses
            .Where(a => a.Id.ToString() == id)
            .Select(a => new AddressViewModel()
            {
                Id = a.Id.ToString(),
                Street = a.Street,
                City = a.City,
                ZipCode = a.ZipCode,
                Country = a.Country
            })
            .FirstOrDefaultAsync();

        return model;
    }

    public async Task<AddressEditViewModel> GetAddressEditByIdAsync(string id)
    {
        AddressEditViewModel model = await _dbContext
            .Addresses
            .Where(a => a.Id.ToString() == id)
            .Select(a => new AddressEditViewModel()
            {
                Street = a.Street,
                City = a.City,
                ZipCode = a.ZipCode,
                Country = a.Country
            })
            .FirstAsync();

        return model;
    }

    public async Task EditAsync(string id, AddressEditViewModel modelAddress)
    {
        Address address = await _dbContext
            .Addresses
            .FirstAsync(a => a.Id.ToString() == id);

        address.Street = modelAddress.Street;
        address.City = modelAddress.City;
        address.ZipCode = modelAddress.ZipCode;
        address.Country = modelAddress.Country;

        await _dbContext.SaveChangesAsync();
    }
}
