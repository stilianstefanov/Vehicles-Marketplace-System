namespace ThinkElectric.Services;

using Contracts;
using Data;
using Data.Models;

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
}
