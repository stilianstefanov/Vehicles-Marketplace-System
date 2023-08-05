namespace ThinkElectric.Services;

using Microsoft.EntityFrameworkCore;

using Contracts;
using Data;
using Data.Models;
using Web.ViewModels.Address;
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
                OrderItems = o.OrderItems.Select(oi => new OrderItemBuyerViewModel()
                {
                    ProductName = oi.Product.Name,
                    Price = oi.Product.Price.ToString("F2"),
                    Quantity = oi.Quantity,
                    TotalSum = (oi.Product.Price * oi.Quantity).ToString("F2"),
                    CompanyName = oi.Product.Company.Name
                })
                    .ToArray()
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

    public async Task<IEnumerable<OrderAllViewModel>> GetAllByUserAsync(string userId)
    {
        IEnumerable<OrderAllViewModel> orderModels = await _dbContext
            .Orders
            .Where(o => o.UserId.ToString() == userId && o.IsConfirmedByUser)
            .OrderByDescending(o => o.CreatedOn)
            .Select(o => new OrderAllViewModel()
            {
                CreatedOn = o.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                TotalSum = o.OrderItems.Sum(oi => oi.Product.Price * oi.Quantity).ToString("F2"),
                Status = o.OrderItems.Any(o => !o.IsFulfilled) ? "Not Fulfilled" : "Fulfilled",
                OrderItems = o.OrderItems.Select(oi => new OrderItemBuyerViewModel()
                    {
                        ProductName = oi.Product.Name,
                        Price = oi.Product.Price.ToString("F2"),
                        Quantity = oi.Quantity,
                        TotalSum = (oi.Product.Price * oi.Quantity).ToString("F2"),
                        Status = oi.IsFulfilled ? "Fulfilled" : "Not Fulfilled",
                        CompanyName = oi.Product.Company.Name,
                        CompanyId = oi.Product.CompanyId.ToString(),
                    })
                    .ToArray()
            })
            .ToArrayAsync();

        return orderModels;
    }

    public async Task<IEnumerable<OrderItemCompanyViewModel>> GetAllByCompanyAsync(string companyId)
    {
        IEnumerable<OrderItemCompanyViewModel> orderItemsModels = await _dbContext
            .OrderItems
            .Where(oi => oi.Product.CompanyId.ToString() == companyId && oi.Order.IsConfirmedByUser)
            .OrderByDescending(oi => oi.Order.CreatedOn)
            .Select(oi => new OrderItemCompanyViewModel()
            {
                Id = oi.Id.ToString(),
                ProductName = oi.Product.Name,
                Price = oi.Product.Price.ToString("F2"),
                Quantity = oi.Quantity,
                TotalSum = (oi.Product.Price * oi.Quantity).ToString("F2"),
                Status = oi.IsFulfilled ? "Fulfilled" : "Not Fulfilled",
                BuyerName = oi.Order.User.FirstName + " " + oi.Order.User.LastName,
                BuyerPhoneNumber = oi.Order.User.PhoneNumber,
                Address = new AddressViewModel()
                {
                    Country = oi.Order.User.Address!.Country,
                    City = oi.Order.User.Address.City,
                    Street = oi.Order.User.Address.Street,
                    ZipCode = oi.Order.User.Address.ZipCode,
                },
            })
            .ToArrayAsync();

        return orderItemsModels;
    }

    public async Task<bool> OrderItemExistsAsync(string orderItemId)
    {
        bool orderItemExists = await _dbContext.OrderItems
            .AnyAsync(oi => oi.Id.ToString() == orderItemId);

        return orderItemExists;
    }

    public async Task<bool> IsOrderItemFromCompanyAsync(string orderItemId, string companyId)
    {
        bool isOrderItemFromCompany = await _dbContext.OrderItems
            .AnyAsync(oi => oi.Id.ToString() == orderItemId && oi.Product.CompanyId.ToString() == companyId);

        return isOrderItemFromCompany;
    }

    public async Task MarkAsFulfilledAsync(string orderItemId)
    {
        OrderItem orderItem = await _dbContext.OrderItems
            .Where(oi => oi.Id.ToString() == orderItemId)
            .FirstAsync();

        orderItem.IsFulfilled = true;

        await _dbContext.SaveChangesAsync();
    }
}
