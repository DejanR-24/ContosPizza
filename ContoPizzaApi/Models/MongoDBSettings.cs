namespace ContoPizzaApi.Models;

public class MongoDBSettings
{
    public string ConnectionURI{ get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string CollectionNamePizza { get; set; } = null!;

    public string CollectionNameSandwitch { get; set; } = null!;

    public string CollectionNameBeverage { get; set; } = null!;
}
