namespace ThinkElectric.Services;

using System.Security.Claims;
using Contracts;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.User;

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
        ApplicationUser user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber
        };

        IdentityResult result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddClaimAsync(user, new Claim("FullName", $"{user.FirstName} {user.LastName}"));
        }

        return result;
    }

    public async Task<SignInResult> SignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
    {
        SignInResult result = await _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);

        return result;
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        ApplicationUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }

    public async Task<ApplicationUser?> GetUserByEmailWithCartAndCompany(string email)
    {
        ApplicationUser? user = await _userManager
            .Users
            .Include(u => u.Cart)
            .Include(u => u.Company)
            .FirstOrDefaultAsync(u => u.UserName == email);

        return user;
    }

    public async Task AddClaimAsync(string userId, string key, string value)
    {
        ApplicationUser user = await _userManager.FindByIdAsync(userId);

        await _userManager.AddClaimAsync(user, new Claim(key, value));
    }

    public async Task<bool> IsRegisteredAsCompanyAsync(string userId)
    {
        ApplicationUser user = await _userManager.FindByIdAsync(userId);

        IList<Claim> claims = await _userManager.GetClaimsAsync(user);

        bool isRegisteredAsCompany = claims.Any(c => c.Type == "companyUser");

        return isRegisteredAsCompany;
    }

    public async Task<bool> UserExistsByIdAsync(string userId)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return false;
        }

        return true;
    }

    public async Task<string?> GetAddressIdByUserIdAsync(string userId)
    {
        string? addressId = await _userManager
            .Users
            .Where(u => u.Id.ToString() == userId)
            .Select(u => u.AddressId.ToString())
            .FirstOrDefaultAsync();

        return addressId;
    }

    public async Task<bool> UserHasAddressAsync(string userId)
    {
        bool isUserHasAddress = await _userManager
            .Users
            .AnyAsync(u => u.Id.ToString() == userId && !string.IsNullOrWhiteSpace(u.AddressId.ToString()));

        return isUserHasAddress;
    }

    public async Task AddAddressToUserAsync(string userId, string addressId)
    {
        ApplicationUser user = await _userManager.FindByIdAsync(userId);

        user.AddressId = Guid.Parse(addressId);

        await _userManager.UpdateAsync(user);
    }
}
