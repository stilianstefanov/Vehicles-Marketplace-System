namespace ThinkElectric.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;
using ViewModels.User;

using static Common.GeneralApplicationConstants;
using static Common.NotificationsMessagesConstants;
using static Common.GeneralMessages;

public class UserController : BaseAdminController
{
    private readonly IUserService _userService;
    private readonly ICompanyService _companyService;
    private readonly IProductService _productService;
    private readonly IReviewService _reviewService;
    private readonly ICartService _cartService;

    public UserController(
        IUserService userService, 
        ICompanyService companyService, 
        IProductService productService,
        IReviewService reviewService,
        ICartService cartService)
    {
        _userService = userService;
        _companyService = companyService;
        _productService = productService;
        _reviewService = reviewService;
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        try
        {
            var users = await _userService.GetAllUsersForAdminAsync();

            return View(users);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Block(string id)
    {
        bool userExists = await _userService.UserExistsByIdAsync(id);

        if (!userExists)
        {
            return GeneralError();
        }

        try
        {
            bool isRegisteredAsCompany = await _userService.IsUserRegisteredAsCompanyAsync(id);

            if (isRegisteredAsCompany)
            {
                string companyId = await _userService.GetCompanyIdByUserIdAsync(id);

                await _companyService.BlockCompanyByIdAsync(companyId);

                await _productService.DeleteAllProductsByCompanyIdAsync(companyId);
            }
            else
            {
                await _reviewService.DeleteAllReviewsByUserIdAsync(id);
            }

            await _userService.BlockUserByIdAsync(id);

            return RedirectToAction("All", "User", new { Area = AdminAreaName });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Unblock(string id)
    {
        bool userExists = await _userService.UserExistsByIdAsync(id);

        if (!userExists)
        {
            return GeneralError();
        }

        try
        {
            bool isRegisteredAsCompany = await _userService.IsUserRegisteredAsCompanyAsync(id);

            if (isRegisteredAsCompany)
            {
                string companyId = await _userService.GetCompanyIdByUserIdAsync(id);

                await _companyService.UnblockCompanyByIdAsync(companyId);

                await _productService.RestoreAllProductsByCompanyIdAsync(companyId);
            }

            await _userService.UnblockUserByIdAsync(id);

            return RedirectToAction("All", "User", new { Area = AdminAreaName });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
    public IActionResult CreateAdmin()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdmin(RegisterAdminViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var resultRegister = await _userService.RegisterAdminAsync(model);

            if (resultRegister.Succeeded)
            {
                var user = await _userService.GetUserByEmailAsync(model.Email);

                var cartId = await _cartService.CreateAsync(user!.Id);

                await _userService.AddClaimAsync(user.Id.ToString(), "cartId", cartId);

                TempData[SuccessMessage] = AdminRegistrationSuccessMessage;

                return RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }

            foreach (var error in resultRegister.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);

        }
        catch (Exception)
        {
            return GeneralError();
        }
    }
}
