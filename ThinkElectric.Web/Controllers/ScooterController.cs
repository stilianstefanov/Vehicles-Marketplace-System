namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ViewModels.Scooter;
using static Common.ErrorMessages;
using static Common.NotificationsMessagesConstants;
using static Common.GeneralMessages;

[Authorize]
public class ScooterController : Controller
{
    private readonly IScooterService _scooterService;
    private readonly IProductService _productService;
    private readonly IImageService _imageService;

    public ScooterController(IScooterService scooterService, IProductService productService, IImageService imageService)
    {
        _scooterService = scooterService;
        _productService = productService;
        _imageService = imageService;
    }

    [HttpGet]
    [Authorize(Policy = "CompanyOnly")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Policy = "CompanyOnly")]
    public async Task<IActionResult> Create(ScooterCreateViewModel scooterModel)
    {
        if (!ModelState.IsValid)
        {
            return View(scooterModel);
        }

        if (scooterModel.Product.ImageFile == null || scooterModel.Product.ImageFile.Length == 0)
        {
            ModelState.AddModelError(nameof(scooterModel.Product.ImageFile), ImageRequiredErrorMessage);
            return View(scooterModel);
        }

        var imageType = scooterModel.Product.ImageFile.ContentType;

        if (imageType != "image/jpg" && imageType != "image/jpeg" && imageType != "image/png")
        {
            ModelState.AddModelError(nameof(scooterModel.Product.ImageFile), ImageFormatErrorMessage);
            return View(scooterModel);
        }

        try
        {
            var companyId = User.FindFirst("companyId")!.Value;

            var imageId = await _imageService.CreateAsync(scooterModel.Product.ImageFile);

            var productId = await _productService.CreateAsync(scooterModel.Product, companyId, imageId);

            var scooterId = await _scooterService.CreateAsync(scooterModel, productId);

            TempData[SuccessMessage] = ScooterCreateSuccessMessage;

            return RedirectToAction("Index", "Home");

            //return RedirectToAction("Details", "Scooter", new { id = scooterId });
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
