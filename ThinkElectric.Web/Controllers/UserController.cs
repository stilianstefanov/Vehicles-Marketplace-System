namespace ThinkElectric.Web.Controllers;

using System.Security.Claims;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ViewModels.User;

public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ICartService _cartService;

    public UserController(

        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager,
        ICartService cartService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _cartService = cartService;
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

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        await _userManager.AddClaimAsync(user, new Claim("FullName", $"{user.FirstName} {user.LastName}"));

        if (result.Succeeded)
        {
            await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (model.IsCompany)
            {
                return RedirectToAction("Create", "Company");
            }

            await _cartService.CreateAsync(user.Id);

            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByNameAsync(model.Email);

        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");

        return View(model);
    }
}
