using ContoPizzaApi.Interfaces;
using ContoPizzaApi.Models;
using ContoPizzaApi.Queries;
using MediatR;

namespace ContoPizzaApi.QueryHandlers
{
    public class GetPizzasBySearchHandler: IRequestHandler<GetPizzasBySearchQuery, List<Pizza?>>
    {
        private readonly IPizzaService _pizzaService;
        public GetPizzasBySearchHandler(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        public async Task<List<Pizza?>> Handle(GetPizzasBySearchQuery request, CancellationToken cancellationToken)
        {
            var pizzas = await _pizzaService.SearchAndGetAsync();
            var results = new List<Pizza?>();
            for(var i = 0; i < pizzas.Count; i++)
            {
                if (pizzas[i].Name.ToLower().Contains(request.Search.ToLower()))
                    results.Add(pizzas[i]);
            }
            return results;
        }

    }
}
