using ContoPizzaApi.Models;

namespace ContoPizzaApi.Interfaces
{
    public interface IPizzaService
    {
        Task<List<Pizza>> GetAsync();

        Task<Pizza?> GetAsync(string id);

        Task CreateAsync(Pizza newPizza);

        Task UpdateAsync(string id, Pizza updatedPizza);

        Task RemoveAsync(string id);
    }
}
