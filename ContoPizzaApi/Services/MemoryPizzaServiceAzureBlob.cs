using ContoPizzaApi.Interfaces;
using ContoPizzaApi.Models;
using System.Text.Json;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;

namespace ContoPizzaApi.Services
{
    public class MemoryPizzaServiceAzureBlob : IBackupServiceBlob
    {
        private readonly IOptions<AzureSettings> azureSettings;

        public MemoryPizzaServiceAzureBlob(IOptions<AzureSettings> azureSettings)
        {
            this.azureSettings = azureSettings;
            
        }
        public async void SavePizzaToBlob(Pizza pizza)
        {


            string connectionString = azureSettings.Value.ConnectionURI;
            string containerName = azureSettings.Value.ContainerName;
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            containerClient.CreateIfNotExists();

            string filePath = @".\Deleted\Pizza\"; 
            string jsonString = JsonSerializer.Serialize(pizza);
            string fileName = "deleted_Pizza" + Guid.NewGuid().ToString() + ".json";
       
            File.WriteAllText(filePath + fileName, jsonString);

            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(filePath + fileName, true);


            
            

        }


    }
}
