namespace ThinkElectric.Services.Contracts;

using Microsoft.AspNetCore.Identity;
using Web.ViewModels.User;

public interface IUserService
{
    Task<IdentityResult> RegisterAsync(RegisterViewModel model);
}
