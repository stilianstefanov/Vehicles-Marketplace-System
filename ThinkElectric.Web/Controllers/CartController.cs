namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;
using Infrastructure.Extensions;

using static Common.NotificationsMessagesConstants;
using static Common.ErrorMessages;
using static Common.GeneralMessages;

[Authorize(Policy = "BuyerOnly")]
public class CartController : BaseController
{
    private readonly ICartService _cartService;
    private readonly IProductService _productService;

    public CartController(
        ICartService cartService, 
        IProductService productService)
    {
        _cartService = cartService;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> AddToCart(string id)
    {
        var productExists = await _productService.ProductExistsAsync(id);

        if (!productExists)
        {
            TempData[ErrorMessage] = ProductNotFoundErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        var hasProductQuantity = await _productService.HasProductQuantityAsync(id);

        if (!hasProductQuantity)
        {
            TempData[ErrorMessage] = ProductOutOfStockErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        var productAlreadyAdded = await _cartService.ProductAlreadyAdded(id, User.GetId()!);

        if (productAlreadyAdded)
        {
            TempData[ErrorMessage] = ProductAlreadyAddedToCartErrorMessage;
            return RedirectToAction("All");
        }

        try
        {
            await _cartService.AddToCartAsync(id, User.GetId()!);

            TempData[SuccessMessage] = ProductAddedToCartSuccessMessage;

            var referer = Request.Headers["Referer"].ToString();

            if (referer.Contains("AddToCart"))
            {
                return RedirectToAction("All");
            }

            return Redirect(referer);

        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(string id)
    {
        var cartItemExists = await _cartService.CartItemExistsAsync(id);

        if (!cartItemExists)
        {
            TempData[ErrorMessage] = CartItemNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        var isUserAuthorized = await _cartService.IsUserAuthorizedAsync(id, User.GetId());

        if (!isUserAuthorized)
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        try
        {
            await _cartService.RemoveFromCartAsync(id);

            TempData[SuccessMessage] = CartItemRemovedSuccessMessage;

            return RedirectToAction("All");
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
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
}
