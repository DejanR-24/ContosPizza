using ContoPizzaApi.Commands;
using ContoPizzaApi.Interfaces;
using ContoPizzaApi.Models;
using MediatR;

namespace ContoPizzaApi.CommandHandlers
{
    public class UpdatePizzaHandler: IRequestHandler<UpdatePizzaCommand, Pizza>
    
        
    {
        private readonly IPizzaService _pizzaService;

        public UpdatePizzaHandler(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }
        public async Task<Pizza> Handle(UpdatePizzaCommand request, CancellationToken cancellationToken)
        {

            return await _pizzaService.UpdateAsync(request.Id,request.UpdatedPizza);

        }
    }
}

