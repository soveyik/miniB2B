namespace MiniB2B.Entities;

public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = null!;
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Beklemede;

    public User User { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
