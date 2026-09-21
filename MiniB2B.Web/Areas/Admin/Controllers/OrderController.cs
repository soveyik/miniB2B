using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniB2B.Business;
using MiniB2B.Entities;

namespace MiniB2B.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class OrderController : Controller
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetOrdersAsync();
        return View(orders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await _orderService.GetOrderDetailsAsync(id);
        if (order == null) return NotFound();
        return View(order);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateStatus(int id, string status)
    {
        if (Enum.TryParse<OrderStatus>(status, out var parsedStatus))
        {
            await _orderService.UpdateOrderStatusAsync(id, parsedStatus);
        }
        return RedirectToAction("Details", new { id });
    }
}
