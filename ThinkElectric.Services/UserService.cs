namespace ThinkElectric.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

using Contracts;
using Data.Models;
using Web.ViewModels.User;

using static Common.GeneralApplicationConstants;

public class UserService : IUserService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
    {
        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddClaimAsync(user, new Claim("FullName", $"{user.FirstName} {user.LastName}"));
        }

        return result;
    }

    public async Task<SignInResult> SignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
    {
        var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);

        return result;
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }

    public async Task<ApplicationUser?> GetUserByEmailWithCartAndCompany(string email)
    {
        var user = await _userManager
            .Users
            .Include(u => u.Cart)
            .Include(u => u.Company)
            .FirstOrDefaultAsync(u => u.UserName == email && !u.IsBlocked);

        return user;
    }

    public async Task AddClaimAsync(string userId, string key, string value)
    {
        var user = await _userManager.FindByIdAsync(userId);

        await _userManager.AddClaimAsync(user, new Claim(key, value));
    }

    public async Task<bool> IsRegisteredAsCompanyAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        var claims = await _userManager.GetClaimsAsync(user);

        var isRegisteredAsCompany = claims.Any(c => c.Type == "companyUser");

        return isRegisteredAsCompany;
    }

    public async Task<bool> UserExistsByIdAsync(string userId)
    {
        var user = await _userManager
            .Users
            .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

        if (user == null)
        {
            return false;
        }

        return true;
    }

    public async Task<string?> GetAddressIdByUserIdAsync(string userId)
    {
        var addressId = await _userManager
            .Users
            .Where(u => u.Id.ToString() == userId)
            .Select(u => u.AddressId.ToString())
            .FirstOrDefaultAsync();

        return addressId;
    }

    public async Task<bool> UserHasAddressAsync(string userId)
    {
        var isUserHasAddress = await _userManager
            .Users
            .AnyAsync(u => u.Id.ToString() == userId && !string.IsNullOrWhiteSpace(u.AddressId.ToString()));

        return isUserHasAddress;
    }

    public async Task AddAddressToUserAsync(string userId, string addressId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        user.AddressId = Guid.Parse(addressId);

        await _userManager.UpdateAsync(user);
    }

    public async Task BlockUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        user.IsBlocked = true;

        await _userManager.UpdateAsync(user);
    }

    public async Task UnblockUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        user.IsBlocked = false;

        await _userManager.UpdateAsync(user);
    }

    public async Task<IEnumerable<UserAdminAllViewModel>> GetAllUsersForAdminAsync()
    {
        var adminUsers = await _userManager.GetUsersInRoleAsync(AdminRoleName);

        var adminUserIdSet = adminUsers.Select(u => u.Id);

        IEnumerable<UserAdminAllViewModel> users = await _userManager
            .Users
            .Where(u => !adminUserIdSet.Contains(u.Id))
            .Select(u => new UserAdminAllViewModel()
            {
                Id = u.Id.ToString(),
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                UserType = u.Company == null ? "Buyer" : "Company",
                Status = u.IsBlocked ? "Blocked" : "Active"
            })
            .ToArrayAsync();

        return users;
    }

    public async Task<bool> IsUserRegisteredAsCompanyAsync(string id)
    {
        var isUserRegisteredAsCompany = await _userManager
            .Users
            .AnyAsync(u => u.Id.ToString() == id && u.Company != null);

        return isUserRegisteredAsCompany;
    }

    public async Task<string> GetCompanyIdByUserIdAsync(string id)
    {
        var companyId = await _userManager
            .Users
            .Where(u => u.Id.ToString() == id)
            .Select(u => u.Company!.Id.ToString())
            .FirstAsync();

        return companyId;
    }

    public async Task<IdentityResult> RegisterAdminAsync(RegisterAdminViewModel model)
    {
        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, AdminRoleName);

            await _userManager.AddClaimAsync(user, new Claim("FullName", $"{user.FirstName} {user.LastName}"));
        }

        return result;
    }
}
