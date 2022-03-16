using ContoPizzaApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ContoPizzaApi.Services;

public class PizzasService
{
    private readonly IMongoCollection<Pizza> _pizzasCollection;

    public PizzasService(
        IOptions<ContosPizzaDatabaseSettings> contosPizzaDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            contosPizzaDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            contosPizzaDatabaseSettings.Value.DatabaseName);

        _pizzasCollection = mongoDatabase.GetCollection<Pizza>(
           contosPizzaDatabaseSettings.Value.PizzasCollectionName);
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
