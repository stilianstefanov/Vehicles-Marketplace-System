namespace ThinkElectric.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using Web.ViewModels.Company;

    public class CompanyService : ICompanyService
    {
        private readonly ThinkElectricDbContext _dbContext;

        public CompanyService(ThinkElectricDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(CompanyCreateViewModel model, string imageId, Address address, string userId)
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
                Address = address,
                UserId = Guid.Parse(userId)
            };

            await _dbContext.Companies.AddAsync(company);
            await _dbContext.SaveChangesAsync();
        }
    }
}
