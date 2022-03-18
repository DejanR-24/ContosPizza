using ContoPizzaApi.Models;

namespace ContoPizzaApi.Interfaces;

public interface IBackupService
{
   void SavePizza(Pizza pizza);
   void SavePizzaToFile(Pizza pizza);
   void SavePizzaToBlob(Pizza pizza);

}
