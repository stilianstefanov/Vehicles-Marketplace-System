﻿namespace ThinkElectric.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
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

        public async Task<CompanyCreateViewModel> GetCompanyCreateViewModelAsync(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            CompanyCreateViewModel model = new CompanyCreateViewModel()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return model;
        }
    }
}