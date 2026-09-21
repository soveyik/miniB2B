using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniB2B.Business;
using System.Security.Claims;

namespace MiniB2B.Web.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var orders = await _orderService.GetOrdersAsync(userId);
        return View(orders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await _orderService.GetOrderDetailsAsync(id);
        if (order == null) return NotFound();

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        if (order.UserId != userId && !User.IsInRole("Admin"))
        {
            return Unauthorized();
        }

        return View(order);
    }
}
