namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Bike;

public interface IBikeService
{
    Task<string> CreateAsync(BikeCreateViewModel bikeModel, string productId);
}
