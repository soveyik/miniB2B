using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniB2B.DataAccess;
using MiniB2B.Entities;

namespace MiniB2B.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly MiniB2BContext _context;

    public UserController(MiniB2BContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _context.Users.OrderByDescending(u => u.Id).ToListAsync();
        return View(users);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, User user)
    {
        if (id != user.Id) return NotFound();

        // Şifre hash kısmı frontend'den gelmeyeceği için ModelState'den çıkaralım
        ModelState.Remove("PasswordHash");

        if (ModelState.IsValid)
        {
            var existing = await _context.Users.FindAsync(id);
            if (existing == null) return NotFound();

            if (existing.Username != user.Username && await _context.Users.AnyAsync(u => u.Username == user.Username))
            {
                ModelState.AddModelError("Username", "Bu kullanıcı adı zaten mevcut.");
                return View(user);
            }
            if (existing.Email != user.Email && await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                ModelState.AddModelError("Email", "Bu e-posta adresi zaten mevcut.");
                return View(user);
            }

            existing.FirstName = user.FirstName;
            existing.LastName = user.LastName;
            existing.Email = user.Email;
            existing.Username = user.Username;
            existing.PhoneNumber = user.PhoneNumber;
            existing.Role = user.Role;

            // Şifre güncelleniyorsa backend'de hashlenmeli ama biz burada sadece bilgileri güncelliyoruz.
            // Şifre güncelleme ayrı bir metotla yapılmalı (karmaşıklığı önlemek adına şimdilik pas geçildi).

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }
}
