namespace ContoPizzaApi.Models
{
    public class AzureSettings
    {
        public string ConnectionURI{ get; set; } = null!;
        
        public string ContainerName{ get; set; } = null!;

        public string DirectoryName { get; set; } = null!;

        public string ShareName { get; set; } = null!;


    }
}
