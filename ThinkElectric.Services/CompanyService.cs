namespace ThinkElectric.Services
{
    using Contracts;
    using Data;
    using Web.ViewModels.Company;

    public class CompanyService : ICompanyService
    {
        private readonly ThinkElectricDbContext _dbContext;

        public CompanyService(ThinkElectricDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(CompanyCreateViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
