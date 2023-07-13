namespace ThinkElectric.Services.Contracts;

public interface ICartService
{
    Task<string> CreateAsync(Guid userId);
}
