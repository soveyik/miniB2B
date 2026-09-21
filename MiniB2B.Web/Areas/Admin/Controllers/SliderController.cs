using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniB2B.DataAccess;
using MiniB2B.Entities;

namespace MiniB2B.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SliderController : Controller
{
    private readonly MiniB2BContext _context;

    public SliderController(MiniB2BContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var sliders = await _context.Sliders.OrderBy(s => s.OrderIndex).ToListAsync();
        return View(sliders);
    }

    public IActionResult Create()
    {
        return View(new Slider { OrderIndex = 0, IsActive = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Slider slider, IFormFile? imageFile)
    {
        if (ModelState.IsValid)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = "slider_" + DateTime.Now.Ticks + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                slider.ImageUrl = "/images/" + fileName;
            }

            _context.Add(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(slider);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var slider = await _context.Sliders.FindAsync(id);
        if (slider == null) return NotFound();

        return View(slider);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Slider slider, IFormFile? imageFile)
    {
        if (id != slider.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileName = "slider_" + DateTime.Now.Ticks + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    slider.ImageUrl = "/images/" + fileName;
                }

                _context.Update(slider);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SliderExists(slider.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(slider);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var slider = await _context.Sliders.FindAsync(id);
        if (slider != null)
        {
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
        return Json(new { success = false, message = "Slider bulunamadı." });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateOrder([FromBody] List<int> orderedIds)
    {
        if (orderedIds == null || !orderedIds.Any()) return Json(new { success = false });

        for (int i = 0; i < orderedIds.Count; i++)
        {
            var slider = await _context.Sliders.FindAsync(orderedIds[i]);
            if (slider != null)
            {
                slider.OrderIndex = i + 1;
                _context.Update(slider);
            }
        }
        await _context.SaveChangesAsync();
        return Json(new { success = true });
    }

    private bool SliderExists(int id)
    {
        return _context.Sliders.Any(e => e.Id == id);
    }
}
