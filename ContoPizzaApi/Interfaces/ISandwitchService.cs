using ContoPizzaApi.Models;

namespace ContoPizzaApi.Interfaces;

public interface ISandwitchService
{
    Task<List<Sandwitch>> GetAsync();

    Task<Sandwitch> GetAsync(string id);

    Task CreateAsync(Sandwitch newSandwitch);

   Task UpdateAsync(string id, Sandwitch updatedSandwitch);

    Task RemoveAsync(string id) ;
}
