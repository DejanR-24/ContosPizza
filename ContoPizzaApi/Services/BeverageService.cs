using ContoPizzaApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace ContoPizzaApi.Services;

public class BeverageService
{
    private readonly IMongoCollection<Beverage> _beverageCollection;

    public BeverageService(
        IOptions<MongoDBSettings> mongoDBSettings)
    {
        var mongoClient = new MongoClient(
            mongoDBSettings.Value.ConnectionURI);

        var mongoDatabase = mongoClient.GetDatabase(
            mongoDBSettings.Value.DatabaseName);

        _beverageCollection = mongoDatabase.GetCollection<Beverage>(
           mongoDBSettings.Value.CollectionNameBeverage);
    }
    public async Task<List<Beverage>> GetAsync() =>
        await _beverageCollection.Find(_ => true).ToListAsync();

    public async Task<Beverage?> GetAsync(string id) =>
        await _beverageCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Beverage newBeverage) =>
        await _beverageCollection.InsertOneAsync(newBeverage);

    public async Task UpdateAsync(string id, Beverage updatedBeverage) =>
        await _beverageCollection.ReplaceOneAsync(x => x.Id == id, updatedBeverage);

    public async Task RemoveAsync(string id) =>
        await _beverageCollection.DeleteOneAsync(x => x.Id == id);


}
