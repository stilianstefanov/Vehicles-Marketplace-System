namespace ThinkElectric.Services;

using Contracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.CartItem;

public class CartService : ICartService
{
    private readonly ThinkElectricDbContext _dbContext;

    public CartService(ThinkElectricDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<string> CreateAsync(Guid userId)
    {
        Cart cart = new Cart()
        {
            UserId = userId
        };

        await _dbContext.Carts.AddAsync(cart);

        await _dbContext.SaveChangesAsync();

        return cart.Id.ToString();
    }

    public async Task AddToCartAsync(string id, string userId)
    {
        Guid cartId = await _dbContext.Carts
            .Where(c => c.UserId.ToString() == userId)
            .Select(c => c.Id)
            .FirstOrDefaultAsync();

        CartItem cartItem = new CartItem()
        {
            ProductId = Guid.Parse(id),
            CartId = cartId
        };

        await _dbContext.CartItems.AddAsync(cartItem);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> CartItemExistsAsync(string id)
    {
        bool cartItemExists = await _dbContext.CartItems
            .AnyAsync(ci => ci.Id.ToString() == id);

        return cartItemExists;
    }

    public async Task<bool> IsUserAuthorizedAsync(string id, string? getId)
    {
        bool isUserAuthorized = await _dbContext.CartItems
            .AnyAsync(ci => ci.Id.ToString() == id && ci.Cart.UserId.ToString() == getId);

        return isUserAuthorized;
    }

    public async Task RemoveFromCartAsync(string id)
    {
        CartItem cartItem = await _dbContext.CartItems
            .FirstAsync(ci => ci.Id.ToString() == id);

        _dbContext.CartItems.Remove(cartItem);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<IList<CartItemViewModel>> GetAllAsync(string getId)
    {
        IList<CartItemViewModel> cartItems = await _dbContext.CartItems
            .Where(ci => ci.Cart.UserId.ToString() == getId)
            .Select(ci => new CartItemViewModel()
            {
                Id = ci.Id.ToString(),
                ProductId = ci.ProductId.ToString(), 
                ProductName = ci.Product.Name,
                Price = ci.Product.Price.ToString("f2"),
                AvailableQuantity = ci.Product.Quantity
            })
            .ToArrayAsync();

        return cartItems;
    }

    public async Task<bool> ProductAlreadyAdded(string id, string userId)
    {
        bool productAlreadyAdded = await _dbContext.CartItems
            .AnyAsync(ci => ci.ProductId.ToString() == id && ci.Cart.UserId.ToString() == userId);

        return productAlreadyAdded;
    }

    public async Task<bool> AreCartItemsValidAsync(IEnumerable<CartItemViewModel> cartItems)
    {
        bool areCartItemsValid = true;

        IEnumerable<Product> products = await _dbContext
            .Products
            .ToArrayAsync();

        foreach (CartItemViewModel cartItem in cartItems)
        {
            if (products.All(p => p.Id.ToString().ToLower() != cartItem.ProductId.ToLower()))
            {
                areCartItemsValid = false;
                break;
            }

            if (cartItem.Quantity > products.First(p => p.Id.ToString().ToLower() == cartItem.ProductId.ToLower()).Quantity)
            {
                areCartItemsValid = false;
                break;
            }
        }

        return areCartItemsValid;
    }

    public async Task ClearAsync(string userId)
    {
        IEnumerable<CartItem> cartItems = await _dbContext
            .CartItems
            .Where(ci => ci.Cart.UserId.ToString() == userId)
            .ToArrayAsync();

        _dbContext.CartItems.RemoveRange(cartItems);

        await _dbContext.SaveChangesAsync();
    }
}
