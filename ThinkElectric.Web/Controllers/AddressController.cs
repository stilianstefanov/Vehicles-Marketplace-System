namespace ThinkElectric.Web.Controllers;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;
using ViewModels.Address;
using Infrastructure.Extensions;

using static Common.ErrorMessages;
using static Common.NotificationsMessagesConstants;
using static Common.GeneralMessages;
using static Common.GeneralApplicationConstants;

[Authorize(Policy = BuyerOrAdminPolicyName)]
public class AddressController : BaseController
{
    private readonly IAddressService _addressService;
    private readonly IOrderService _orderService;
    private readonly IUserService _userService;

    public AddressController(
        IAddressService addressService,
        IOrderService orderService,
        IUserService userService)
    {
        _addressService = addressService;
        _orderService = orderService;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> CreateUserAddress(string id)
    {
        var isOrderExisting = await _orderService.IsOrderExisting(id);

        if (!isOrderExisting)
        {
            TempData[ErrorMessage] = OrderNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        var alreadyHasAddress = await _userService.UserHasAddressAsync(User.GetId()!);

        if (alreadyHasAddress)
        {
            TempData[ErrorMessage] = UserAlreadyHasAddressErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAddress(AddressCreateViewModel addressModel, string id)
    {
        var isOrderExisting = await _orderService.IsOrderExisting(id);

        if (!isOrderExisting)
        {
            TempData[ErrorMessage] = OrderNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        var alreadyHasAddress = await _userService.UserHasAddressAsync(User.GetId()!);

        if (alreadyHasAddress)
        {
            TempData[ErrorMessage] = UserAlreadyHasAddressErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            return View(addressModel);
        }

        try
        {
            var addressId = await _addressService.CreateAsync(addressModel);

            await _userService.AddAddressToUserAsync(User.GetId()!, addressId);

            TempData[SuccessMessage] = AddressCreatedSuccessMessage;

            return RedirectToAction("Details", "Order", new { id });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
    public async Task<IActionResult> EditUserAddress(string id)
    {
        var isOrderExisting = await _orderService.IsOrderExisting(id);

        if (!isOrderExisting)
        {
            TempData[ErrorMessage] = OrderNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        var hasAddress = await _userService.UserHasAddressAsync(User.GetId()!);

        if (!hasAddress)
        {
            TempData[ErrorMessage] = UserHasNoAddressErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        try
        {
            var addressId = await _userService.GetAddressIdByUserIdAsync(User.GetId()!);

            var addressModel = await _addressService.GetAddressEditByIdAsync(addressId!);

            return View(addressModel);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditUserAddress(AddressEditViewModel addressModel, string id)
    {
        var isOrderExisting = await _orderService.IsOrderExisting(id);

        if (!isOrderExisting)
        {
            TempData[ErrorMessage] = OrderNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        var hasAddress = await _userService.UserHasAddressAsync(User.GetId()!);

        if (!hasAddress)
        {
            TempData[ErrorMessage] = UserHasNoAddressErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            return View(addressModel);
        }

        try
        {
            var addressId = await _userService.GetAddressIdByUserIdAsync(User.GetId()!);

            await _addressService.EditAsync(addressId!, addressModel);

            TempData[SuccessMessage] = AddressEditedSuccessMessage;

            return RedirectToAction("Details", "Order", new { id });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }
}
