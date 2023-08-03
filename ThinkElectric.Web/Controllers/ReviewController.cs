namespace ThinkElectric.Web.Controllers;

using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ViewModels.Review;
using static Common.NotificationsMessagesConstants;
using static Common.ErrorMessages;
using static Common.GeneralMessages;

[Authorize(Policy = "BuyerOnly")]
public class ReviewController : Controller
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
        bool productExists = await _productService.ProductExistsAsync(id);

        if (!productExists)
        {
            TempData[ErrorMessage] = ProductNotFoundErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        bool alreadyReviewed = await _reviewService.AlreadyReviewedProductAsync(id, User.GetId()!);

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

        bool productExists = await _productService.ProductExistsAsync(id);

        if (!productExists)
        {
            TempData[ErrorMessage] = ProductNotFoundErrorMessage;
            return RedirectToAction("Index", "Home");
        }

        bool alreadyReviewed = await _reviewService.AlreadyReviewedProductAsync(id, User.GetId()!);

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
    public IActionResult AddToCompany(string id)
    {
        return View();
    }

    private IActionResult GeneralError()
    {
        this.TempData[ErrorMessage] = UnexpectedErrorMessage;

        return RedirectToAction("Index", "Home");
    }
}
