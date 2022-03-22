using ContoPizzaApi.Interfaces;
using ContoPizzaApi.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Azure.Storage.Files.Shares;
using Azure;

namespace ContoPizzaApi.Services;

public class MemoryPizzaServiceAzureFile : IBackupServiceFile
{

    private readonly IOptions<AzureSettings> azureSettings;

    public MemoryPizzaServiceAzureFile(IOptions<AzureSettings> azureSettings)
    {
        this.azureSettings = azureSettings;

    }
    public void SavePizzaToFile(Pizza pizza)
    {


        string connectionString = azureSettings.Value.ConnectionURI;
        string shareName = azureSettings.Value.ShareName;
        string directoryName = azureSettings.Value.DirectoryName;
        ShareClient shareClient = new ShareClient(connectionString, shareName);
        shareClient.CreateIfNotExists();

        ShareDirectoryClient directory = shareClient.GetDirectoryClient(directoryName);
        directory.CreateIfNotExists();

        string filePath = @".\Deleted\Pizza\";
        string jsonString = JsonSerializer.Serialize(pizza);
        string fileName = "deleted_Pizza" + Guid.NewGuid().ToString() + ".json";

        File.WriteAllText(filePath + fileName, jsonString);

        ShareFileClient file = directory.GetFileClient(fileName);
        using (FileStream stream = File.OpenRead(filePath + fileName))
        {
            file.Create(stream.Length);
            file.UploadRange(
                new HttpRange(0, stream.Length),
                stream);
        }




    }


}
