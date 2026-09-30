namespace MiniB2B.Entities;

public class SiparisD
{
    public int Id { get; set; }
    public int SiparisRId { get; set; }
    public int ProductId { get; set; }
    public string ProductCode { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }

    public SiparisR SiparisR { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
