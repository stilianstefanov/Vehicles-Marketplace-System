namespace ThinkElectric.Services
{
    using System.Security.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Driver;

    using Contracts;
    using Data.MongoDb.Models;
    using Web.ViewModels;

    public class ImageService : IImageService
    {
        private readonly IMongoCollection<Image> _imageCollection;

        public ImageService(IOptions<ImageStoreDatabaseSettings> imageStoreDatabaseSettings)
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(
                new MongoUrl(imageStoreDatabaseSettings.Value.ConnectionString)
            );
            settings.SslSettings =
                new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);

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
            var x = await _imageCollection
                .Find(image => image.Id == imageId)
                .FirstOrDefaultAsync();

            var model = new ImageViewModel()
            {
                ImageType = x.ImageType,
                Data = x.Data
            };
           

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
