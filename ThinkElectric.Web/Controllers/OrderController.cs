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

    public OrderController(IOrderService orderService, ICartService cartService, IProductService productService)
    {
        _orderService = orderService;
        _cartService = cartService;
        _productService = productService;
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromForm] IEnumerable<CartItemViewModel> cartItems)
    {
        // 1. Create Order entity
        // 2. create orderItems entities
        // 3. Redirect to Orders/Details/{id}
        // 4. Redirect to Home/Index - 2 buttons - Cancel, Confirm

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
        // 1. Get order by id
        // 2. Get orderItems by orderId
        // 3. Display order details
        // 4. Display orderItems details
        // 5. Display total price
        // 6. Display button - Cancel
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
