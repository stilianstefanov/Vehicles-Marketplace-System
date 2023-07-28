namespace ThinkElectric.Web.Controllers;

using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ViewModels.Cart;
using static Common.NotificationsMessagesConstants;
using static Common.ErrorMessages;

[Authorize] // TODO: Add CartUserPolicy
public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly IProductService _productService;

    public CartController(ICartService cartService, IProductService productService)
    {
        _cartService = cartService;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> AddToCart(string id)
    {
        bool productExists = await _productService.ProductExistsAsync(id);

        if (!productExists)
        {
            TempData[ErrorMessage] = ProductNotFoundErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        await _cartService.AddToCartAsync(id, User.GetId()!);

        return RedirectToAction(nameof(All));
    }

    public async Task<IActionResult> RemoveFromCart(string id)
    {
        bool cartItemExists = await _cartService.CartItemExistsAsync(id);

        if (!cartItemExists)
        {
            TempData[ErrorMessage] = CartItemNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        bool isUserAuthorized = await _cartService.IsUserAuthorizedAsync(id, User.GetId());

        if (!isUserAuthorized)
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        await _cartService.RemoveFromCartAsync(id);

        return RedirectToAction(nameof(All));
    }

    public async Task<IActionResult> All()
    {
        var cartItems = await _cartService.GetAllAsync(User.GetId()!);

        return View(cartItems);
    }
}
