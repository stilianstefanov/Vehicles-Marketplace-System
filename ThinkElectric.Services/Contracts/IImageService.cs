namespace ThinkElectric.Services.Contracts;

using Microsoft.AspNetCore.Http;

public interface IImageService
{
    Task<string> CreateAsync(IFormFile imageFile);
}
