namespace MiniB2B.Entities;

public class SepetD
{
    public int Id { get; set; }
    public int SepetRId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public SepetR SepetR { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
