namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ViewModels.Accessory;
using static Common.ErrorMessages;
using static Common.NotificationsMessagesConstants;
using static Common.GeneralMessages;
using ThinkElectric.Data.Models.Enums.Product;
using ThinkElectric.Services;

[Authorize]
public class AccessoryController : Controller
{
    private readonly IAccessoryService _accessoryService;
    private readonly IProductService _productService;
    private readonly IImageService _imageService;

    public AccessoryController(IAccessoryService accessoryService, IProductService productService, IImageService imageService)
    {
        _productService = productService;
        _imageService = imageService;
        _accessoryService = accessoryService;
    }

    [HttpGet]
    [Authorize(Policy = "CompanyOnly")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Policy = "CompanyOnly")]
    public async Task<IActionResult> Create(AccessoryCreateViewModel accessoryModel)
    {
        if (!ModelState.IsValid)
        {
            return View(accessoryModel);
        }

        if (accessoryModel.Product.ImageFile == null || accessoryModel.Product.ImageFile.Length == 0)
        {
            ModelState.AddModelError(nameof(accessoryModel.Product.ImageFile), ImageRequiredErrorMessage);
            return View(accessoryModel);
        }

        var imageType = accessoryModel.Product.ImageFile.ContentType;

        if (imageType != "image/jpg" && imageType != "image/jpeg" && imageType != "image/png")
        {
            ModelState.AddModelError(nameof(accessoryModel.Product.ImageFile), ImageFormatErrorMessage);
            return View(accessoryModel);
        }

        try
        {
            var companyId = User.FindFirst("companyId")!.Value;

            var imageId = await _imageService.CreateAsync(accessoryModel.Product.ImageFile);

            var productId = await _productService.CreateAsync(accessoryModel.Product, companyId, imageId, ProductType.Accessory);

            var scooterId = await _accessoryService.CreateAsync(accessoryModel, productId);

            TempData[SuccessMessage] = AccessoryCreateSuccessMessage;

            return RedirectToAction("Index", "Home");

            //return RedirectToAction("Details", "Accessory", new { id = scooterId });
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
