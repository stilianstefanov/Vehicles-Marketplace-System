namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Bike;

public interface IBikeService
{
    Task<string> CreateAsync(BikeCreateViewModel bikeModel, string productId);
    Task<BikeDetailsViewModel?> GetBikeDetailsByIdAsync(string id);
    Task<bool> IsBikeExistingAsync(string id);
    Task<bool> IsUserAuthorizedToEditAsync(string id, string companyId);
    Task<BikeEditViewModel> GetBikeEditViewModelByIdAsync(string id);
    Task<string> GetProductIdByBikeIdAsync(string id);
    Task EditAsync(string id, BikeEditViewModel bikeModel);
}
