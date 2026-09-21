namespace MiniB2B.Entities;

public class GridColumnConfig
{
    public int Id { get; set; }
    public string TableName { get; set; } = null!;
    public string PropertyName { get; set; } = null!;
    public string HeaderText { get; set; } = null!;
    public int OrderIndex { get; set; }
    
    // "Text", "Image", "Price", "StockStatus", "Action"
    public string RenderType { get; set; } = "Text";
    public bool IsVisible { get; set; } = true;
    public string? Width { get; set; }
}
