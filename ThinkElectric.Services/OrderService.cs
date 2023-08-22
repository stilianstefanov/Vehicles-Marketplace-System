namespace ThinkElectric.Services;

using Microsoft.EntityFrameworkCore;

using Contracts;
using Data;
using Data.Models;
using Web.ViewModels.Address;
using Web.ViewModels.CartItem;
using Web.ViewModels.Order;
using Web.ViewModels.OrderItem;
using OfficeOpenXml;

public class OrderService : IOrderService
{
    private readonly ThinkElectricDbContext _dbContext;

    public OrderService(ThinkElectricDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<string> CreateAsync(IEnumerable<CartItemViewModel> cartItems, string userId)
    {
        var order = new Order()
        {
            CreatedOn = DateTime.UtcNow,
            UserId = Guid.Parse(userId),
        };

        foreach (var cartItem in cartItems)
        {
            var orderItem = new OrderItem()
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
        var model = await _dbContext.Orders
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
        var orderExists = await _dbContext.Orders
            .AnyAsync(o => o.Id.ToString() == id);

        return orderExists;
    }

    public async Task<bool> IsOrderFromUserAsync(string id, string userId)
    {
        var isOrderFromUser = await _dbContext.Orders
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

        var order = await _dbContext.Orders
            .Where(o => o.Id.ToString() == id)
            .FirstAsync();

        _dbContext.Orders.Remove(order);

        await _dbContext.SaveChangesAsync();
    }

    public async Task ConfirmAsync(string id)
    {
        var order = await _dbContext.Orders
            .Where(o => o.Id.ToString() == id)
            .FirstAsync();

        order.IsConfirmedByUser = true;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsOrderExisting(string id)
    {
        var isOrderExisting = await _dbContext.Orders
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
                Status = o.OrderItems.Any(oi => !oi.IsFulfilled) ? "Not Fulfilled" : "Fulfilled",
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

    public async Task<byte[]> GetAllInExcelFormatAsync(IEnumerable<OrderItemExcelModel> orderItemModels)
    {
        using var package = new ExcelPackage();

        var worksheet = package.Workbook.Worksheets.Add("Orders");

        var headers = new string[]
        {
            "Product Name", "Price", "Quantity", "Total Sum", "Status",
            "Buyer Name", "Buyer Phone Number", "Country", "City", "Street", "Zip Code", "Created On"
        };

        for (int col = 1; col <= headers.Length; col++)
        {
            worksheet.Cells[1, col].Value = headers[col - 1];
        }

        using (var range = worksheet.Cells[1, 1, 1, headers.Length])
        {
            range.Style.Font.Bold = true;
        }

        var row = 2;
        foreach (var orderItem in orderItemModels)
        {
            worksheet.Cells[row, 1].Value = orderItem.ProductName;
            worksheet.Cells[row, 2].Value = orderItem.Price;
            worksheet.Cells[row, 3].Value = orderItem.Quantity;
            worksheet.Cells[row, 4].Value = orderItem.TotalSum;
            worksheet.Cells[row, 5].Value = orderItem.Status;
            worksheet.Cells[row, 6].Value = orderItem.BuyerName;
            worksheet.Cells[row, 7].Value = orderItem.BuyerPhoneNumber;
            worksheet.Cells[row, 8].Value = orderItem.Country;
            worksheet.Cells[row, 9].Value = orderItem.City;
            worksheet.Cells[row, 10].Value = orderItem.Street;
            worksheet.Cells[row, 11].Value = orderItem.ZipCode;
            worksheet.Cells[row, 12].Value = orderItem.CreatedOn;

            row++;
        }

        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

        return await package.GetAsByteArrayAsync();
    }

    public async Task<IEnumerable<OrderItemExcelModel>> GetOrderItemsForExcelAsync(string companyId)
    {
        IEnumerable<OrderItemExcelModel> orderItemModels = await _dbContext
            .OrderItems
            .Where(oi => oi.Product.CompanyId.ToString() == companyId && oi.Order.IsConfirmedByUser)
            .OrderByDescending(oi => oi.Order.CreatedOn)
            .Select(oi => new OrderItemExcelModel()
            {
                ProductName = oi.Product.Name,
                Price = oi.Product.Price.ToString("F2"),
                Quantity = oi.Quantity,
                TotalSum = (oi.Product.Price * oi.Quantity).ToString("F2"),
                Status = oi.IsFulfilled ? "Fulfilled" : "Not Fulfilled",
                BuyerName = oi.Order.User.FirstName + " " + oi.Order.User.LastName,
                BuyerPhoneNumber = oi.Order.User.PhoneNumber,
                Country = oi.Order.User.Address!.Country,
                City = oi.Order.User.Address.City,
                Street = oi.Order.User.Address.Street,
                ZipCode = oi.Order.User.Address.ZipCode,
                CreatedOn = oi.Order.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
            })
            .ToArrayAsync();

        return orderItemModels;
    }

    public async Task<bool> HasOrdersAsync(string companyId)
    {
        var hasOrders = await _dbContext.OrderItems
            .AnyAsync(oi => oi.Product.CompanyId.ToString() == companyId);

        return hasOrders;
    }

    public async Task<bool> OrderItemExistsAsync(string orderItemId)
    {
        var orderItemExists = await _dbContext.OrderItems
            .AnyAsync(oi => oi.Id.ToString() == orderItemId);

        return orderItemExists;
    }

    public async Task<bool> IsOrderItemFromCompanyAsync(string orderItemId, string companyId)
    {
        var isOrderItemFromCompany = await _dbContext.OrderItems
            .AnyAsync(oi => oi.Id.ToString() == orderItemId && oi.Product.CompanyId.ToString() == companyId);

        return isOrderItemFromCompany;
    }

    public async Task MarkAsFulfilledAsync(string orderItemId)
    {
        var orderItem = await _dbContext.OrderItems
            .Where(oi => oi.Id.ToString() == orderItemId)
            .FirstAsync();

        orderItem.IsFulfilled = true;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<OrderAllAdminViewModel>> GetAllOrdersAsync()
    {
        IEnumerable<OrderAllAdminViewModel> orderModels = await _dbContext
            .Orders
            .Where(o => o.IsConfirmedByUser)
            .OrderByDescending(o => o.CreatedOn)
            .Select(o => new OrderAllAdminViewModel()
            {
                UserFullName = o.User.FirstName + " " + o.User.LastName,
                UserEmail = o.User.Email,
                CreatedOn = o.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                TotalSum = o.OrderItems.Sum(oi => oi.Product.Price * oi.Quantity).ToString("F2"),
                Status = o.OrderItems.Any(oi => !oi.IsFulfilled) ? "Not Fulfilled" : "Fulfilled",
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
}
