namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Data.Models.Enums.Product;
using Services.Contracts;
using ViewModels.Scooter;

using static Common.ErrorMessages;
using static Common.NotificationsMessagesConstants;
using static Common.GeneralMessages;

public class ScooterController : BaseController
{
    private readonly IScooterService _scooterService;
    private readonly IProductService _productService;
    private readonly IImageService _imageService;
    private readonly IReviewService _reviewService;

    public ScooterController(
        IScooterService scooterService,
        IProductService productService,
        IImageService imageService,
        IReviewService reviewService)
    {
        _scooterService = scooterService;
        _productService = productService;
        _imageService = imageService;
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
    public async Task<IActionResult> Create(ScooterCreateViewModel scooterModel)
    {
        if (!ModelState.IsValid)
        {
            return View(scooterModel);
        }

        if (scooterModel.Product.ImageFile == null || scooterModel.Product.ImageFile.Length == 0)
        {
            TempData[ErrorMessage] = ImageRequiredErrorMessage;
            return View(scooterModel);
        }

        var imageType = scooterModel.Product.ImageFile.ContentType;

        if (imageType != "image/jpg" && imageType != "image/jpeg" && imageType != "image/png")
        {
            TempData[ErrorMessage] = ImageFormatErrorMessage;
            return View(scooterModel);
        }

        try
        {
            var companyId = User.FindFirst("companyId")!.Value;

            var imageId = await _imageService.CreateAsync(scooterModel.Product.ImageFile);

            var productId = await _productService.CreateAsync(scooterModel.Product, companyId, imageId, ProductType.Scooter);

            var scooterId = await _scooterService.CreateAsync(scooterModel, productId);

            TempData[SuccessMessage] = ScooterCreateSuccessMessage;

            return RedirectToAction("Details", "Scooter", new { id = scooterId });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(string id)
    {
        var scooterModel = await _scooterService.GetScooterDetailsByIdAsync(id);

        if (scooterModel == null)
        {
            TempData[ErrorMessage] = ScooterNotFoundErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        try
        {
            scooterModel.Product = await _productService.GetProductDetailsByIdAsync(scooterModel.ProductId);

            scooterModel.Product.Image = await _imageService.GetImageByIdAsync(scooterModel.Product.ImageId);

            scooterModel.Product.Reviews = await _reviewService.GetReviewsByProductIdAsync(scooterModel.ProductId);

            return View(scooterModel);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
    [Authorize(Policy = "CompanyOnly")]
    public async Task<IActionResult> Edit(string id)
    {
        var isScooterExisting = await _scooterService.IsScooterExistingAsync(id);

        if (!isScooterExisting)
        {
            TempData[ErrorMessage] = ScooterNotFoundErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        var isUserAuthorized = await _scooterService.IsUserAuthorizedToEditAsync(id, User.FindFirst("companyId")!.Value);

        if (!isUserAuthorized)
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        try
        {
            var scooterModel = await _scooterService.GetScooterEditViewModelByIdAsync(id);

            scooterModel.Product = await _productService.GetProductEditViewModelByIdAsync(scooterModel.ProductId!);

            scooterModel.Product.CurrentImage = await _imageService.GetImageByIdAsync(scooterModel.Product.ImageId!);

            return View(scooterModel);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    [Authorize(Policy = "CompanyOnly")]
    public async Task<IActionResult> Edit([FromForm] ScooterEditViewModel scooterModel, string id)
    {
        var isScooterExisting = await _scooterService.IsScooterExistingAsync(id);

        if (!isScooterExisting)
        {
            TempData[ErrorMessage] = ScooterNotFoundErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        var isUserAuthorized = await _scooterService.IsUserAuthorizedToEditAsync(id, User.FindFirst("companyId")!.Value);

        if (!isUserAuthorized)
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            var productId = await _scooterService.GetProductIdByScooterIdAsync(id);
            var imageId = await _productService.GetImageIdByProductIdAsync(productId);
            
            scooterModel.Product.CurrentImage = await _imageService.GetImageByIdAsync(imageId);
            return View(scooterModel);
        }

        if (scooterModel.Product.NewImage != null && scooterModel.Product.NewImage.Length != 0)
        {
            var imageType = scooterModel.Product.NewImage.ContentType;

            if (imageType != "image/jpg" && imageType != "image/jpeg" && imageType != "image/png")
            {
                var productId = await _scooterService.GetProductIdByScooterIdAsync(id);
                var imageId = await _productService.GetImageIdByProductIdAsync(productId);

                scooterModel.Product.CurrentImage = await _imageService.GetImageByIdAsync(imageId);

                TempData[ErrorMessage] = ImageFormatErrorMessage;
                return View(scooterModel);
            }
        }

        try
        {
            var productId = await _scooterService.GetProductIdByScooterIdAsync(id);

            if (scooterModel.Product.NewImage != null && scooterModel.Product.NewImage.Length != 0)
            {
                var imageId = await _productService.GetImageIdByProductIdAsync(productId);

                await _imageService.UpdateAsync(imageId, scooterModel.Product.NewImage);
            }

            await _productService.EditAsync(productId, scooterModel.Product);

            await _scooterService.EditAsync(id, scooterModel);

            TempData[SuccessMessage] = ScooterEditSuccessMessage;

            return RedirectToAction("Details", "Scooter", new { id });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult All()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> AllFilteredAndPaged(ScooterAllQueryModel queryModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        queryModel = await _scooterService.GetAllFilteredAndPagedAsync(queryModel);

        queryModel.Scooters = queryModel
            .Scooters
            .Select(async scooter =>
            {
                scooter.Product.Image = await _imageService.GetImageByIdAsync(scooter.Product.ImageId);
                return scooter;
            })
            .Select(t => t.Result).ToList();

        return Json(queryModel);
    }
}
