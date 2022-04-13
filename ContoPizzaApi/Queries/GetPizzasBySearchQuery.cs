using ContoPizzaApi.Models;
using MediatR;

namespace ContoPizzaApi.Queries
{
    public class GetPizzasBySearchQuery : IRequest<List<Pizza?>>
    {
        public string Search { get; }
        public GetPizzasBySearchQuery(string search)
        {
            Search = search;
        }
    }
}
