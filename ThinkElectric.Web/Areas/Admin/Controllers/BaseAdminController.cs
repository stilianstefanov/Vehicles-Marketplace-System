namespace ThinkElectric.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static Common.GeneralApplicationConstants;
using static Common.ErrorMessages;
using static Common.NotificationsMessagesConstants;

[Area(AdminAreaName)]
[Authorize(Roles = AdminRoleName)]
public class BaseAdminController : Controller
{
    protected IActionResult GeneralError()
    {
        this.TempData[ErrorMessage] = UnexpectedErrorMessage;

        return RedirectToAction("Index", "Home", new { Area = AdminAreaName});
    }
}
