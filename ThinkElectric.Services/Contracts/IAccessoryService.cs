namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Accessory;

public interface IAccessoryService
{
    Task<string> CreateAsync(AccessoryCreateViewModel accessoryModel, string productId);
    Task<AccessoryDetailsViewModel?> GetAccessoryDetailsByIdAsync(string id);
    Task<bool> IsAccessoryExistingAsync(string id);
    Task<bool> IsUserAuthorizedToEditAsync(string id, string companyId);
    Task<AccessoryEditViewModel> GetAccessoryEditViewModelByIdAsync(string id);
    Task<string> GetProductIdByAccessoryIdAsync(string id);
    Task EditAsync(string id, AccessoryEditViewModel accessoryModel);
    Task<AccessoryAllQueryModel> GetAllFilteredAndPagedAsync(AccessoryAllQueryModel queryModel);
}
