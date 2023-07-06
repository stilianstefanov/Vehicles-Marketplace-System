namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using ViewModels.Company;

public class CompanyController : Controller
{
    private readonly IFileService _fileService;

    public CompanyController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CompanyCreateViewModel model, IFormFile imageFile)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        string imageUrl = await _fileService.UploadFileAsync(imageFile, "companies");



        return RedirectToAction("Index", "Home");
    }
}
