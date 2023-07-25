namespace ThinkElectric.Services.Contracts;

using Web.ViewModels.Scooter;

public interface IScooterService
{
    Task<string> CreateAsync(ScooterCreateViewModel model, string productId);
    Task<ScooterDetailsViewModel?> GetScooterDetailsByIdAsync(string id);
}
