namespace ThinkElectric.Web.Controllers;

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
    private readonly IUserService _userService;

    public CompanyController(IImageService imageService,
        ICompanyService companyService,
        IAddressService addressService,
        IReviewService reviewService,
        IUserService userService)
    {
        _imageService = imageService;
        _companyService = companyService;
        _addressService = addressService;
        _reviewService = reviewService;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Create(string id)
    {
        //ToDO - check if user is exists and if has company role with the user service!!!!!!!!!!!!!!!!!!!
        //ToDo - check if the user already has a company created !!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        var companyModel = await _companyService.GetCompanyCreateViewModelByUserIdAsync(id);

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

            var addressId = await _addressService.CreateAsync(model.Address);

            var companyId = await _companyService.CreateAsync(model, imageId, addressId, id);

            await _userService.AddClaimAsync(id, "Company", companyId);
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "An unexpected error occurred while proceeding you request.");
        }

        return RedirectToAction("Login", "User");
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var companyModel = await _companyService.GetCompanyDetailsByIdAsync(id);

        if (companyModel == null)
        {
            return NotFound();
        }

        try
        {
            companyModel.Address = await _addressService.GetAddressByIdAsync(companyModel.AddressId);

            companyModel.Image = await _imageService.GetImageByIdAsync(companyModel.ImageId);

            companyModel.Reviews = await _reviewService.GetReviewsByCompanyIdAsync(companyModel.Id);
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "An unexpected error occurred while proceeding you request.");
        }

        return View(companyModel);
    }
}
