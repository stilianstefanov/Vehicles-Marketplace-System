using Microsoft.AspNetCore.Mvc;

namespace ThinkElectric.Web.Controllers;

public class CompanyController : Controller
{
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
}
