using MediatR;
using ContoPizzaApi.Models;
using ContoPizzaApi.Queries;
using ContoPizzaApi.Interfaces;

namespace ContoPizzaApi.QueryHandlers;

public class GetAllPizzasHandler : IRequestHandler<GetAllPizzasQuery, List<Pizza>>
{
    private readonly IPizzaService _pizzaService;
    public GetAllPizzasHandler(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }
    public async Task<List<Pizza>> Handle(GetAllPizzasQuery request, CancellationToken cancellationToken)
    {
        var pizzas = await _pizzaService.GetAsync();
        return pizzas;
    }
}
