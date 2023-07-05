namespace ThinkElectric.Services.Contracts;

public interface ICartService
{
    Task CreateAsync(string userId);
}
