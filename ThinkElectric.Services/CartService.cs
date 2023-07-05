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


    public async Task CreateAsync(Guid userId)
    {
        await _dbContext.Carts.AddAsync(new Cart()
        {
            UserId = userId
        });

        await _dbContext.SaveChangesAsync();
    }
}
