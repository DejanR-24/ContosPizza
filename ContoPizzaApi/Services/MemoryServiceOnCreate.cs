using ContoPizzaApi.Interfaces;
using System.Text.Json;

namespace ContoPizzaApi.Services;

public class MemoryServiceOnCreate:IBackupServiceOnCreate
{
    public static int backupCount = 0;

    public void SaveOnCreate<T>(T instanceToSave) { 

    string filePath = ".\\Created\\" + typeof(T).Name + "\\";
    string jsonString = JsonSerializer.Serialize(instanceToSave);
    string fileName = "created_" + typeof(T).Name + backupCount + ".json";
    backupCount++;
    File.WriteAllText(filePath + fileName, jsonString);
}
}
