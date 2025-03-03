using BlazorAppShopping.Data;
using BlazorAppShopping.Entities;
using Microsoft.EntityFrameworkCore;

public class OrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetOrdersByUserAsync(string userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
    {
        return await _context.OrderItems
            .Where(oi => oi.OrderId == orderId)
            .Include(oi => oi.Product) 
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task SaveOrderAsync(string userId, List<CartItem> cartItems, string transactionId, string paymentMethod)
    {

        var order = new Order
        {
            UserId = userId,
            TotalPrice = cartItems.Sum(item => item.Product.Price * item.Quantity),
            Status = "Pending",
            OrderDate = DateTime.UtcNow
        };


        _context.Orders.Add(order);
        await _context.SaveChangesAsync();


        foreach (var cartItem in cartItems)
        {
            var orderItem = new OrderItem
            {
                OrderId = order.OrderId,
                ProductId = cartItem.Product.ProductId,
                Quantity = cartItem.Quantity,
                UnitPrice = cartItem.Product.Price
            };
            _context.OrderItems.Add(orderItem);
        }

        string status = "Completed";
        if (paymentMethod == "COD")
        {
            status = "Pending";
        }
        var payment = new Payment
        {
            OrderId = order.OrderId,
            UserId = userId,
            TotalPrice = order.TotalPrice,
            Status = status,
            PaymentDate = DateTime.UtcNow,
            PaymentMethod = paymentMethod,
            TransactionId = transactionId
        };
        _context.Payments.Add(payment);

        await _context.SaveChangesAsync();
    }
}
