namespace ThinkElectric.Web.Controllers;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;
using ViewModels.Product;
using Infrastructure.Extensions;

using static Common.ErrorMessages;
using static Common.NotificationsMessagesConstants;
using static Common.GeneralMessages;

public class ProductController : BaseController
{
    private readonly IProductService _productService;
    private readonly IImageService _imageService;
    private readonly ICompanyService _companyService;

    public ProductController(
        IProductService productService,
        IImageService imageService,
        ICompanyService companyService)
    {
        _productService = productService;
        _imageService = imageService;
        _companyService = companyService;
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

        var companyExists = await _companyService.CompanyExistsByIdAsync(queryModel.CompanyId!);

        if (!companyExists)
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

    [HttpGet]
    [Authorize(Policy = "CompanyOnly")]
    public async Task<IActionResult> Edit(string id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        if (product == null)
        {
            TempData[ErrorMessage] = ProductNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        if (product.Scooter != null)
        {
            return RedirectToAction("Edit", "Scooter", new { product.Scooter.Id });
        }

        if (product.Bike != null)
        {
            return RedirectToAction("Edit", "Bike", new { product.Bike.Id });
        }

        if (product.Accessory != null)
        {
            return RedirectToAction("Edit", "Accessory", new { product.Accessory.Id });
        }

        return GeneralError();
    }

    [HttpPost]
    [Authorize(Policy = "CompanyOnly")]
    public async Task<IActionResult> Delete(string id)
    {
        var productExists = await _productService.ProductExistsAsync(id);

        if (!productExists)
        {
            TempData[ErrorMessage] = ProductNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        var isUserAuthorized = await _productService.IsUserAuthorizedAsync(id, User.GetCompanyId()!);

        if (!isUserAuthorized)
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        try
        {
            await _productService.DeleteAsync(id);

            TempData[SuccessMessage] = ProductDeletedSuccessfullyMessage;

            return RedirectToAction("Index", "Home");
        }
        catch
        {
            return GeneralError();
        }
    }
}
