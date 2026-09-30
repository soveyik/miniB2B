namespace MiniB2B.Entities;

public class SepetR
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public User User { get; set; } = null!;
    public ICollection<SepetD> SepetDs { get; set; } = new List<SepetD>();
}
