using ContoPizzaApi.Interfaces;
using ContoPizzaApi.Models;
using System.Text.Json;

namespace ContoPizzaApi.Services;

public class MemoryPizzaService : IBackupService
{
    public static int backupCount = 0;

    public void SavePizza(Pizza pizzaToSave)
    {
        string filePath = @".\DeletedPizzas\";
        string jsonString = JsonSerializer.Serialize(pizzaToSave);
        string fileName = "deleted_pizza_" + backupCount + ".json";
        backupCount++;
        File.WriteAllText(filePath + fileName, jsonString);
    }

    void IBackupService.SavePizzaToBlob(Pizza pizza)
    {
        return;
    }

    void IBackupService.SavePizzaToFile(Pizza pizza)
    {
        return;
    }
}
