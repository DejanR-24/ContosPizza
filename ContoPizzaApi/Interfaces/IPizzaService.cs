using ContoPizzaApi.Models;

namespace ContoPizzaApi.Interfaces
{
    public interface IPizzaService
    {
        Task<List<Pizza>> GetAsync();

        Task<Pizza?> GetAsync(string id);

        Task<List<Pizza?>> SearchAndGetAsync();

        Task<Pizza?> CreateAsync(Pizza newPizza);

        Task<Pizza?> UpdateAsync(string id, Pizza updatedPizza);

        Task RemoveAsync(string id);
    }
}
