namespace ThinkElectric.Services;

using Contracts;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Web.ViewModels.User;

public class UserService : IUserService
{

    public Task<IdentityResult> RegisterAsync(RegisterViewModel model)
    {
        throw new NotImplementedException();
    }
}
