namespace ThinkElectric.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

public class OrderController : BaseAdminController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        try
        {
            var orders = await _orderService.GetAllOrdersAsync();

            return View(orders);
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }
}
