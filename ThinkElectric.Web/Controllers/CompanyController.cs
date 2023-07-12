namespace ThinkElectric.Web.Controllers;

using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ViewModels.Company;
using static Common.ValidationErrors;

public class CompanyController : Controller
{
    private readonly IImageService _imageService;
    private readonly ICompanyService _companyService;
    private readonly IAddressService _addressService;
    private readonly IReviewService _reviewService;

    public CompanyController(IImageService imageService,
        ICompanyService companyService,
        IAddressService addressService,
        IReviewService reviewService)
    {
        _imageService = imageService;
        _companyService = companyService;
        _addressService = addressService;
        _reviewService = reviewService;
    }

    [HttpGet]
    public async Task<IActionResult> Create(string id)
    {
        var companyModel = await _companyService.GetCompanyCreateViewModelAsync(id);

        return View(companyModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm]CompanyCreateViewModel model, string id)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.FoundedDate == null)
        {
            ModelState.AddModelError(nameof(model.FoundedDate), DateErrorMessage);
            return View(model);
        }

        if (model.ImageFile == null || model.ImageFile.Length == 0)
        {
            ModelState.AddModelError(nameof(model.ImageFile), ImageRequiredErrorMessage);
            return View(model);
        }

        var imageType = model.ImageFile.ContentType;

        if (imageType != "image/jpg" && imageType != "image/jpeg" && imageType != "image/png")
        {
            ModelState.AddModelError(nameof(model.ImageFile), ImageFormatErrorMessage);
            return View(model);
        }

        try
        {
            var imageId = await _imageService.CreateAsync(model.ImageFile);

            var address = await _addressService.CreateAsync(model.Address);

            await _companyService.CreateAsync(model, imageId, address, id);
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "An unexpected error occurred while proceeding you request.");
        }

        return RedirectToAction("Login", "User");
    }

    [HttpGet]
    public async Task<IActionResult> Details()
    {
        var company = await _companyService.GetCompanyDetailsByUserIdAsync(User.GetId()!);

        if (company == null)
        {
            return NotFound();
        }

        try
        {
            company.Address = await _addressService.GetAddressByCompanyIdAsync(company.Id);

            company.Image = await _imageService.GetImageByIdAsync(company.ImageId);

            company.Reviews = await _reviewService.GetReviewsByCompanyIdAsync(company.Id);
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "An unexpected error occurred while proceeding you request.");
        }

        return View(company);
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        return View();
    }

}
