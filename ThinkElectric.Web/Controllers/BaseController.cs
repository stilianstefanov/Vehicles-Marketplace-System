namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using static Common.ErrorMessages;
using static Common.NotificationsMessagesConstants;

[Authorize]
public class BaseController : Controller
{
    protected IActionResult GeneralError()
    {
        this.TempData[ErrorMessage] = UnexpectedErrorMessage;

        return RedirectToAction("Index", "Home");
    }
}
