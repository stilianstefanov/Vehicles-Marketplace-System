namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Scooter;

public interface IScooterService
{
    Task<string> CreateAsync(ScooterCreateViewModel model, string productId);
    Task<ScooterDetailsViewModel?> GetScooterDetailsByIdAsync(string id);
    Task<bool> IsScooterExistingAsync(string id);
    Task<bool> IsUserAuthorizedToEditAsync(string id, string userCompanyId);
    Task<ScooterEditViewModel> GetScooterEditViewModelByIdAsync(string id);
    Task<string> GetProductIdByScooterIdAsync(string id);
    Task EditAsync(string id, ScooterEditViewModel scooterModel);
    Task<ScooterAllQueryModel> GetAllFilteredAndPagedAsync(ScooterAllQueryModel queryModel);
}
