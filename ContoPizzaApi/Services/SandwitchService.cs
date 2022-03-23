using ContoPizzaApi.Interfaces;
using ContoPizzaApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ContoPizzaApi.Services;

public class SandwitchService : ISandwitchService
{
    private readonly IMongoCollection<Sandwitch> _sandwitchCollection;

    public SandwitchService(
        IOptions<MongoDBSettings> mongoDBSettings)
    {
        var mongoClient = new MongoClient(
            mongoDBSettings.Value.ConnectionURI);

        var mongoDatabase = mongoClient.GetDatabase(
            mongoDBSettings.Value.DatabaseName);

        _sandwitchCollection = mongoDatabase.GetCollection<Sandwitch>(
            mongoDBSettings.Value.CollectionNameSandwitch);
    }
    public async Task<List<Sandwitch>> GetAsync() =>
        await _sandwitchCollection.Find(_ => true).ToListAsync();

    public async Task<Sandwitch> GetAsync(string id) =>
        await _sandwitchCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Sandwitch newSandwitch) =>
        await _sandwitchCollection.InsertOneAsync(newSandwitch);

    public async Task UpdateAsync(string id, Sandwitch updatedSandwitch) =>
        await _sandwitchCollection.ReplaceOneAsync(x => x.Id == id, updatedSandwitch);

    public async Task RemoveAsync(string id) =>
        await _sandwitchCollection.DeleteOneAsync(x => x.Id == id);

}
