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

    public CompanyController(IImageService imageService, ICompanyService companyService, IAddressService addressService)
    {
        _imageService = imageService;
        _companyService = companyService;
        _addressService = addressService;
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

        string imageType = Path.GetExtension(model.ImageFile.FileName);

        if (imageType != ".jpg" && imageType != ".jpeg" && imageType != ".png")
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
}
