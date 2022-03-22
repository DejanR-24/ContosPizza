namespace ContoPizzaApi.Interfaces
{
    public interface IBackupServiceOnCreate
    {
           void SaveOnCreate<T>(T instance);
    }
}
