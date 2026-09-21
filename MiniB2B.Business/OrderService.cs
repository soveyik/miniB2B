using Microsoft.EntityFrameworkCore;
using MiniB2B.DataAccess;
using MiniB2B.Entities;

namespace MiniB2B.Business;

public class OrderService
{
    private readonly MiniB2BContext _context;

    public OrderService(MiniB2BContext context)
    {
        _context = context;
    }

    public async Task<int> GetCartItemCountAsync(int userId)
    {
        var cart = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
        return cart?.CartItems.Sum(c => c.Quantity) ?? 0;
    }

    public async Task<Cart> GetOrCreateCartAsync(int userId)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }

    public async Task<bool> AddToCartAsync(int userId, int productId, int quantity)
    {
        var cart = await GetOrCreateCartAsync(userId);
        var product = await _context.Products.FindAsync(productId);

        if (product == null) return false;

        var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cart.CartItems.Add(new CartItem { ProductId = productId, Quantity = quantity });
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task RemoveFromCartAsync(int userId, int cartItemId)
    {
        var cart = await GetOrCreateCartAsync(userId);
        var item = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
        if (item != null)
        {
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateCartItemQuantityAsync(int userId, int cartItemId, int quantity)
    {
        var cart = await GetOrCreateCartAsync(userId);
        var item = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
        if (item != null)
        {
            if (quantity <= 0)
            {
                _context.CartItems.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }
            await _context.SaveChangesAsync();
        }
    }

    public async Task<(bool Success, string Message)> CompleteOrderAsync(int userId)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null || !cart.CartItems.Any())
        {
            return (false, "Sepetiniz boş.");
        }

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var order = new Order
            {
                UserId = userId,
                OrderNumber = "ORD-" + DateTime.Now.Year + "-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                OrderDate = DateTime.Now,
                Status = OrderStatus.Beklemede,
                TotalAmount = 0
            };

            foreach (var item in cart.CartItems)
            {
                var product = item.Product;
                if (product.StockQuantity < item.Quantity)
                {
                    await transaction.RollbackAsync();
                    return (false, $"{product.ProductName} ürünü için yeterli stok bulunmamaktadır. Mevcut stok: {product.StockQuantity}");
                }

                // Stok düş
                product.StockQuantity -= item.Quantity;

                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName,
                    UnitPrice = product.Price,
                    Quantity = item.Quantity,
                    TotalPrice = product.Price * item.Quantity
                };

                order.OrderItems.Add(orderItem);
                order.TotalAmount += orderItem.TotalPrice;
            }

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cart.CartItems); // Sepeti boşalt
            
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return (true, "Siparişiniz başarıyla oluşturuldu.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return (false, "Sipariş tamamlanırken bir hata oluştu: " + ex.Message);
        }
    }

    public async Task<List<Order>> GetOrdersAsync(int? userId = null)
    {
        var query = _context.Orders.Include(o => o.User).AsQueryable();
        if (userId.HasValue)
        {
            query = query.Where(o => o.UserId == userId.Value);
        }
        return await query.OrderByDescending(o => o.OrderDate).ToListAsync();
    }

    public async Task<Order?> GetOrderDetailsAsync(int orderId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }
    
    public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order != null)
        {
            order.Status = status;
            await _context.SaveChangesAsync();
        }
    }
}
