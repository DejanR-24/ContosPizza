using ContoPizzaApi.Models;
using MediatR;

namespace ContoPizzaApi.Commands
{
    public class UpdatePizzaCommand : IRequest<Pizza>
    {
        public string Id {get;set;}

        public Pizza UpdatedPizza = new Pizza();

        public UpdatePizzaCommand(string id,Pizza pizza)
        {
            Id= id;
            UpdatedPizza.Name = pizza.Name;
            UpdatedPizza.IsGlutenFree = pizza.IsGlutenFree;
            UpdatedPizza.Id = pizza.Id;

        }
    }
}
