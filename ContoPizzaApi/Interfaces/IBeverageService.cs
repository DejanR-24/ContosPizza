using ContoPizzaApi.Models;

namespace ContoPizzaApi.Interfaces
{
    public interface IBeverageService
    {
        Task<List<Beverage>> GetAsync();

        Task<Beverage?> GetAsync(string id);

        Task CreateAsync(Beverage newBeverage);

        Task UpdateAsync(string id, Beverage updatedBeverage);

        Task RemoveAsync(string id);
    }
}
