namespace ThinkElectric.Web.Controllers;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;
using ViewModels.Review;
using Infrastructure.Extensions;

using static Common.NotificationsMessagesConstants;
using static Common.ErrorMessages;
using static Common.GeneralMessages;

[Authorize(Policy = "BuyerOnly")]
public class ReviewController : BaseController
{
    private readonly IReviewService _reviewService;
    private readonly IProductService _productService;
    private readonly ICompanyService _companyService;

    public ReviewController(
        IReviewService reviewService, 
        IProductService productService,
        ICompanyService companyService)
    {
        _reviewService = reviewService;
        _productService = productService;
        _companyService = companyService;
    }

    [HttpGet]
    public async Task<IActionResult> AddToProduct(string id)
    {
        var productExists = await _productService.ProductExistsAsync(id);

        if (!productExists)
        {
            TempData[ErrorMessage] = ProductNotFoundErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        var alreadyReviewed = await _reviewService.AlreadyReviewedProductAsync(id, User.GetId()!);

        if (alreadyReviewed)
        {
            TempData[ErrorMessage] = AlreadyReviewedProductErrorMessage;

            return RedirectToAction("Details", "Product", new { id });
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddToProduct(ReviewAddViewModel reviewModel, string id)
    {

        var productExists = await _productService.ProductExistsAsync(id);

        if (!productExists)
        {
            TempData[ErrorMessage] = ProductNotFoundErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        var alreadyReviewed = await _reviewService.AlreadyReviewedProductAsync(id, User.GetId()!);

        if (alreadyReviewed)
        {
            TempData[ErrorMessage] = AlreadyReviewedProductErrorMessage;

            return RedirectToAction("Details", "Product", new { id });
        }

        if (!ModelState.IsValid)
        {
            return View(reviewModel);
        }

        try
        {
            await _reviewService.AddToProductAsync(reviewModel, id, User.GetId()!);

            TempData[SuccessMessage] = ReviewAddedSuccessMessage;

            return RedirectToAction("Details", "Product", new { id });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
    public async Task<IActionResult> AddToCompany(string id)
    {
        var companyExists = await _companyService.CompanyExistsByIdAsync(id);

        if (!companyExists)
        {
            TempData[ErrorMessage] = CompanyNotFoundErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        var alreadyReviewed = await _reviewService.AlreadyReviewedCompanyAsync(id, User.GetId()!);

        if (alreadyReviewed)
        {
            TempData[ErrorMessage] = AlreadyReviewedCompanyErrorMessage;

            return RedirectToAction("Details", "Company", new { id });
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddToCompany(ReviewAddViewModel reviewModel, string id)
    {
        var companyExists = await _companyService.CompanyExistsByIdAsync(id);

        if (!companyExists)
        {
            TempData[ErrorMessage] = CompanyNotFoundErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        var alreadyReviewed = await _reviewService.AlreadyReviewedCompanyAsync(id, User.GetId()!);

        if (alreadyReviewed)
        {
            TempData[ErrorMessage] = AlreadyReviewedCompanyErrorMessage;

            return RedirectToAction("Details", "Company", new { id });
        }

        if (!ModelState.IsValid)
        {
            return View(reviewModel);
        }

        try
        {
            await _reviewService.AddToCompanyAsync(reviewModel, id, User.GetId()!);

            TempData[SuccessMessage] = ReviewAddedSuccessMessage;

            return RedirectToAction("Details", "Company", new { id });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Mine()
    {
        try
        {
            var reviews = await _reviewService.GetMineAsync(User.GetId()!);

            return View(reviews);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var reviewExists = await _reviewService.ReviewExistsAsync(id);

        if (!reviewExists)
        {
            TempData[ErrorMessage] = ReviewNotFoundErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        var isUserAuthorized = await _reviewService.IsUserAuthorizedAsync(id, User.GetId()!);

        if (!isUserAuthorized)
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        try
        {
            var review = await _reviewService.GetForEditAsync(id);

            return View(review);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ReviewEditViewModel reviewModel, string id)
    {
        var reviewExists = await _reviewService.ReviewExistsAsync(id);

        if (!reviewExists)
        {
            TempData[ErrorMessage] = ReviewNotFoundErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        var isUserAuthorized = await _reviewService.IsUserAuthorizedAsync(id, User.GetId()!);

        if (!isUserAuthorized)
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            return View(reviewModel);
        }

        try
        {
            await _reviewService.EditAsync(reviewModel, id);

            TempData[SuccessMessage] = ReviewEditedSuccessMessage;

            return RedirectToAction("Mine");
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }


    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var reviewExists = await _reviewService.ReviewExistsAsync(id);

        if (!reviewExists)
        {
            TempData[ErrorMessage] = ReviewNotFoundErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        var isUserAuthorized = await _reviewService.IsUserAuthorizedAsync(id, User.GetId()!);

        if (!isUserAuthorized)
        {
            TempData[ErrorMessage] = UnauthorizedErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        try
        {
            await _reviewService.DeleteAsync(id);

            TempData[SuccessMessage] = ReviewDeletedSuccessMessage;

            return RedirectToAction("Mine");
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }
}
