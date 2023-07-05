namespace ThinkElectric.Services.Contracts;

public interface ICartService
{
    Task CreateAsync(Guid userId);
}
