namespace ThinkElectric.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

using static Common.GeneralApplicationConstants;

public class CompanyController : BaseAdminController
{
    private readonly ICompanyService _companyService;
    private readonly IUserService _userService;
    private readonly IProductService _productService;

    public CompanyController(
        ICompanyService companyService, 
        IUserService userService,
        IProductService productService)
    {
        _companyService = companyService;
        _userService = userService;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        try
        {
            var companies = await _companyService.GetAllCompaniesForAdminAsync();

            return View(companies);
        }
        catch (Exception) 
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Block(string id)
    {
        bool companyExists = await _companyService.CompanyExistsByIdForAdminAsync(id);

        if (!companyExists)
        {
            return GeneralError();
        }

        try
        {
            var userId = await _companyService.BlockCompanyByIdAsync(id);

            await _userService.BlockUserByIdAsync(userId);

            await _productService.DeleteAllProductsByCompanyIdAsync(id);

            return RedirectToAction("All", "Company", new { Area = AdminAreaName });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Unblock(string id)
    {
        bool companyExists = await _companyService.CompanyExistsByIdForAdminAsync(id);

        if (!companyExists)
        {
            return GeneralError();
        }

        try
        {
            var userId = await _companyService.UnblockCompanyByIdAsync(id);

            await _userService.UnblockUserByIdAsync(userId);

            await _productService.RestoreAllProductsByCompanyIdAsync(id);

            return RedirectToAction("All", "Company", new { Area = AdminAreaName });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }
}
