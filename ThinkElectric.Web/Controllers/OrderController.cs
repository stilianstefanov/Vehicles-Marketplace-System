namespace ThinkElectric.Web.Controllers;

using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ViewModels.CartItem;
using static Common.ErrorMessages;
using static Common.NotificationsMessagesConstants;
using static Common.GeneralMessages;

[Authorize(Policy = "BuyerOnly")]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly ICartService _cartService;
    private readonly IProductService _productService;
    private readonly IAddressService _addressService;
    private readonly IUserService _userService;

    public OrderController(
        IOrderService orderService, 
        ICartService cartService, 
        IProductService productService,
        IAddressService addressService,
        IUserService userService)
    {
        _orderService = orderService;
        _cartService = cartService;
        _productService = productService;
        _addressService = addressService;
        _userService = userService;
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromForm] IEnumerable<CartItemViewModel> cartItems)
    {
        try
        {
            bool areCartItemsValid = await _cartService.AreCartItemsValidAsync(cartItems);

            if (!areCartItemsValid)
            {
                return GeneralError();
            }

            string orderId = await _orderService.CreateAsync(cartItems, User.GetId()!);

            return RedirectToAction("Details", "Order", new { id = orderId });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        bool orderExists = await _orderService.OrderExistsAsync(id);

        if (!orderExists)
        {
            TempData[ErrorMessage] = OrderNotFound;

            return RedirectToAction("Index", "Home");
        }

        bool isOrderFromUser = await _orderService.IsOrderFromUserAsync(id, User.GetId()!);

        if (!isOrderFromUser)
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        try
        {
            var orderDetailsViewModel = await _orderService.GetOrderDetailsAsync(id);

            var addressId = await _userService.GetAddressIdByUserIdAsync(User.GetId()!);

            if (!string.IsNullOrWhiteSpace(addressId))
            {
                orderDetailsViewModel.Address = await _addressService.GetAddressDetailsByIdAsync(addressId);
            }

            return View(orderDetailsViewModel);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Cancel(string id)
    {
        bool orderExists = await _orderService.OrderExistsAsync(id);

        if (!orderExists)
        {
            TempData[ErrorMessage] = OrderNotFound;

            return RedirectToAction("Index", "Home");
        }

        bool isOrderFromUser = await _orderService.IsOrderFromUserAsync(id, User.GetId()!);

        if (!isOrderFromUser)
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        try
        {
            await _orderService.CancelAsync(id);

            TempData[SuccessMessage] = OrderCancelled;

            return RedirectToAction("All", "Cart");
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Confirm(string id)
    {
        bool orderExists = await _orderService.OrderExistsAsync(id);

        if (!orderExists)
        {
            TempData[ErrorMessage] = OrderNotFound;

            return RedirectToAction("Index", "Home");
        }

        bool isOrderFromUser = await _orderService.IsOrderFromUserAsync(id, User.GetId()!);

        if (!isOrderFromUser)
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        try
        {
            await _cartService.ClearAsync(User.GetId()!);

            await _productService.DecreaseQuantityAsync(id);

            await _orderService.ConfirmAsync(id);

            TempData[SuccessMessage] = OrderConfirmed;

            return RedirectToAction("Index", "Home"); // TODO: Redirect to Order/All/ByUser
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    private IActionResult GeneralError()
    {
        this.TempData[ErrorMessage] = UnexpectedErrorMessage;

        return RedirectToAction("Index", "Home");
    }
}
