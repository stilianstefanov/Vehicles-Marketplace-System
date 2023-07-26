namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ViewModels.Accessory;
using static Common.ErrorMessages;
using static Common.NotificationsMessagesConstants;
using static Common.GeneralMessages;
using Data.Models.Enums.Product;

[Authorize]
public class AccessoryController : Controller
{
    private readonly IAccessoryService _accessoryService;
    private readonly IProductService _productService;
    private readonly IImageService _imageService;
    private readonly IReviewService _reviewService;

    public AccessoryController(
        IAccessoryService accessoryService, 
        IProductService productService, 
        IImageService imageService,
        IReviewService reviewService)
    {
        _productService = productService;
        _imageService = imageService;
        _accessoryService = accessoryService;
        _reviewService = reviewService;
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
            TempData[ErrorMessage] = ImageRequiredErrorMessage;
            return View(accessoryModel);
        }

        var imageType = accessoryModel.Product.ImageFile.ContentType;

        if (imageType != "image/jpg" && imageType != "image/jpeg" && imageType != "image/png")
        {
            TempData[ErrorMessage] = ImageFormatErrorMessage;
            return View(accessoryModel);
        }

        try
        {
            var companyId = User.FindFirst("companyId")!.Value;

            var imageId = await _imageService.CreateAsync(accessoryModel.Product.ImageFile);

            var productId = await _productService.CreateAsync(accessoryModel.Product, companyId, imageId, ProductType.Accessory);

            var accessoryId = await _accessoryService.CreateAsync(accessoryModel, productId);

            TempData[SuccessMessage] = AccessoryCreateSuccessMessage;

            return RedirectToAction("Details", "Accessory", new { id = accessoryId });
        }
        catch
        {
            return GeneralError();
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(string id)
    {
        var accessoryModel = await _accessoryService.GetAccessoryDetailsByIdAsync(id);

        if (accessoryModel == null)
        {
            TempData[ErrorMessage] = AccessoryNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        try
        {
            accessoryModel.Product = await _productService.GetProductDetailsByIdAsync(accessoryModel.ProductId);

            accessoryModel.Product.Image = await _imageService.GetImageByIdAsync(accessoryModel.Product.ImageId);

            accessoryModel.Product.Reviews = await _reviewService.GetReviewsByProductIdAsync(accessoryModel.ProductId);

            return View(accessoryModel);
        }
        catch
        {
            return GeneralError();
        }
    }

    [HttpGet]
    [Authorize(Policy = "CompanyOnly")]
    public async Task<IActionResult> Edit(string id)
    {
        bool isAccessoryExisting = await _accessoryService.IsAccessoryExistingAsync(id);

        if (!isAccessoryExisting)
        {
            TempData[ErrorMessage] = AccessoryNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        bool isUserAuthorized = await _accessoryService.IsUserAuthorizedToEditAsync(id, User.FindFirst("companyId")!.Value);

        if (!isUserAuthorized)
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        try
        {
            var accessoryModel = await _accessoryService.GetAccessoryEditViewModelByIdAsync(id);

            accessoryModel.Product = await _productService.GetProductEditViewModelByIdAsync(accessoryModel.ProductId!);

            accessoryModel.Product.CurrentImage = await _imageService.GetImageByIdAsync(accessoryModel.Product.ImageId!);

            return View(accessoryModel);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    [Authorize(Policy = "CompanyOnly")]
    public async Task<IActionResult> Edit([FromForm]AccessoryEditViewModel accessoryModel, string id)
    {
        bool isAccessoryExisting = await _accessoryService.IsAccessoryExistingAsync(id);

        if (!isAccessoryExisting)
        {
            TempData[ErrorMessage] = AccessoryNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        bool isUserAuthorized = await _accessoryService.IsUserAuthorizedToEditAsync(id, User.FindFirst("companyId")!.Value);

        if (!isUserAuthorized)
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            var productId = await _accessoryService.GetProductIdByAccessoryIdAsync(id);
            var imageId = await _productService.GetImageIdByProductIdAsync(productId);

            accessoryModel.Product.CurrentImage = await _imageService.GetImageByIdAsync(imageId);
            return View(accessoryModel);
        }

        if (accessoryModel.Product.NewImage != null && accessoryModel.Product.NewImage.Length != 0)
        {
            var imageType = accessoryModel.Product.NewImage.ContentType;

            if (imageType != "image/jpg" && imageType != "image/jpeg" && imageType != "image/png")
            {
                var productId = await _accessoryService.GetProductIdByAccessoryIdAsync(id);
                var imageId = await _productService.GetImageIdByProductIdAsync(productId);

                accessoryModel.Product.CurrentImage = await _imageService.GetImageByIdAsync(imageId);

                TempData[ErrorMessage] = ImageFormatErrorMessage;
                return View(accessoryModel);
            }
        }

        try
        {
            var productId = await _accessoryService.GetProductIdByAccessoryIdAsync(id);

            if (accessoryModel.Product.NewImage != null && accessoryModel.Product.NewImage.Length != 0)
            {
                var imageId = await _productService.GetImageIdByProductIdAsync(productId);

                await _imageService.UpdateAsync(imageId, accessoryModel.Product.NewImage);
            }

            await _productService.EditAsync(productId, accessoryModel.Product);

            await _accessoryService.EditAsync(id, accessoryModel);

            TempData[SuccessMessage] = AccessoryEditSuccessMessage;

            return RedirectToAction("Details", "Accessory", new { id });
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
