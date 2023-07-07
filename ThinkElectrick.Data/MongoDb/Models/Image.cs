namespace ThinkElectric.Data.MongoDb.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Image
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonElement("imageType")]
    public string ImageType { get; set; } = null!;

    [BsonElement("data")]
    public Byte[] Data { get; set; } = null!;
}
