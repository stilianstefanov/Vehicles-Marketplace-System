namespace ThinkElectric.Services;

using Contracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Cart;

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

    public async Task<IEnumerable<CartItemViewModel>> GetAllAsync(string getId)
    {
        IEnumerable<CartItemViewModel> cartItems = await _dbContext.CartItems
            .Where(ci => ci.Cart.UserId.ToString() == getId)
            .Select(ci => new CartItemViewModel()
            {
                Id = ci.Id.ToString(), //TODO Return ProductID
                ProductName = ci.Product.Name,
                Quantity = 1
            })
            .ToArrayAsync();

        return cartItems;
    }
}
