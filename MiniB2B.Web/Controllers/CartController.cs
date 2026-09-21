using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniB2B.Business;
using System.Security.Claims;

namespace MiniB2B.Web.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly OrderService _orderService;

    public CartController(OrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var cart = await _orderService.GetOrCreateCartAsync(userId);
        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _orderService.RemoveFromCartAsync(userId, id);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, int quantity)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _orderService.UpdateCartItemQuantityAsync(userId, id, quantity);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> CompleteOrder()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _orderService.CompleteOrderAsync(userId);

        if (result.Success)
        {
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index", "Order");
        }
        else
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}
