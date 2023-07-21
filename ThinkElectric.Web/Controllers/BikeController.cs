namespace ThinkElectric.Web.Controllers;

using Data.Models.Enums.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThinkElectric.Services.Contracts;
using ViewModels.Bike;
using static Common.ErrorMessages;
using static Common.NotificationsMessagesConstants;
using static Common.GeneralMessages;


[Authorize]
public class BikeController : Controller
{
    private readonly IBikeService _bikeService;
    private readonly IProductService _productService;
    private readonly IImageService _imageService;

    public BikeController(IBikeService bikeService, IProductService productService, IImageService imageService)
    {
        _bikeService = bikeService;
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
    public async Task<IActionResult> Create(BikeCreateViewModel bikeModel)
    {
        if (!ModelState.IsValid)
        {
            return View(bikeModel);
        }

        if (bikeModel.Product.ImageFile == null || bikeModel.Product.ImageFile.Length == 0)
        {
            ModelState.AddModelError(nameof(bikeModel.Product.ImageFile), ImageRequiredErrorMessage);
            return View(bikeModel);
        }

        var imageType = bikeModel.Product.ImageFile.ContentType;

        if (imageType != "image/jpg" && imageType != "image/jpeg" && imageType != "image/png")
        {
            ModelState.AddModelError(nameof(bikeModel.Product.ImageFile), ImageFormatErrorMessage);
            return View(bikeModel);
        }

        try
        {
            var companyId = User.FindFirst("companyId")!.Value;

            var imageId = await _imageService.CreateAsync(bikeModel.Product.ImageFile);

            var productId = await _productService.CreateAsync(bikeModel.Product, companyId, imageId, ProductType.Bike);

            var scooterId = await _bikeService.CreateAsync(bikeModel, productId);

            TempData[SuccessMessage] = BikeCreateSuccessMessage;

            return RedirectToAction("Index", "Home");

            //return RedirectToAction("Details", "Bike", new { id = scooterId });
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
