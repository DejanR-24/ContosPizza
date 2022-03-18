using ContoPizzaApi.Interfaces;
using ContoPizzaApi.Models;
using System.Text.Json;
using Azure.Storage.Blobs;

namespace ContoPizzaApi.Services
{
    public class MemoryPizzaServiceAzureBlob : IBackupService
    {
            public static int backupCount = 0;
            string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
        public void SavePizzaToBlob(Pizza pizzaToSave)
        {
            string filePath = @"C:\Users\dejan\OneDrive\Desktop\Dex\mojiProjekti\dotNET\memory\"; //Directory.GetCurrentDirectory
            string jsonString = JsonSerializer.Serialize(pizzaToSave);
            string fileName = "deleted_pizza_" + backupCount + ".json";
            backupCount++;
            File.WriteAllText(filePath + fileName, jsonString);
        }

        void IBackupService.SavePizza(Pizza pizza)
        {
            throw new NotImplementedException();
        }


        void IBackupService.SavePizzaToFile(Pizza pizza)
        {
            throw new NotImplementedException();
        }
    }
}
