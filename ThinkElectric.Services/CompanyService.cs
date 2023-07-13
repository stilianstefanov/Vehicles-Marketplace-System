namespace ThinkElectric.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Web.ViewModels.Company;

    public class CompanyService : ICompanyService
    {
        private readonly ThinkElectricDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public CompanyService(ThinkElectricDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<string> CreateAsync(CompanyCreateViewModel model, string imageId, string addressId, string userId)
        {
            Company company = new Company()
            {
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Website = model.Website,
                Description = model.Description,
                FoundedDate = model.FoundedDate!.Value,
                ImageId = imageId,
                AddressId = Guid.Parse(addressId),
                UserId = Guid.Parse(userId)
            };

            await _dbContext.Companies.AddAsync(company);
            await _dbContext.SaveChangesAsync();

            return company.Id.ToString();
        }

        public async Task<CompanyCreateViewModel> GetCompanyCreateViewModelByUserIdAsync(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            CompanyCreateViewModel model = new CompanyCreateViewModel()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return model;
        }

        public async Task<CompanyDetailsViewModel?> GetCompanyDetailsByIdAsync(string id)
        {
            CompanyDetailsViewModel? model = await _dbContext
                .Companies
                .Where(c => c.Id.ToString() == id)
                .Select(c => new CompanyDetailsViewModel()
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Website = c.Website,
                    Description = c.Description,
                    FoundedDate = c.FoundedDate,
                    ImageId = c.ImageId,
                    AddressId = c.AddressId.ToString(),

                })
                .FirstOrDefaultAsync();
                
                
            return model;
        }

        public async Task<bool> HasAlreadyCreatedCompanyAsync(string id)
        {
            bool hasCompany = await _dbContext.
                Companies.
                AnyAsync(c => c.UserId.ToString() == id);

            return hasCompany;
        }

        public async Task<bool> CompanyExistsByIdAsync(string id)
        {
            bool hasCompany = await _dbContext.
                Companies.
                AnyAsync(c => c.Id.ToString() == id);

            return hasCompany;
        }

        public async Task<bool> IsUserCompanyOwnerAsync(string companyId, string userId)
        {
            bool isOwner = await _dbContext.
                Companies.
                AnyAsync(c => c.Id.ToString() == companyId && c.UserId.ToString() == userId);

            return isOwner;
        }

        public async Task<CompanyEditViewModel> GetCompanyEditViewModelByIdAsync(string id)
        {
            CompanyEditViewModel model = await _dbContext
                .Companies
                .Where(c => c.Id.ToString() == id)
                .Select(c => new CompanyEditViewModel()
                {
                    Name = c.Name,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Website = c.Website,
                    Description = c.Description,
                    FoundedDate = c.FoundedDate,
                    ImageId = c.ImageId,
                    AddressId = c.AddressId.ToString(),

                })
                .FirstAsync();

            return model;
        }

        public async Task<string> GetImageIdByCompanyIdAsync(string id)
        {
            string imageId = await _dbContext
                .Companies
                .Where(c => c.Id.ToString() == id)
                .Select(c => c.ImageId)
                .FirstAsync();

            return imageId;
        }

        public async Task<string> GetAddressIdByCompanyIdAsync(string id)
        {
            string addressId = await _dbContext
                .Companies
                .Where(c => c.Id.ToString() == id)
                .Select(c => c.AddressId.ToString())
                .FirstAsync();

            return addressId;
        }

        public async Task EditAsync(CompanyEditViewModel model, string id)
        {
            Company company = await _dbContext
                .Companies
                .Where(c => c.Id.ToString() == id)
                .FirstAsync();

            company.Name = model.Name;
            company.Email = model.Email;
            company.PhoneNumber = model.PhoneNumber;
            company.Website = model.Website;
            company.Description = model.Description;
            company.FoundedDate = model.FoundedDate!.Value;

            await _dbContext.SaveChangesAsync();
        }
    }
}
