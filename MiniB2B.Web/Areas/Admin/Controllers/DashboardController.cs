using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniB2B.DataAccess;

namespace MiniB2B.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    private readonly MiniB2BContext _context;

    public DashboardController(MiniB2BContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.TotalOrders = await _context.Orders.CountAsync();
        ViewBag.TotalProducts = await _context.Products.CountAsync();
        ViewBag.TotalUsers = await _context.Users.CountAsync();

        return View();
    }
}
