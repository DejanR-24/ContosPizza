using ContoPizzaApi.Models;

namespace ContoPizzaApi.Interfaces;

public interface IBackupServiceBlob
{
      void SavePizzaToBlob(Pizza pizza);
}
