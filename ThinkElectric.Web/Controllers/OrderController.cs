namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;
using ViewModels.CartItem;
using Infrastructure.Extensions;

using static Common.ErrorMessages;
using static Common.NotificationsMessagesConstants;
using static Common.GeneralMessages;


public class OrderController : BaseController
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
    [Authorize(Policy = "BuyerOnly")]
    public async Task<IActionResult> Create([FromForm] IEnumerable<CartItemViewModel> cartItems)
    {
        try
        {
            var areCartItemsValid = await _cartService.AreCartItemsValidAsync(cartItems);

            if (!areCartItemsValid)
            {
                return GeneralError();
            }

            var orderId = await _orderService.CreateAsync(cartItems, User.GetId()!);

            return RedirectToAction("Details", "Order", new { id = orderId });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
    [Authorize(Policy = "BuyerOnly")]
    public async Task<IActionResult> Details(string id)
    {
        var orderExists = await _orderService.OrderExistsAsync(id);

        if (!orderExists)
        {
            TempData[ErrorMessage] = OrderNotFound;

            return RedirectToAction("Index", "Home");
        }

        var isOrderFromUser = await _orderService.IsOrderFromUserAsync(id, User.GetId()!);

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
    [Authorize(Policy = "BuyerOnly")]
    public async Task<IActionResult> Cancel(string id)
    {
        var orderExists = await _orderService.OrderExistsAsync(id);

        if (!orderExists)
        {
            TempData[ErrorMessage] = OrderNotFound;

            return RedirectToAction("Index", "Home");
        }

        var isOrderFromUser = await _orderService.IsOrderFromUserAsync(id, User.GetId()!);

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
    [Authorize(Policy = "BuyerOnly")]
    public async Task<IActionResult> Confirm(string id)
    {
        var orderExists = await _orderService.OrderExistsAsync(id);

        if (!orderExists)
        {
            TempData[ErrorMessage] = OrderNotFound;

            return RedirectToAction("Index", "Home");
        }

        var isOrderFromUser = await _orderService.IsOrderFromUserAsync(id, User.GetId()!);

        if (!isOrderFromUser)
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;

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
            await _cartService.ClearAsync(User.GetId()!);

            await _productService.DecreaseQuantityAsync(id);

            await _orderService.ConfirmAsync(id);

            TempData[SuccessMessage] = OrderConfirmed;

            return RedirectToAction(nameof(AllUserOrders));
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
    [Authorize(Policy = "BuyerOnly")]
    public async Task<IActionResult> AllUserOrders()
    {
        try
        {
            var orders = await _orderService.GetAllByUserAsync(User.GetId()!);

            return View(orders);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
    [Authorize(Policy = "CompanyOnly")]
    public async Task<IActionResult> AllCompanyOrders()
    {
        try
        {
            var orderItems = await _orderService.GetAllByCompanyAsync(User.GetCompanyId()!);

            return View(orderItems);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    [Authorize(Policy = "CompanyOnly")]
    public async Task<IActionResult> MarkAsFulfilled(string orderItemId)
    {
        var orderItemExists = await _orderService.OrderItemExistsAsync(orderItemId);

        if (!orderItemExists)
        {
            return GeneralError();
        }

        var isOrderItemFromCompany = await _orderService.IsOrderItemFromCompanyAsync(orderItemId, User.GetCompanyId()!);

        if (!isOrderItemFromCompany)
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        try
        {
            await _orderService.MarkAsFulfilledAsync(orderItemId);

            TempData[SuccessMessage] = OrderItemMarkedAsFulfilled;

            return RedirectToAction(nameof(AllCompanyOrders));
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }
}
