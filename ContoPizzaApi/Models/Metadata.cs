using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContoPizzaApi.Models;

public class Metadata
{

    [BsonElement("Name")]
    public string Name { get; set; } = null!;

    public string Description { get;set;}

    public DateTime Date { get;set;}


}
