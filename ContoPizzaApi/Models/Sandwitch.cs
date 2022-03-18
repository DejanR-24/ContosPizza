using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContoPizzaApi.Models;

public class Sandwitch
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; } = null!;
    public bool IsGlutenFree { get; set; }
}
