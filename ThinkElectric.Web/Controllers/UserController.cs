namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ViewModels.User;

using static Common.NotificationsMessagesConstants;
using static Common.ErrorMessages;
using static Common.GeneralMessages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

public class UserController : Controller
{
    private readonly ICartService _cartService;
    private readonly IUserService _userService;

    public UserController(ICartService cartService, IUserService userService)
    {
        _cartService = cartService;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var resultRegister = await _userService.RegisterAsync(model);

        if (resultRegister.Succeeded)
        {
            var user = await _userService.GetUserByEmailAsync(model.Email);

            if (model.IsCompany)
            {
                await _userService.AddClaimAsync(user!.Id.ToString(), "companyUser", "");

                return RedirectToAction("Create", "Company", new { id = user!.Id.ToString() });
            }

            var cartId = await _cartService.CreateAsync(user!.Id);

            await _userService.AddClaimAsync(user.Id.ToString(), "cartId", cartId.ToString());

            await _userService.SignInAsync(user, model.Password, false, false);

            TempData[SuccessMessage] = RegistrationSuccessMessage;

            return RedirectToAction("Index", "Home");
        }

        foreach (var error in resultRegister.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    [HttpGet]
    public async  Task<IActionResult> Login(string? returnUrl = null)
    {
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        LoginViewModel model = new LoginViewModel()
        {
            ReturnUrl = returnUrl
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) 
            return View(model);

        var user = await _userService.GetUserByEmailWithCartAndCompany(model.Email);

        if (user != null)
        {
            if (user.Cart == null && user.Company == null)
            {
                TempData[WarningMessage] = UncompletedRegistrationErrorMessage;

                return RedirectToAction("Create", "Company", new { id = user.Id.ToString() });
            }

            var result = await _userService.SignInAsync(user, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "/Home/Index");
            }
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _userService.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
}
