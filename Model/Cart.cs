using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Cart
{   
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Status")]
    public string Status { get; set; }

    [BsonElement("Product")]
    public List<Product> Product { get; set; }

    [BsonElement("modified_on")]
    public string modified_on { get; set; }
}