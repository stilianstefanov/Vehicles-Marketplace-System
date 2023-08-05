namespace ThinkElectric.Services.Contracts;

using Microsoft.AspNetCore.Http;

using Web.ViewModels;

public interface IImageService
{
    Task<string> CreateAsync(IFormFile imageFile);

    Task<ImageViewModel> GetImageByIdAsync(string imageId);
    Task UpdateAsync(string imageId, IFormFile newImage);
}
