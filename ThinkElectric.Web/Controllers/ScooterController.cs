namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class ScooterController : Controller
{
    [Authorize(Policy = "CompanyOnly")]
    public IActionResult Create()
    {
        return View();
    }
}
