namespace ThinkElectric.Web.Controllers;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;
using ViewModels.Company;
using Infrastructure.Extensions;

using static Common.ErrorMessages;
using static Common.NotificationsMessagesConstants;
using static Common.GeneralMessages;


public class CompanyController : BaseController
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
    [HttpGet]
    public async Task<IActionResult> Create(string id)
    {
        try
        {
            var userExists = await _userService.UserExistsByIdAsync(id);

            if (!userExists)
            {
                return GeneralError();
            }
        }
        catch
        {
            return GeneralError();
        }

        var isCompany = await _userService.IsRegisteredAsCompanyAsync(id);

        if (!isCompany)
        {
            this.TempData[ErrorMessage] = NotRegisteredAsCompanyErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        var hasAlreadyCreatedCompany = await _companyService.HasAlreadyCreatedCompanyAsync(id);

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
            return GeneralError();
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromForm] CompanyCreateViewModel model, string id)
    {
        try
        {
            var userExists = await _userService.UserExistsByIdAsync(id);

            if (!userExists)
            {
                return GeneralError();
            }
        }
        catch
        {
            return GeneralError();
        }

        var isCompany = await _userService.IsRegisteredAsCompanyAsync(id);

        if (!isCompany)
        {
            this.TempData[ErrorMessage] = NotRegisteredAsCompanyErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        var hasAlreadyCreatedCompany = await _companyService.HasAlreadyCreatedCompanyAsync(id);

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
            TempData[ErrorMessage] = ImageRequiredErrorMessage;
            return View(model);
        }

        var imageType = model.ImageFile.ContentType;

        if (imageType != "image/jpg" && imageType != "image/jpeg" && imageType != "image/png")
        {
            TempData[ErrorMessage] = ImageFormatErrorMessage;
            return View(model);
        }

        try
        {
            var imageId = await _imageService.CreateAsync(model.ImageFile);

            var addressId = await _addressService.CreateAsync(model.Address);

            var companyId = await _companyService.CreateAsync(model, imageId, addressId, id);

            await _userService.AddClaimAsync(id, "companyId", companyId);

            TempData[SuccessMessage] = RegistrationSuccessMessage;

            return RedirectToAction("Login", "User");
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
            return GeneralError();
        }
    }

    [Authorize(Policy = "CompanyOnly")]
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var companyExists = await _companyService.CompanyExistsByIdAsync(id);

        if (!companyExists)
        {
            this.TempData[ErrorMessage] = CompanyNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        var isUserCompanyOwner = await _companyService.IsUserCompanyOwnerAsync(id, User.GetId()!);

        if (!isUserCompanyOwner)
        {
            this.TempData[ErrorMessage] = NotOwnerOfCompanyErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        try
        {
            var companyModel = await _companyService.GetCompanyEditViewModelByIdAsync(id);

            companyModel.Address = await _addressService.GetAddressEditByIdAsync(companyModel.AddressId!);

            companyModel.CurrentImage = await _imageService.GetImageByIdAsync(companyModel.ImageId!);

            return View(companyModel);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [Authorize(Policy = "CompanyOnly")]
    [HttpPost]
    public async Task<IActionResult> Edit([FromForm] CompanyEditViewModel model, string id)
    {
        var companyExists = await _companyService.CompanyExistsByIdAsync(id);

        if (!companyExists)
        {
            this.TempData[ErrorMessage] = CompanyNotFoundErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        var isUserCompanyOwner = await _companyService.IsUserCompanyOwnerAsync(id, User.GetId()!);

        if (!isUserCompanyOwner)
        {
            this.TempData[ErrorMessage] = NotOwnerOfCompanyErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            var imageId = await _companyService.GetImageIdByCompanyIdAsync(id);

            model.CurrentImage = await _imageService.GetImageByIdAsync(imageId);
            return View(model);
        }

        if (model.FoundedDate == null)
        {
            var imageId = await _companyService.GetImageIdByCompanyIdAsync(id);

            model.CurrentImage = await _imageService.GetImageByIdAsync(imageId);

            ModelState.AddModelError(nameof(model.FoundedDate), DateErrorMessage);
            return View(model);
        }

        if (model.NewImage != null && model.NewImage.Length != 0)
        {
            var imageType = model.NewImage.ContentType;

            if (imageType != "image/jpg" && imageType != "image/jpeg" && imageType != "image/png")
            {
                var imageId = await _companyService.GetImageIdByCompanyIdAsync(id);

                model.CurrentImage = await _imageService.GetImageByIdAsync(imageId);

                TempData[ErrorMessage] = ImageFormatErrorMessage;
                return View(model);
            }
        }

        try
        {
            if (model.NewImage != null && model.NewImage.Length != 0)
            {
                var imageId = await _companyService.GetImageIdByCompanyIdAsync(id);

                await _imageService.UpdateAsync(imageId, model.NewImage);
            }

            var addressId = await _companyService.GetAddressIdByCompanyIdAsync(id);

            await _addressService.EditAsync(addressId, model.Address);

            await _companyService.EditAsync(model, id);

            TempData[SuccessMessage] = CompanyEditSuccessMessage;

            return RedirectToAction("Details", "Company", new { id });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult All()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> AllFilteredAndPaged(CompaniesAllQueryModel queryModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        queryModel = await _companyService.AllAsync(queryModel);

        queryModel.Companies = queryModel
            .Companies
            .Select(async company =>
            {
            company.Image = await _imageService.GetImageByIdAsync(company.ImageId);
            return company;
        })
            .Select(t => t.Result).ToList();

        return Json(queryModel);
    }
}
