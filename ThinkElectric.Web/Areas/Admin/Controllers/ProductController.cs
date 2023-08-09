namespace ThinkElectric.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

using static Common.GeneralApplicationConstants;

public class ProductController : BaseAdminController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        try
        {
           var products = await _productService.GetAllProductsForAdminAsync();

            return View(products);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Restore(string id)
    {
        bool productExists = await _productService.ProductExistsForAdminAsync(id);

        if (!productExists)
        {
            return GeneralError();
        }

        try
        {
            await _productService.RestoreProductAsync(id);

            return RedirectToAction("All", "Product", new { Area = AdminAreaName });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        bool productExists = await _productService.ProductExistsForAdminAsync(id);

        if (!productExists)
        {
            return GeneralError();
        }

        try
        {
            await _productService.DeleteAsync(id);

            return RedirectToAction("All", "Product", new { Area = AdminAreaName });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }
}
