namespace ThinkElectric.Services;

using Contracts;
using Data;

public class BikeService : IBikeService
{
    private readonly ThinkElectricDbContext _dbContext;

    public BikeService(ThinkElectricDbContext dbContext)
    {
        _dbContext = dbContext;
    }


}
