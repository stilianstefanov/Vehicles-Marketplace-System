namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Data.Models.Enums.Product;
using Infrastructure.Extensions;
using Services.Contracts;
using ViewModels.Bike;

using static Common.ErrorMessages;
using static Common.NotificationsMessagesConstants;
using static Common.GeneralMessages;
using static Common.GeneralApplicationConstants;

public class BikeController : BaseController
{
    private readonly IBikeService _bikeService;
    private readonly IProductService _productService;
    private readonly IImageService _imageService;
    private readonly IReviewService _reviewService;

    public BikeController(
        IBikeService bikeService, 
        IProductService productService, 
        IImageService imageService,
        IReviewService reviewService)
    {
        _bikeService = bikeService;
        _productService = productService;
        _imageService = imageService;
        _reviewService = reviewService;
    }

    [HttpGet]
    [Authorize(Policy = CompanyOnlyPolicyName)]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Policy = CompanyOnlyPolicyName)]
    public async Task<IActionResult> Create(BikeCreateViewModel bikeModel)
    {
        if (!ModelState.IsValid)
        {
            return View(bikeModel);
        }

        if (bikeModel.Product.ImageFile == null || bikeModel.Product.ImageFile.Length == 0)
        {
            TempData[ErrorMessage] = ImageRequiredErrorMessage;
            return View(bikeModel);
        }

        var imageType = bikeModel.Product.ImageFile.ContentType;

        if (imageType != "image/jpg" && imageType != "image/jpeg" && imageType != "image/png")
        {
            TempData[ErrorMessage] = ImageFormatErrorMessage;
            return View(bikeModel);
        }

        try
        {
            var companyId = User.GetCompanyId()!;

            var imageId = await _imageService.CreateAsync(bikeModel.Product.ImageFile);

            var productId = await _productService.CreateAsync(bikeModel.Product, companyId, imageId, ProductType.Bike);

            var bikeId = await _bikeService.CreateAsync(bikeModel, productId);

            TempData[SuccessMessage] = BikeCreateSuccessMessage;

            return RedirectToAction("Details", "Bike", new { id = bikeId });
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
        var bikeModel = await _bikeService.GetBikeDetailsByIdAsync(id);

        if (bikeModel == null)
        {
            TempData[ErrorMessage] = BikeNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        try
        {
            bikeModel.Product = await _productService.GetProductDetailsByIdAsync(bikeModel.ProductId);

            bikeModel.Product.Image = await _imageService.GetImageByIdAsync(bikeModel.Product.ImageId);

            bikeModel.Product.Reviews = await _reviewService.GetReviewsByProductIdAsync(bikeModel.ProductId);

            return View(bikeModel);
        }
        catch
        {
            return GeneralError();
        }
    }

    [HttpGet]
    [Authorize(Policy = CompanyOrAdminPolicyName)]
    public async Task<IActionResult> Edit(string id)
    {
        var isBikeExisting = await _bikeService.IsBikeExistingAsync(id);

        if (!isBikeExisting)
        {
            TempData[ErrorMessage] = BikeNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        var isUserAuthorized = await _bikeService.IsUserAuthorizedToEditAsync(id, User.GetCompanyId()!);

        if (!isUserAuthorized && !User.IsAdmin())
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        try
        {
            var bikeModel = await _bikeService.GetBikeEditViewModelByIdAsync(id);

            bikeModel.Product = await _productService.GetProductEditViewModelByIdAsync(bikeModel.ProductId!);

            bikeModel.Product.CurrentImage = await _imageService.GetImageByIdAsync(bikeModel.Product.ImageId!);

            return View(bikeModel);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    [Authorize(Policy = CompanyOrAdminPolicyName)]
    public async Task<IActionResult> Edit([FromForm] BikeEditViewModel bikeModel, string id)
    {
        var isBikeExisting = await _bikeService.IsBikeExistingAsync(id);

        if (!isBikeExisting)
        {
            TempData[ErrorMessage] = BikeNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        var isUserAuthorized = await _bikeService.IsUserAuthorizedToEditAsync(id, User.GetCompanyId()!);

        if (!isUserAuthorized && !User.IsAdmin())
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            var productId = await _bikeService.GetProductIdByBikeIdAsync(id);
            var imageId = await _productService.GetImageIdByProductIdAsync(productId);

            bikeModel.Product.CurrentImage = await _imageService.GetImageByIdAsync(imageId);

            return View(bikeModel);
        }

        if (bikeModel.Product.NewImage != null && bikeModel.Product.NewImage.Length != 0)
        {
            var imageType = bikeModel.Product.NewImage.ContentType;

            if (imageType != "image/jpg" && imageType != "image/jpeg" && imageType != "image/png")
            {
                var productId = await _bikeService.GetProductIdByBikeIdAsync(id);
                var imageId = await _productService.GetImageIdByProductIdAsync(productId);

                bikeModel.Product.CurrentImage = await _imageService.GetImageByIdAsync(imageId);

                TempData[ErrorMessage] = ImageFormatErrorMessage;
                return View(bikeModel);
            }
        }

        try
        {
            var productId = await _bikeService.GetProductIdByBikeIdAsync(id);

            if (bikeModel.Product.NewImage != null && bikeModel.Product.NewImage.Length != 0)
            {
                var imageId = await _productService.GetImageIdByProductIdAsync(productId);

                await _imageService.UpdateAsync(imageId, bikeModel.Product.NewImage);
            }

            await _productService.EditAsync(productId, bikeModel.Product);

            await _bikeService.EditAsync(id, bikeModel);

            TempData[SuccessMessage] = BikeEditSuccessMessage;

            return RedirectToAction("Details", "Bike", new { id });
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
    public async Task<IActionResult> AllFilteredAndPaged(BikeAllQueryModel queryModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        queryModel = await _bikeService.GetAllFilteredAndPagedAsync(queryModel);

        queryModel.Bikes = queryModel
            .Bikes
            .Select(async bike =>
            {
                bike.Product.Image = await _imageService.GetImageByIdAsync(bike.Product.ImageId);
                return bike;
            })
            .Select(t => t.Result).ToList();

        return Json(queryModel);
    }
}
