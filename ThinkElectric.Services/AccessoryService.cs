namespace ThinkElectric.Services;

using Contracts;
using Data;

public class AccessoryService : IAccessoryService
{
    private readonly ThinkElectricDbContext _dbContext;

    public AccessoryService(ThinkElectricDbContext dbContext)
    {
        _dbContext = dbContext;
    }


}
