namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Accessory;

public interface IAccessoryService
{
    Task<string> CreateAsync(AccessoryCreateViewModel accessoryModel, string productId);
    Task<AccessoryDetailsViewModel?> GetAccessoryDetailsByIdAsync(string id);
}
