namespace ThinkElectric.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Cart;

[Authorize]
public class OrderController : Controller
{
    [HttpPost]
    public IActionResult Create([FromForm] IEnumerable<CartItemViewModel> cartItems)
    {
        // 1. Create Order entity
        // 2. create orderItems entities
        // 3. Redirect to Orders/Details/{id}
        // 4. Redirect to Home/Index - 2 buttons - Cancel, Confirm

        return Ok();
    }
}
