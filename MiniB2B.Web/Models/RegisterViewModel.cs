using System.ComponentModel.DataAnnotations;

namespace MiniB2B.Web.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Ad zorunludur.")]
    public string FirstName { get; set; } = null!;
    
    [Required(ErrorMessage = "Soyad zorunludur.")]
    public string LastName { get; set; } = null!;
    
    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    public string Username { get; set; } = null!;
    
    [Required(ErrorMessage = "E-posta zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    public string Email { get; set; } = null!;
    
    [Required(ErrorMessage = "Şifre zorunludur.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    
    [Required(ErrorMessage = "Şifre doğrulama zorunludur.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
    public string ConfirmPassword { get; set; } = null!;
}
