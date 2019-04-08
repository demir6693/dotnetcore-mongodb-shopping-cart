using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Reserved
{   
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id; // Id of user

    [BsonElement("Quantity")]
    public int Quantity { get; set; }

    [BsonElement("created_on")]
    public string created_on { get; set; }
}