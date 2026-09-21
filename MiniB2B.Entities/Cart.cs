namespace MiniB2B.Entities;

public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public User User { get; set; } = null!;
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
