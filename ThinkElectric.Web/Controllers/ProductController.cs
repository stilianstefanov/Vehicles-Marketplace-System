namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ViewModels.Product;
using static Common.ErrorMessages;
using static Common.NotificationsMessagesConstants;

[Authorize]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly IImageService _imageService;

    public ProductController(IProductService productService, IImageService imageService)
    {
        _productService = productService;
        _imageService = imageService;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult AllByCompanyId()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> AllByCompanyIdFilteredAndPaged(ProductAllQueryModel queryModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        queryModel = await _productService.AllByCompanyIdAsync(queryModel);

        queryModel.Products = queryModel
            .Products
            .Select(async product =>
            {
                product.Image = await _imageService.GetImageByIdAsync(product.ImageId);
                return product;
            })
            .Select(t => t.Result).ToList();

        return Json(queryModel);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(string id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        if (product == null)
        {
            TempData[ErrorMessage] = ProductNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        if (product.Scooter != null)
        {
            return RedirectToAction("Details", "Scooter", new { product.Scooter.Id });
        }
        if (product.Bike != null)
        {
            return RedirectToAction("Details", "Bike", new { product.Bike.Id });
        }
        if (product.Accessory != null)
        {
            return RedirectToAction("Details", "Accessory", new { product.Accessory.Id });
        }

        return GeneralError();
    }

    private IActionResult GeneralError()
    {
        this.TempData[ErrorMessage] = UnexpectedErrorMessage;

        return RedirectToAction("Index", "Home");
    }
}
