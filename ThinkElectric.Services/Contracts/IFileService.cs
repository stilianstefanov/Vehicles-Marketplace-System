namespace ThinkElectric.Services.Contracts;

using Microsoft.AspNetCore.Http;

public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file, string folderName);
}
