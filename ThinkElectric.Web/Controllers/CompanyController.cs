namespace ThinkElectric.Web.Controllers;

using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Serializers;
using Services.Contracts;
using ViewModels.Company;
using static Common.ErrorMessages;
using static Common.NotificationsMessagesConstants;


[Authorize]
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


    [AllowAnonymous]
    public async Task<IActionResult> Create(string id)
    {
        bool userExists = await _userService.UserExistsByIdAsync(id);

        if (!userExists)
        {
            this.TempData[ErrorMessage] = UnexpectedErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        bool isCompany = await _userService.IsRegisteredAsCompanyAsync(id);

        if (!isCompany)
        {
            this.TempData[ErrorMessage] = NotRegisteredAsCompanyErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        bool hasAlreadyCreatedCompany = await _companyService.HasAlreadyCreatedCompanyAsync(id);

        if (hasAlreadyCreatedCompany)
        {
            this.TempData[ErrorMessage] = AlreadyRegisteredAsCompanyErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        try
        {
            var companyModel = await _companyService.GetCompanyCreateViewModelByUserIdAsync(id);

            return View(companyModel);
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = UnexpectedErrorMessage;

            return RedirectToAction("Index", "Home");
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromForm]CompanyCreateViewModel model, string id)
    {
        bool userExists = await _userService.UserExistsByIdAsync(id);

        if (!userExists)
        {
            this.TempData[ErrorMessage] = UnexpectedErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        bool isCompany = await _userService.IsRegisteredAsCompanyAsync(id);

        if (!isCompany)
        {
            this.TempData[ErrorMessage] = NotRegisteredAsCompanyErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        bool hasAlreadyCreatedCompany = await _companyService.HasAlreadyCreatedCompanyAsync(id);

        if (hasAlreadyCreatedCompany)
        {
            this.TempData[ErrorMessage] = AlreadyRegisteredAsCompanyErrorMessage;

            return RedirectToAction("Index", "Home");
        }

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

            await _userService.AddClaimAsync(id, "companyId", companyId);

            return RedirectToAction("Login", "User");
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = UnexpectedErrorMessage;

            return RedirectToAction("Index", "Home");
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(string id)
    {
        var companyModel = await _companyService.GetCompanyDetailsByIdAsync(id);

        if (companyModel == null)
        {
            TempData[ErrorMessage] = CompanyNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        try
        {
            companyModel.Address = await _addressService.GetAddressDetailsByIdAsync(companyModel.AddressId);

            companyModel.Image = await _imageService.GetImageByIdAsync(companyModel.ImageId);

            companyModel.Reviews = await _reviewService.GetReviewsByCompanyIdAsync(companyModel.Id);

            return View(companyModel);
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = UnexpectedErrorMessage;

            return RedirectToAction("Index", "Home");
        }
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        bool companyExists = await _companyService.CompanyExistsByIdAsync(id);

        if (!companyExists)
        {
            this.TempData[ErrorMessage] = CompanyNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        bool isUserCompanyOwner = await _companyService.IsUserCompanyOwnerAsync(id, User.GetId()!);

        if (!isUserCompanyOwner)
        {
            this.TempData[ErrorMessage] = NotOwnerOfCompanyErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        try
        {
            CompanyEditViewModel companyModel = await _companyService.GetCompanyEditViewModelByIdAsync(id);

            companyModel.Address = await _addressService.GetAddressEditByIdAsync(companyModel.AddressId);

            companyModel.CurrentImage = await _imageService.GetImageByIdAsync(companyModel.ImageId!);

            return View(companyModel);
        }
        catch (Exception)
        {
            this.TempData[ErrorMessage] = UnexpectedErrorMessage;

            return RedirectToAction("Index", "Home");
        }
    }
}
