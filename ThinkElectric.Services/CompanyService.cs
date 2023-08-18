namespace ThinkElectric.Services
{
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Web.ViewModels.Address;
    using Web.ViewModels.Company;
    using Web.ViewModels.Company.Enums;

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
            var company = new Company()
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
            var user = await _userManager.FindByIdAsync(id);

            var model = new CompanyCreateViewModel()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return model;
        }

        public async Task<CompanyDetailsViewModel?> GetCompanyDetailsByIdAsync(string id)
        {
            var model = await _dbContext
                .Companies
                .Where(c => c.Id.ToString() == id && !c.IsBlocked)
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
            var hasCompany = await _dbContext.
                Companies.
                AnyAsync(c => c.UserId.ToString() == id);

            return hasCompany;
        }

        public async Task<bool> CompanyExistsByIdAsync(string id)
        {
            var hasCompany = await _dbContext.
                Companies.
                AnyAsync(c => c.Id.ToString() == id && !c.IsBlocked);

            return hasCompany;
        }

        public async Task<bool> IsUserCompanyOwnerAsync(string companyId, string userId)
        {
            var isOwner = await _dbContext.
                Companies.
                AnyAsync(c => c.Id.ToString() == companyId && c.UserId.ToString() == userId);

            return isOwner;
        }

        public async Task<CompanyEditViewModel> GetCompanyEditViewModelByIdAsync(string id)
        {
            var model = await _dbContext
                .Companies
                .Where(c => c.Id.ToString() == id && !c.IsBlocked)
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
            var imageId = await _dbContext
                .Companies
                .Where(c => c.Id.ToString() == id)
                .Select(c => c.ImageId)
                .FirstAsync();

            return imageId;
        }

        public async Task<string> GetAddressIdByCompanyIdAsync(string id)
        {
            var addressId = await _dbContext
                .Companies
                .Where(c => c.Id.ToString() == id)
                .Select(c => c.AddressId.ToString())
                .FirstAsync();

            return addressId;
        }

        public async Task EditAsync(CompanyEditViewModel model, string id)
        {
            var company = await _dbContext
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

        public async Task<CompaniesAllQueryModel> AllAsync(CompaniesAllQueryModel queryModel)
        {
            var companiesQuery = _dbContext
                .Companies
                .Where(c => !c.IsBlocked)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
            {
                var wildCardSearchTerm = $"%{queryModel.SearchTerm.ToLower()}%";

                companiesQuery = companiesQuery
                    .Where(c => EF.Functions.Like(c.Name, wildCardSearchTerm) ||
                                EF.Functions.Like(c.Description, wildCardSearchTerm) ||
                                EF.Functions.Like(c.Email, wildCardSearchTerm ));
            }

            companiesQuery = queryModel.CompanySorting switch
            {
                CompanySorting.NameAscending => companiesQuery
                    .OrderBy(c => c.Name),
                CompanySorting.NameDescending => companiesQuery
                    .OrderByDescending(c => c.Name),
                CompanySorting.FoundedDateAscending => companiesQuery
                    .OrderBy(c => c.FoundedDate),
                CompanySorting.FoundedDateDescending => companiesQuery
                    .OrderByDescending(c => c.FoundedDate),
                _ => companiesQuery.OrderBy(c => c.Name)
            };

            IEnumerable<CompanyAllViewModel> allCompanies = await companiesQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.CompaniesPerPage)
                .Take(queryModel.CompaniesPerPage)
                .Select(c => new CompanyAllViewModel()
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Website = c.Website,
                    FoundedDate = c.FoundedDate.ToString("d"),
                    ImageId = c.ImageId,
                })
                .ToArrayAsync();

            var totalPages = (int)Math.Ceiling(await companiesQuery.CountAsync() / (double)queryModel.CompaniesPerPage);

            queryModel.TotalPages = totalPages;

            queryModel.Companies = allCompanies;

            return queryModel;
        }

        public async Task<IEnumerable<CompanyAdminAllViewModel>> GetAllCompaniesForAdminAsync()
        {
            IEnumerable<CompanyAdminAllViewModel> companies = await _dbContext
                .Companies
                .Select(c => new CompanyAdminAllViewModel()
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    UserFullName = c.User.FirstName + " " + c.User.LastName,
                    UserEmail = c.User.Email,
                    Status = c.IsBlocked ? "Blocked" : "Active",
                    Address = new AddressViewModel()
                    {
                        City = c.Address.City,
                        Country = c.Address.Country,
                        Street = c.Address.Street,
                        ZipCode = c.Address.ZipCode
                    }
                })
                .ToArrayAsync();

            return companies;
        }

        public async Task<string> BlockCompanyByIdAsync(string id)
        {
            var company = await _dbContext
                .Companies
                .Where(c => c.Id.ToString() == id)
                .FirstAsync();

            company.IsBlocked = true;

            await _dbContext.SaveChangesAsync();

            return company.UserId.ToString();
        }

        public async Task<bool> CompanyExistsByIdForAdminAsync(string id)
        {
            var hasCompany = await _dbContext.
                Companies.
                AnyAsync(c => c.Id.ToString() == id);

            return hasCompany;
        }

        public async Task<string> UnblockCompanyByIdAsync(string id)
        {
            var company = await _dbContext
                .Companies
                .Where(c => c.Id.ToString() == id)
                .FirstAsync();

            company.IsBlocked = false;

            await _dbContext.SaveChangesAsync();

            return company.UserId.ToString();
        }
    }
}
