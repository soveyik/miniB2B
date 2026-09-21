using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniB2B.DataAccess;
using MiniB2B.Entities;

namespace MiniB2B.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductController : Controller
{
    private readonly MiniB2BContext _context;

    public ProductController(MiniB2BContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.Include(p => p.Category).OrderByDescending(p => p.Id).ToListAsync();
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        ModelState.Remove("Category");
        if (ModelState.IsValid)
        {
            if (await _context.Products.AnyAsync(p => p.ProductCode == product.ProductCode))
            {
                ModelState.AddModelError("ProductCode", "Bu ürün kodu zaten mevcut.");
                ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
                return View(product);
            }

            product.CreatedAt = DateTime.Now;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id) return NotFound();

        ModelState.Remove("Category");
        if (ModelState.IsValid)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing == null) return NotFound();

            if (existing.ProductCode != product.ProductCode && await _context.Products.AnyAsync(p => p.ProductCode == product.ProductCode))
            {
                ModelState.AddModelError("ProductCode", "Bu ürün kodu zaten mevcut.");
                ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
                return View(product);
            }

            existing.ProductName = product.ProductName;
            existing.ProductCode = product.ProductCode;
            existing.CategoryId = product.CategoryId;
            existing.Brand = product.Brand;
            existing.ManufacturerCode = product.ManufacturerCode;
            existing.SpecialCode1 = product.SpecialCode1;
            existing.SpecialCode2 = product.SpecialCode2;
            existing.Description = product.Description;
            existing.ImageUrl = product.ImageUrl;
            existing.Price = product.Price;
            existing.StockQuantity = product.StockQuantity;
            existing.CriticalStockLevel = product.CriticalStockLevel;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
        return View(product);
    }
}
