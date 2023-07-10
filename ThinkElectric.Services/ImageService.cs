namespace ThinkElectric.Services
{
    using Contracts;
    using Data.MongoDb.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Web.ViewModels;

    public class ImageService : IImageService
    {
        private readonly IMongoCollection<Image> _imageCollection;

        public ImageService(IOptions<ImageStoreDatabaseSettings> imageStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(imageStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(imageStoreDatabaseSettings.Value.DatabaseName);

            _imageCollection = mongoDatabase.GetCollection<Image>(imageStoreDatabaseSettings.Value.ImagesCollectionName);
        }

        public async Task<string> CreateAsync(IFormFile imageFile)
        {
            string imageType = Path.GetExtension(imageFile.FileName);

            byte[] imageBytes;

            await using MemoryStream memoryStream = new MemoryStream();

            await imageFile.CopyToAsync(memoryStream);

            imageBytes = memoryStream.ToArray();

            Image image = new Image
            {
                Id = ObjectId.GenerateNewId().ToString(),
                ImageType = imageType,
                Data = imageBytes
            };

            await _imageCollection.InsertOneAsync(image);

            return image.Id;
        }

        public async Task<ImageViewModel> GetImageByIdAsync(string imageId)
        {
            ImageViewModel model = await _imageCollection
                .Find(image => image.Id == imageId)
                .Project(image => new ImageViewModel
                {
                    ImageType = image.ImageType,
                    Data = image.Data
                })
                .FirstOrDefaultAsync();

            return model;
        }
    }
}
