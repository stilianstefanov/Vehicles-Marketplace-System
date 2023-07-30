namespace ThinkElectric.Web.Controllers;

using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using static Common.NotificationsMessagesConstants;
using static Common.ErrorMessages;
using static Common.GeneralMessages;

[Authorize(Policy = "BuyerOnly")]
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

        bool productAlreadyAdded = await _cartService.ProductAlreadyAdded(id);

        if (productAlreadyAdded)
        {
            TempData[ErrorMessage] = ProductAlreadyAddedToCartErrorMessage;
            return RedirectToAction(nameof(All));
        }

        try
        {
            await _cartService.AddToCartAsync(id, User.GetId()!);

            TempData[SuccessMessage] = ProductAddedToCartSuccessMessage;
        }
        catch (Exception)
        {
            return GeneralError();
        }

        return RedirectToAction(nameof(All));
    }

    [HttpPost]
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

        try
        {
            await _cartService.RemoveFromCartAsync(id);

            TempData[SuccessMessage] = CartItemRemovedSuccessMessage;

            return RedirectToAction(nameof(All));
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    public async Task<IActionResult> All()
    {
        try
        {
            var cartItems = await _cartService.GetAllAsync(User.GetId()!);

            return View(cartItems);
        }
        catch
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
