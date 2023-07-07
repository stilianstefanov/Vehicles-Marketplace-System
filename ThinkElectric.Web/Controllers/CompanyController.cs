namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ViewModels.Company;

public class CompanyController : Controller
{
    private readonly IImageService _imageService;

    public CompanyController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CompanyCreateViewModel model, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (imageFile == null || imageFile.Length == 0)
        {
            ModelState.AddModelError("Image", "Image is required.");
            return View(model);
        }

        var imageId = await _imageService.CreateAsync(imageFile);


        return RedirectToAction("Index", "Home");
    }
}
