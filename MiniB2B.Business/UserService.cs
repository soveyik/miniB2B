using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MiniB2B.DataAccess;
using MiniB2B.Entities;

namespace MiniB2B.Business;

public class UserService
{
    private readonly MiniB2BContext _context;

    public UserService(MiniB2BContext context)
    {
        _context = context;
    }

    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        var hash = HashPassword(password);
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == hash);
    }

    public async Task<(bool Success, string Message)> RegisterUserAsync(string firstName, string lastName, string email, string username, string password)
    {
        if (await _context.Users.AnyAsync(u => u.Username == username))
            return (false, "Bu kullanıcı adı zaten alınmış.");
            
        if (await _context.Users.AnyAsync(u => u.Email == email))
            return (false, "Bu e-posta adresi zaten kullanılıyor.");

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Username = username,
            PasswordHash = HashPassword(password),
            Role = "Customer" // Default role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return (true, "Kayıt başarılı.");
    }

    public string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}
