using System.ComponentModel.DataAnnotations;

namespace MiniB2B.Entities;

public class Product
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Ürün kodu zorunludur.")]
    public string ProductCode { get; set; } = null!;
    
    [Required(ErrorMessage = "Ürün adı zorunludur.")]
    public string ProductName { get; set; } = null!;
    
    public string? Description { get; set; }
    public string? Brand { get; set; }
    public string? ManufacturerCode { get; set; }
    public string? SpecialCode1 { get; set; }
    public string? SpecialCode2 { get; set; }
    public string? ImageUrl { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Stok miktarı negatif olamaz.")]
    public int StockQuantity { get; set; }
    
    [Range(0, double.MaxValue, ErrorMessage = "Fiyat negatif olamaz.")]
    public decimal Price { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Kritik stok seviyesi negatif olamaz.")]
    public int CriticalStockLevel { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}
