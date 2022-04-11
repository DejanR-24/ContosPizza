using MediatR;
using ContoPizzaApi.Models;
using ContoPizzaApi.Queries;
using ContoPizzaApi.Interfaces;

namespace ContoPizzaApi.QueryHandlers;

    public class GetPizzaByIdHandler : IRequestHandler<GetPizzaByIdQuery, Pizza>
    {
        private readonly IPizzaService _pizzaService;
        public GetPizzaByIdHandler(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }
        public async Task<Pizza?> Handle(GetPizzaByIdQuery request, CancellationToken cancellationToken)
        {
            var pizza = await  _pizzaService.GetAsync(request.Id);
            return pizza == null ? null : pizza;
        }


    }

