using ContoPizzaApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace ContoPizzaApi.Services;

public class PizzaService 
{
    private readonly IMongoCollection<Pizza> _pizzasCollection;

    public PizzaService(
        IOptions<MongoDBSettings> mongoDBSettings)
    {
        var mongoClient = new MongoClient(
            mongoDBSettings.Value.ConnectionURI);

        var mongoDatabase = mongoClient.GetDatabase(
            mongoDBSettings.Value.DatabaseName);

        _pizzasCollection = mongoDatabase.GetCollection<Pizza>(
           mongoDBSettings.Value.CollectionNamePizza);
    }
    public async Task<List<Pizza>> GetAsync() =>
        await _pizzasCollection.Find(_ => true).ToListAsync();

    public async Task<Pizza?> GetAsync(string id) =>
        await _pizzasCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Pizza newPizza) =>
        await _pizzasCollection.InsertOneAsync(newPizza);

    public async Task UpdateAsync(string id, Pizza updatedPizza) =>
        await _pizzasCollection.ReplaceOneAsync(x => x.Id == id, updatedPizza);

    public async Task RemoveAsync(string id) =>
        await _pizzasCollection.DeleteOneAsync(x => x.Id == id);

    
}
