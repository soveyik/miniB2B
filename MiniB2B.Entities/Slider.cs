using System.ComponentModel.DataAnnotations;

namespace MiniB2B.Entities;

public class Slider
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Görsel URL alanı zorunludur.")]
    [StringLength(500)]
    public string ImageUrl { get; set; } = null!;

    [StringLength(200)]
    public string? Title { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(500)]
    public string? LinkUrl { get; set; }

    public int OrderIndex { get; set; } = 0;

    public bool IsActive { get; set; } = true;
}
