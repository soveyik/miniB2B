using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniB2B.Business;
using MiniB2B.DataAccess;
using MiniB2B.Web.Models;

namespace MiniB2B.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ProductService _productService;
    private readonly OrderService _orderService;
    private readonly MiniB2BContext _context;

    public HomeController(ProductService productService, OrderService orderService, MiniB2BContext context)
    {
        _productService = productService;
        _orderService = orderService;
        _context = context;
    }

    public async Task<IActionResult> Index(string? searchTerm)
    {
        var model = new ProductListViewModel
        {
            Products = await _productService.GetProductsAsync(searchTerm),
            GridConfigs = await _productService.GetGridConfigurationAsync(),
            Sliders = await _context.Sliders.Where(s => s.IsActive).OrderBy(s => s.OrderIndex).ToListAsync(),
            SearchTerm = searchTerm
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ProductDetail(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();

        return PartialView("_ProductDetailModal", product);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();

        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int productId, int quantity)
    {
        if (quantity <= 0) return Json(new { success = false, message = "Geçersiz miktar." });

        var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var result = await _orderService.AddToCartAsync(userId, productId, quantity);

        if (result)
        {
            var totalCount = await _orderService.GetCartItemCountAsync(userId);
            return Json(new { success = true, cartCount = totalCount });
        }
        
        return Json(new { success = false, message = "Ürün bulunamadı." });
    }
}
