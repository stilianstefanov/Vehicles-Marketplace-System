namespace ThinkElectric.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

public class ReviewController : BaseAdminController
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet]
    public async Task<IActionResult> AllProductReviews()
    {
        try
        {
            var reviews = await _reviewService.GetAllProductReviewsAsync();

            return View(reviews);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
    public async Task<IActionResult> AllCompanyReviews()
    {
        try
        {
           var reviews = await _reviewService.GetAllCompanyReviewsAsync();

            return View(reviews);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        bool reviewExists = await _reviewService.ReviewExistsAsync(id);

        if (!reviewExists)
        {
            return GeneralError();
        }

        try
        {
            await _reviewService.DeleteAsync(id);

            return Redirect(Request.Headers["Referer"].ToString());
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }
}
