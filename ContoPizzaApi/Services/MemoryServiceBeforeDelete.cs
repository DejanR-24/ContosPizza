using ContoPizzaApi.Interfaces;
using ContoPizzaApi.Models;
using System.Text.Json;

namespace ContoPizzaApi.Services;

public class MemoryServiceBeforeDelete : IBackupServiceBeforeDelete
{
    public static int backupCount = 0;

    public void SaveBeforeDelete<T>(T instanceToSave)
    {
        string filePath = ".\\Deleted\\"+ typeof(T).Name +"\\";
        string jsonString = JsonSerializer.Serialize(instanceToSave);
        string fileName = "deleted_"+ typeof(T).Name + backupCount + ".json";
        backupCount++;
        File.WriteAllText(filePath + fileName, jsonString);
    }


}
