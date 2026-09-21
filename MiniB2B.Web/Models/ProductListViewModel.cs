using MiniB2B.Entities;

namespace MiniB2B.Web.Models;

public class ProductListViewModel
{
    public List<Product> Products { get; set; } = new();
    public List<GridColumnConfig> GridConfigs { get; set; } = new();
    public List<Slider> Sliders { get; set; } = new();
    public string? SearchTerm { get; set; }
}
