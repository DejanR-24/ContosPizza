using ContoPizzaApi.Models;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContoPizzaApi.Commands
{
    public class CreatePizzaCommand : IRequest<Pizza>
    {

        public Pizza NewPizza = new Pizza();

        public CreatePizzaCommand(Pizza pizza)
        {

            NewPizza.Name = pizza.Name;
            NewPizza.IsGlutenFree = pizza.IsGlutenFree;
            if (pizza.Id != null)
                NewPizza.Id = pizza.Id;

        }

    }

}
