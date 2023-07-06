namespace ThinkElectric.Services
{
    using Contracts;
    using Microsoft.AspNetCore.Http;

    public class FileService : IFileService
    {
        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folderName);

            string filePath = Path.Combine(folderPath, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(stream);

            return filePath;
        }
    }
}
