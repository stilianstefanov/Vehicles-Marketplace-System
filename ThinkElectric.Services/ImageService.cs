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
            string imageType = imageFile.ContentType;

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

        public async Task UpdateAsync(string imageId, IFormFile newImage)
        {
            string imageType = newImage.ContentType;

            byte[] imageBytes;

            await using MemoryStream memoryStream = new MemoryStream();

            await newImage.CopyToAsync(memoryStream);

            imageBytes = memoryStream.ToArray();

            Image updatedImage = new Image
            {
                Id = imageId,
                ImageType = imageType,
                Data = imageBytes
            };

            await _imageCollection.ReplaceOneAsync(i => i.Id == imageId, updatedImage);
        }
    }
}
