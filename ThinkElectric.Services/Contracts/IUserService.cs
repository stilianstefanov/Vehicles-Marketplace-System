namespace ThinkElectric.Services.Contracts;

using Microsoft.AspNetCore.Identity;
using Web.ViewModels.User;

using Data.Models;

public interface IUserService
{
    Task<IdentityResult> RegisterAsync(RegisterViewModel model);
    Task<SignInResult> SignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure);
    Task SignOutAsync();
    Task<ApplicationUser?> GetUserByEmailAsync(string email);
    Task<ApplicationUser?> GetUserByEmailWithCartAndCompany(string email);
    Task AddClaimAsync(string userId, string key, string value);
    Task<bool> IsRegisteredAsCompanyAsync(string userId);
    Task<bool> UserExistsByIdAsync(string userId);
    Task<string?> GetAddressIdByUserIdAsync(string userId);
    Task<bool> UserHasAddressAsync(string userId);
    Task AddAddressToUserAsync(string userId, string addressId);
    Task BlockUserByIdAsync(string userId);
    Task UnblockUserByIdAsync(string userId);
    Task<IEnumerable<UserAdminAllViewModel>> GetAllUsersForAdminAsync();
    Task<bool> IsUserRegisteredAsCompanyAsync(string id);
    Task<string> GetCompanyIdByUserIdAsync(string id);
}
