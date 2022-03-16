namespace ContoPizzaApi.Models;

public class ContosPizzaDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string PizzasCollectionName { get; set; } = null!;
}
