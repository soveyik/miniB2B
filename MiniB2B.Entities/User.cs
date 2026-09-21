using System.ComponentModel.DataAnnotations;

namespace MiniB2B.Entities;

public class User
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Ad zorunludur.")]
    public string FirstName { get; set; } = null!;
    
    [Required(ErrorMessage = "Soyad zorunludur.")]
    public string LastName { get; set; } = null!;
    
    [Required(ErrorMessage = "E-posta zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    public string Email { get; set; } = null!;
    
    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    public string Username { get; set; } = null!;
    
    [Required(ErrorMessage = "Şifre zorunludur.")]
    public string PasswordHash { get; set; } = null!;
    
    public string? PhoneNumber { get; set; }
    
    // "Admin" or "Customer"
    public string Role { get; set; } = "Customer";
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
