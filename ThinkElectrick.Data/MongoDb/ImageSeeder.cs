namespace ThinkElectric.Data.MongoDb;

using System.Security.Authentication;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

using Models;


public class ImageSeeder
{
    private readonly IMongoCollection<Image> _imageCollection;

    public ImageSeeder(IOptions<ImageStoreDatabaseSettings> imageStoreDatabaseSettings)
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

    public void SeedData()
    {
        if (_imageCollection.CountDocuments(image => true) == 0)
        {
             _imageCollection.InsertMany(SeedImages());
        }
    }

    private List<Image> SeedImages()
    {
        List<Image> images = new List<Image>();

        Image image = new Image()
        {
            Id = "64ce4237f1bda0c4b930c421",
            ImageType = "image/jpeg",
            Data = File.ReadAllBytes("wwwroot/img/seed/company1.jpeg")
        };

        images.Add(image);

        image = new Image()
        {
            Id = "64ce53d31dee708ccae9319d",
            ImageType = "image/jpeg",
            Data = File.ReadAllBytes("wwwroot/img/seed/company2.jpeg")
        };

        images.Add(image);

        image = new Image()
        {
            Id = "64ce5c39ad51218262de3f60",
            ImageType = "image/jpeg",
            Data = File.ReadAllBytes("wwwroot/img/seed/kaabowolfwarrior.jpeg")
        };

        images.Add(image);

        image = new Image()
        {
            Id = "64ce5f7abd83b3123fc9ab71",
            ImageType = "image/jpeg",
            Data = File.ReadAllBytes("wwwroot/img/seed/kaabomantisking.jpeg")
        };

        images.Add(image);

        image = new Image()
        {
            Id = "64ce64743037f396fd344914",
            ImageType = "image/jpeg",
            Data = File.ReadAllBytes("wwwroot/img/seed/kaabomantis10pro.jpeg")
        };

        images.Add(image);

        image = new Image()
        {
            Id = "64ce66572b18cc1f74d4f76b",
            ImageType = "image/jpeg",
            Data = File.ReadAllBytes("wwwroot/img/seed/xiaomipro2.jpeg")
        };
        
        images.Add(image);

        image = new Image()
        {
            Id = "64ce67d918f465b345e8e244",
            ImageType = "image/jpeg",
            Data = File.ReadAllBytes("wwwroot/img/seed/xiaomimi365.jpeg")
        };

        images.Add(image);

        image = new Image()
        {
            Id = "64ce68eaffdd5b60bd0f0da6",
            ImageType = "image/jpeg",
            Data = File.ReadAllBytes("wwwroot/img/seed/xiamioamg.jpeg")
        };

        images.Add(image);

        image = new Image()
        {
            Id = "64ce6ac5332b4fcd3cfbf213",
            ImageType = "image/jpeg",
            Data = File.ReadAllBytes("wwwroot/img/seed/adoa16xe.jpeg")
        };

        images.Add(image);

        image = new Image()
        {
            Id = "64ce6e7274be46075f7f39ef",
            ImageType = "image/jpeg",
            Data = File.ReadAllBytes("wwwroot/img/seed/adoa20air.jpeg")
        };

        images.Add(image);

        image = new Image()
        {
            Id = "64ce6fc7a61cf5992be7bf4f",
            ImageType = "image/jpeg",
            Data = File.ReadAllBytes("wwwroot/img/seed/ado20fbeast.jpeg")
        };

        images.Add(image);

        image = new Image()
        {
            Id = "64ce7219abc83a6ff8f1da33",
            ImageType = "image/jpeg",
            Data = File.ReadAllBytes("wwwroot/img/seed/kaabobag.jpeg")
        };

        images.Add(image);

        image = new Image()
        {
            Id = "64ce737583efd7dc2360de20",
            ImageType = "image/jpeg",
            Data = File.ReadAllBytes("wwwroot/img/seed/kaabodisclock.jpeg")
        };

        images.Add(image);

        image = new Image()
        {
            Id = "64ce74f819d865ac7cfd73e6",
            ImageType = "image/jpeg",
            Data = File.ReadAllBytes("wwwroot/img/seed/xiaomisidemirrors.jpeg")
        };

        images.Add(image);

        return images;
    }
}
