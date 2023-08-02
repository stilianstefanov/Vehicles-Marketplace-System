namespace ThinkElectric.Services;

using Contracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.CartItem;
using Web.ViewModels.Order;
using Web.ViewModels.OrderItem;

public class OrderService : IOrderService
{
    private readonly ThinkElectricDbContext _dbContext;

    public OrderService(ThinkElectricDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<string> CreateAsync(IEnumerable<CartItemViewModel> cartItems, string userId)
    {
        Order order = new Order()
        {
            CreatedOn = DateTime.UtcNow,
            UserId = Guid.Parse(userId),
        };

        foreach (CartItemViewModel cartItem in cartItems)
        {
            OrderItem orderItem = new OrderItem()
            {
                ProductId = Guid.Parse(cartItem.ProductId),
                Quantity = cartItem.Quantity,
                Order = order,
            };

            order.OrderItems.Add(orderItem);
        }

        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return order.Id.ToString();
    }

    public async Task<OrderDetailsViewModel> GetOrderDetailsAsync(string id)
    {
        OrderDetailsViewModel model = await _dbContext.Orders
            .Where(o => o.Id.ToString() == id)
            .Select(o => new OrderDetailsViewModel()
            {
                Id = o.Id.ToString(),
                CreatedOn = o.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                TotalSum = o.OrderItems.Sum(oi => oi.Product.Price * oi.Quantity).ToString("F2"),
                OrderItems = o.OrderItems.Select(oi => new OrderItemViewModel()
                {
                    ProductName = oi.Product.Name,
                    Price = oi.Product.Price.ToString("F2"),
                    Quantity = oi.Quantity,
                    TotalSum = (oi.Product.Price * oi.Quantity).ToString("F2"),
                })
            })
            .FirstAsync();

        return model;
    }

    public async Task<bool> OrderExistsAsync(string id)
    {
        bool orderExists = await _dbContext.Orders
            .AnyAsync(o => o.Id.ToString() == id);

        return orderExists;
    }

    public async Task<bool> IsOrderFromUserAsync(string id, string userId)
    {
        bool isOrderFromUser = await _dbContext.Orders
            .AnyAsync(o => o.Id.ToString() == id && o.UserId.ToString() == userId);

        return isOrderFromUser;
    }

    public async Task CancelAsync(string id)
    {
        IEnumerable<OrderItem> orderItems = await _dbContext
            .OrderItems           
            .Where(oi => oi.OrderId.ToString() == id)
            .ToArrayAsync();

        _dbContext.OrderItems.RemoveRange(orderItems);

        Order order = await _dbContext.Orders
            .Where(o => o.Id.ToString() == id)
            .FirstAsync();

        _dbContext.Orders.Remove(order);

        await _dbContext.SaveChangesAsync();
    }

    public async Task ConfirmAsync(string id)
    {
        Order order = await _dbContext.Orders
            .Where(o => o.Id.ToString() == id)
            .FirstAsync();

        order.IsConfirmedByUser = true;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsOrderExisting(string id)
    {
        bool isOrderExisting = await _dbContext.Orders
            .AnyAsync(o => o.Id.ToString() == id);

        return isOrderExisting;
    }
}
