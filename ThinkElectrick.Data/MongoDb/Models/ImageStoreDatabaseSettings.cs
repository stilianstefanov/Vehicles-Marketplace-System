namespace ThinkElectric.Data.MongoDb.Models;

public class ImageStoreDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ImagesCollectionName { get; set; } = null!;
}
